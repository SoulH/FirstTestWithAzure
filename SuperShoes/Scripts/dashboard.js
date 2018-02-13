let tstates = ["primary", "info", "warning", "danger"];

var stores = new Vue({
    el: '#stores-grid',
    data: {
        list: [],
        isLoading: true
    }
});

$(document).ready(function () {
    load_stores();
});

function load_stores() {
    request({
        url: api("/services/stores"),
        type: "GET"
    }).then(
        function fulfillHandler(data) {
            if (data.Success) {
                stores._data.isLoading = false;
                stores._data.list = data.Stores;
            }
                
        }).catch(function errorHandler(error) {
            // error
        });
}
