define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app'], function (router, http, ko, app) {
    "use strict";

    return {
        errorMessage: ko.observable(),
        
        activate: function () {

            var token = localStorage.getItem("x-brilliance-token");
            
        }
    };
});