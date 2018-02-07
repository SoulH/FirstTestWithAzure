let URL_BASE = location.origin;
let username = "admin";
let password = "12345";

let url = (action = "") => `${URL_BASE}/${action}`;

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