define(['plugins/http', 'durandal/app', 'knockout'], function (http, app, ko) {
    "use strict";

    return {
        message: ko.observable(),
        activate: function (action) {
            
            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            if (action === "logout") {
                http.post(brilliance.appbaseurl() + "/Mobile/AppAccount/Logout", '', { 'x-brilliance-token': token })
                    .then(function(response, textStatus) {
                        that.message(response.Message);
                    }).fail(function(jqXHR, textStatus, errorThrown) {
                        that.message(textStatus);
                    }).always(function() {

                    });

                localStorage.removeItem("x-brilliance-token");
                window.location.href = '';
            }
            else if (action === "deactivate") {
            }
            else if (action === "delete") {
                app.showMessage('Vill du verkligen ta bort ditt konto, din profil, dina meddelande och kontakter kommer att tas bort?', 'Bekräfta', ['Ja', 'Nej']);
            }
        }
    };
});

