define(['plugins/router', 'plugins/http', 'durandal/app', 'knockout', 'knockout-validation'], function (router, http, app, ko, koval) {
    "use strict";

    return {
        countries: ko.observableArray(['Sverige']),
        selectedCountry: ko.observable("Sverige").extend({ required: true }),
        postalCode: ko.observable("").extend({
            required: { params: true, message: 'måste ange giltigt postnummer' },
            min: { params: 10000, message: 'måste ange giltigt postnummer' },
            minLength: { params: 5, message: 'måste ange giltigt postnummer' },
            max: { params: 99000, message: 'måste ange giltigt postnummer' },
            maxLength: { params: 5, message: 'måste ange giltigt postnummer' }
        }),
        birthYear: ko.observable("").extend({
            required: { params: true, message: 'måste ange giltigt födelseår' },
            min: { params: 1901, message: 'måste ange giltigt födelseår' },
            minLength: { params: 4, message: 'måste ange giltigt födelseår' },
            max: { params: new Date().getFullYear() - 19, message: 'måste ange giltigt födelseår' },
            maxLength: { params: 4, message: 'måste ange giltigt födelseår' }
        }),
        lookedupCity: ko.observable(""),

        getCityFromPostalCode: function (postalCode) {
            var city = '';
            if (postalCode) {
                postalCode = postalCode.replace(/\s+/g, '');
                if (postalCode.length === 5) {

                    var result = $.grep(postal, function (code) { return code.pnr === postalCode; });
                    if (result && result.length === 1) {
                        city = result[0].city;
                    }
                }
            }

            return city;
        },
        activate: function () {

            var that = this;
            this.postalCode.subscribe(function (newPostalCodeValue) {
                var city = '';
                if (newPostalCodeValue) {
                    newPostalCodeValue = newPostalCodeValue.replace(/\s+/g, '');
                    if (newPostalCodeValue.length === 5) {
                        city = that.getCityFromPostalCode(newPostalCodeValue);
                    }

                    that.postalCode(newPostalCodeValue);
                }

                that.lookedupCity(city);
            });

            this.birthYear.subscribe(function (newBirthYearValue) {
                if (newBirthYearValue) {
                    newBirthYearValue = newBirthYearValue.replace(/\s+/g, '');
                    that.birthYear(newBirthYearValue);
                }
            });
        },
        onBackClick: function () {
            return true;
        },
        onClick: function () {

            var result = ko.validation.group(this, { deep: false });
            var valid = result().length === 0;

            var city = this.getCityFromPostalCode(this.postalCode());

            if (!city || city.length === 0) {
                if (valid) {
                    app.showMessage('Ogiltigt postnummer.');
                    return false;
                }
            }

            if (valid) {
                var data = {
                    country: this.selectedCountry(),
                    postalCode: this.postalCode(),
                    birthYear: this.birthYear()
                }

                brilliance.signup2 = data;

                return true;
            } else {
               // app.showMessage(result());
                result.showAllMessages();
                return false;
            }
        }
    };
});
