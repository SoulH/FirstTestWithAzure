let URL_BASE = location.origin;
let URL_API = "https://supershoesapi20180211124548.azurewebsites.net";
let username = "admin";
let password = "12345";

let url = (action = "") => `${URL_BASE}/${action}`;
let api = (action = "") => {
    if (action.trim() === "")
        return URL_API;
    if (action[0] != '/')
        return `${URL_API}/${action}`;
    return URL_API + action;
};

$.ajaxSetup({
    dataType: 'json',
    cache: true,
    global: true,
    data: {},
    contentType: 'application/json',
    beforeSend: function (xhr) {
        xhr.setRequestHeader("Authorization", "Basic " + btoa(username + ":" + password));
    }
});

function request(options) {
     return new Promise(function (resolve, reject) {
         $.ajax(options).done(resolve).fail(reject);
     });
}