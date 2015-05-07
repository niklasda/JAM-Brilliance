define(['plugins/http', 'durandal/app', 'knockout'], function (http, app, ko) {
    "use strict";

    return {
        message: ko.observable(),
        activate: function () {
            
            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            http.post(brilliance.appbaseurl() + "/Mobile/AppAccount/Logout", '', { 'x-brilliance-token': token })
                .then(function (response, textStatus) {
                    that.message(response.Message);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    that.message(textStatus);
                }).always(function () {
                    
                });

            localStorage.removeItem("x-brilliance-token");
            window.location.href = '';

        }
    };
});

