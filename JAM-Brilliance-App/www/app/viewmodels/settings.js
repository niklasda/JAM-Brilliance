define(['plugins/router', 'plugins/http', 'plugins/dialog', 'knockout', 'durandal/app', 'jqm', 'jqui'], function (router, http, dlg, ko, app, jqm, jqui) {
    "use strict";

    return {
        errorMessage: ko.observable(),
        ageMin: ko.observable(25),
        ageMax: ko.observable(45),
        //distanceMin: ko.observable(0),
        distanceMax: ko.observable(45),
        activate: function () {

            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            http.get(brilliance.appbaseurl() + "/Mobile/AppSurvey/GetSurveySettings", '', { 'x-brilliance-token': token })
                .then(function (response, textStatus) {
                    that.ageMin(response.Settings.SearchAgeMin);
                    that.ageMax(response.Settings.SearchAgeMax);
                    that.distanceMax(response.Settings.SearchDistanceMax);
                }).fail(brilliance.handleErrors);
        },
        removeAccount: function () {
            var x = dlg.showMessage('Vill du verkligen ta bort ditt konto, din profil, dina meddelande och kontakter kommer att tas bort?', 'Bekräfta', ['Ja', 'Nej'], true)
                .then(function (res) {
                    if (res === 'Ja') {
                        window.location.href = '#logout/remove';
                    }
                    
                });
            return false;
        },
        compositionComplete: function () {

            var that = this;

            $("#slider-agerange").slider({
                range: true,
                min: 18,
                max: 85,
                values: [25, 45],
                slide: function (event, ui) {
                    
                    that.ageMin(ui.values[0]);
                    that.ageMax(ui.values[1]);
                }
            });

            $("#slider-distancerange").slider({
                range: false,
                min: 0,
                max: 85,
                values: [45],
                slide: function (event, ui) {
                    
                    //that.distanceMin(ui.values[0]);
                    that.distanceMax(ui.values[0]);
                }
            });
        }
    };
});