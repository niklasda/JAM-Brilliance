define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app'], function (router, http, ko, app) {
    "use strict";

    return {
        age: ko.observable(),
        postalCode: ko.observable(),
        lookedupCity: ko.observable(""),
        myCity: ko.observable(),
        myPostalCode: ko.observable(),
        search: function () {
            //app.showMessage('Searching for ' + this.postalCode() + '!', 'Brilliance');
            brilliance.searchPostalCode = this.postalCode();
            window.location.href = '#searching';
        },
        searchClose: function () {
            //app.showMessage('Searching for ' + this.myPostalCode() + '!', 'Brilliance');
            brilliance.searchPostalCode = this.myPostalCode();
            window.location.href = '#searching';
        },
        activate: function () {
            
            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            this.postalCode.subscribe(function (newPostalCodeValue) {
                var city = '';
                if (newPostalCodeValue) {
                    newPostalCodeValue = newPostalCodeValue.replace(/\s+/g, '');
                    if (newPostalCodeValue.length === 5) {

                        var result = $.grep(postal, function (code) { return code.pnr === newPostalCodeValue; });
                        if (result && result.length === 1) {
                            city = result[0].city;
                            that.postalCode(newPostalCodeValue);
                        }
                    }
                }

                that.lookedupCity(city);
            });

            http.get(brilliance.appbaseurl() + "/Mobile/AppSurvey/GetMyShortSurvey", '', { 'x-brilliance-token': token })
            .then(function (response, textStatus) {
                that.myPostalCode(response.Survey.PostalCode);
                that.myCity(response.Survey.City);
                that.age(response.Survey.Age);

            }).fail(brilliance.handleErrors);
        }
    };
});