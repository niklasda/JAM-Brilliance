define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app', 'jqm', 'jqui'], function (router, http, ko, app, jqm, jqui) {
    "use strict";

    return {
        errorMessage: ko.observable(),
        ageMin: ko.observable(25),
        ageMax: ko.observable(45),
        distanceMin: ko.observable(0),
        distanceMax: ko.observable(45),
        activate: function () {

            var token = localStorage.getItem("x-brilliance-token");

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
                range: true,
                min: 0,
                max: 85,
                values: [0, 45],
                slide: function (event, ui) {
                    
                    that.distanceMin(ui.values[0]);
                    that.distanceMax(ui.values[1]);
                }
            });
        }
    };
});