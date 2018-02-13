
var articlesTable = new Vue({
    el: '#tbArticles',
    data: {
        isLoading: true,
        dataSet: [],
        page: 1,
        showList: [10, 25, 50, 100],
        show: 10
    },
    computed: {
        pagesCount: (t = this) => Math.ceil(t.dataSet.length / t.show),
        pages: (t = this) => {
            let n = Math.ceil(t.dataSet.length / t.show);
            let arr = new Array(n);
            for (i = 0; i < n; i++) {
                c = ((i + 1) === t.page) ? "page-item active" : "page-item";
                arr[i] = { num: i + 1, class: c };
            }
            return arr;
        },
        list: (t = this) => {
            if (t.dataSet.length == 0) return [];
            let min = (t.page - 1) * t.show;
            let max = min + t.show;
            return t.dataSet.filter((f, i) => (i >= min) && (i < max))
        },
        next: (t = this) => {
            let n = Math.ceil(t.dataSet.length / t.show);
            t.page = ((t.page + 1) > n) ? n : t.page + 1;
        },
        prev: (t = this) => {
            t.page = ((t.page - 1) < 1) ? 1 : t.page - 1;
        }
    }
});

var articleModal = new Vue({
    el: "#mdlArticles",
    data: {
        stores: [],
        article: {
            "Id": 0,
            "Name": "",
            "Description": "",
            "Price": 0,
            "Total_in_shelf": 0,
            "Total_in_vault": 0,
            "StoreId": 0
        }
    },
    computed: {
        resetArticle: (t = this) => {
            t.article = {
                "Id": 0,
                "Name": "",
                "Description": "",
                "Price": 0,
                "Total_in_shelf": 0,
                "Total_in_vault": 0,
                "StoreId": 0
            };
        }
    }
});

$(document).ready(function () {
    load_articles();
    load_stores2();
    $("#btnCloseMdlArticle").click(() => {
        articleModal._computedWatchers.resetArticle.run();
        $("#storeId").val(0);
    });
});

let removeArticle = (id) => {
    let e = articlesTable.dataSet.find(f => f.Id == id);
    let idx = articlesTable.dataSet.indexOf(e);
    if (idx > -1)
        articlesTable.dataSet.splice(idx, 1);
};

let addArticle = (obj) => {
    let e = articlesTable.dataSet.find(f => f.Id == obj.Id);
    if (e != null) removeArticle(e.Id);
    obj.Store_name = articleModal.stores.find(f => f.Id == obj.StoreId).Name;
    articlesTable.dataSet.push(obj);
}

function load_articles(storeId = 0) {
    let action = (storeId === 0) ? "/services/articles" : `/services/articles/stores/${storeId}`;
    request({
        url: api(action),
        type: "GET"
    }).then(
        function fulfillHandler(data) {
            if (data.Success) {
                articlesTable._data.isLoading = false;
                articlesTable._data.dataSet = data.Articles;
            }

        }).catch(function errorHandler(error) {
            // error
        });
}

function load_stores2() {
    request({
        url: api("/services/stores"),
        type: "GET"
    }).then(
        function fulfillHandler(data) {
            if (data.Success) {
                articleModal._data.stores = data.Stores;
            }

        }).catch(function errorHandler(error) {
            // error
        });
}

function loadArticleModal(id) {
    let art = articlesTable._data.dataSet.find(f => f.Id == id);
    articleModal.article.Id = art.Id;
    articleModal.article.Name = art.Name;
    articleModal.article.Description = art.Description;
    articleModal.article.Price = art.Price;
    articleModal.article.Total_in_shelf = art.Total_in_shelf;
    articleModal.article.Total_in_vault = art.Total_in_vault;
    let st = articleModal.stores.find(f => f.Name == art.Store_name);
    articleModal.article.StoreId = st.Id;
    //$("#storeId").val(st.Id);
}

function editArticle(id) {
    loadArticleModal(id);
    $("#addArticle").click();
}

function resetArticleModal() {
    let id = $("#mdlArticleId").val();
    if (id == 0) articleModal._computedWatchers.resetArticle.run();
    else loadArticleModal(id);
}

function deleteArticle(id) {
    art = articlesTable.list.find(f => f.Id == id);
    msg = `Esta seguro que desea eliminar el articulo ${art.Name}`;
    if (confirm(msg)) {
        request({
            url: api("/services/articles/"+id),
            type: "DELETE"
        }).then(
            function fulfillHandler(data) {
                console.log("deleteArticleSuccess");
                console.log(data);
                removeArticle(id);

            }).catch(function errorHandler(error) {
                console.log("deleteArticleError");
                console.log(error);
            });
    }
}

function saveArticle() {
    var art = articleModal.article;
    let method = (art.Id == 0) ? "POST" : "PUT";
    let action = (art.Id == 0) ? "/services/articles" : "/services/articles/" + art.Id;
    console.log();
    request({
        url: api(action),
        type: method,
        data: JSON.stringify(art)
    }).then(
        function fulfillHandler(data) {
            console.log("SaveArticleSuccess");
            console.log(data);
            $("#btnCloseMdlArticle").click();
            articleModal._computedWatchers.resetArticle.run();
            addArticle(art);
        }).catch(function errorHandler(error) {
            console.log("error");
            console.log(error);
        });
}