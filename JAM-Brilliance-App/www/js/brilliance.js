"use strict";
var pnr = [
    {  }
];

var brilliance = {

    appbaseurl: function () {
        var app = document.URL.indexOf('http://') === -1 && document.URL.indexOf('https://') === -1;

        if (!app && document.URL.indexOf("localhost") > -1) {
            return "http://localhost:45562/";
        } else {
            return "https://brilliance.se/";
        }
    },

    handleErrors: function (jqXHR, textStatus, errorThrown) {
        if (jqXHR.status === 403) {
            window.location.href = '#login';
        }
    },

    signup1: {},
    signup2: {},
    signup3: {},
    searchPostalCode: ""
};
