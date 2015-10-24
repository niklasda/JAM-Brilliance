define(['plugins/router', 'plugins/http', 'durandal/app', 'knockout', 'knockout-validation'], function (router, http, app, ko, koval) {
    "use strict";

    return {
        userName: ko.observable("").extend({
            required: { params: true, message: 'måste ange giltigt användarnamn' },
            minLength: { params: 2, message: 'måste vara minst {0} tecken' }
        }),
        password: ko.observable("").extend({
            required: { params: true, message: 'måste ange giltigt lösenord' },
            minLength: { params: 6, message: 'måste vara minst {0} tecken' }
        }),
        message: ko.observable(""),

        onLoginClick: function () {

            var result = ko.validation.group(this, { deep: false });
            var valid = this.isValid();
            if (valid) {

                var loginModel = {
                    userName: this.userName(),
                    password: this.password()
                }

                
                var that = this;

                http.post(brilliance.appbaseurl() + "/Mobile/AppAccount/LoginSubmit", loginModel, '')
                    .then(function (response, textStatus) {
                        localStorage.setItem("x-brilliance-token", response.Token);
                        //that.message(response.Message);
                        window.location.href = '';
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        if (jqXHR.responseJSON) {
                            that.message(jqXHR.responseJSON.Message);
                        } else {
                            that.message(textStatus + " / " + errorThrown);
                        }
                    });

              //  return true;
            } else {
                result.showAllMessages();
                return false;
            }

        }
    };
});



