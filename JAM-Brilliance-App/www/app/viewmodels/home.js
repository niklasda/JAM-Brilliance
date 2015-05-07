define(['plugins/router', 'knockout'], function (router, ko) {
    "use strict";

    return {
        name: ko.observable(),
        isLoggedIn: ko.observable(false),

        activate: function () {
            var token = localStorage.getItem("x-brilliance-token");
            if (token && token.length === 36) {
                this.isLoggedIn(true);

            }
        }
    };
});