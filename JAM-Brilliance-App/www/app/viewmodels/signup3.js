define(['plugins/router', 'plugins/http', 'durandal/app', 'knockout', 'knockout-validation'], function (router, http, app, ko, koval) {
    "use strict";

    return {
        username: ko.observable("").extend({
            required: { params: true, message: 'användarnamn måste anges' },
            minLength: { params: 5, message: 'användarnamn måste vara minst {0} tecken' }
        }),
        password: ko.observable("").extend({
            required: { params: true, message: 'lösenord måste anges' },
            minLength: { params: 6, message: 'lösenord måste vara minst {0} tecken' }
        }),
        email: ko.observable("").extend({
            required: { params: true, message: 'måste ange giltig e-post' },
            minLength: { params: 5, message: 'måste ange giltig e-post' },
            email: { params: true, message: 'måste ange giltig e-post' }
        }),
        termsAccepted: ko.observable(false),

        message: ko.observable(""),
        activate: function () {
            
        },
        onBackClick: function () {
            return true;
        },
        onClick: function () {

            var result = ko.validation.group(this, { deep: false });
            var valid = this.isValid();

            if (!this.termsAccepted()) {
                if (valid) {
                    app.showMessage('Du måste godkänna villkoren.');
                    return false;
                }
            }

            if (valid) {
                var data = {
                    username: this.username(),
                    password: this.password(),
                    email: this.email()
                };

                brilliance.signup3 = data;

                var allData = {
                    amMan: brilliance.signup1.amMan,
                    amWoman: brilliance.signup1.amWoman,
                    wantMan: brilliance.signup1.wantMan,
                    wantWoman: brilliance.signup1.wantWoman,

                    country: brilliance.signup2.country,
                    postalCode: brilliance.signup2.postalCode,
                    birthYear: brilliance.signup2.birthYear,

                    username: brilliance.signup3.username,
                    password: brilliance.signup3.password,
                    email: brilliance.signup3.email
                }
                var that = this;

                http.post(brilliance.appbaseurl() + "/Mobile/AppAccount/SignupSubmit", allData, '')
                    .then(function (response, textStatus) {
                        
                        that.message(response.Message);
                        //                  window.location.href = '';
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        that.message(jqXHR.responseJSON.Message);
                    });

                return true;
            } else {
                result.showAllMessages();
                return false;
            }
        }
    };
});



