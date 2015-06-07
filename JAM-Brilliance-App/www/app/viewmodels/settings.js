define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app', 'jqm', 'jqui'], function (router, http, ko, app, jqm, jqui) {
    "use strict";

    return {
        errorMessage: ko.observable(),
        
        activate: function () {

            var token = localStorage.getItem("x-brilliance-token");
            
         
        },
        bindingComplete: function() {
            $("#slider-agerange").slider({
                range: true,
                min: 18,
                max: 85,
                values: [25, 45],
                slide: function (event, ui) {
                    $("#agerangetext").text($("#slider-agerange").slider("values", 0) + " -> " + $("#slider-agerange").slider("values", 1));

                }
            });

            $("#@Html.IdFor(model => model.AgeMin)").val($("#slider-agerange").slider("values", 0));
            $("#@Html.IdFor(model => model.AgeMax)").val($("#slider-agerange").slider("values", 1));

            $("#@Html.IdFor(model => model.HeightMin)").val($("#slider-heightrange").slider("values", 0));
            $("#@Html.IdFor(model => model.HeightMax)").val($("#slider-heightrange").slider("values", 1));

            $("#slider-distancerange").slider({
                range: true,
                min: 18,
                max: 85,
                values: [25, 45],
                slide: function (event, ui) {
                    $("#distancerangetext").text($("#slider-distancerange").slider("values", 0) + " -> " + $("#slider-distancerange").slider("values", 1));

                }
            });
        }
    };
});