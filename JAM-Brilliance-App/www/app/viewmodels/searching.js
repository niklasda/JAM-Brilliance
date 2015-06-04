define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app', 'jqm', 'slick'], function (router, http, ko, app, jqm, slick) {
    "use strict";

    return {
        survey: ko.observable(),
        //searchResults: ko.observableArray(),
        searchResults2: [],
        //carousel: ko.observable(true),
        buildUrl: function (id) {
            var token = localStorage.getItem("x-brilliance-token");
            return brilliance.appbaseurl() + "/Mobile/AppPicture/MainPictureDataFor/" + id + "?token=" + token;
        },
        rowClick: function (row) {
            window.location.href = '#conversation/' + row.SurveyId;
        },
        setFavourite: function (surveyId) {
            alert('favourite; ' + surveyId);
        },
        startConversation: function (surveyId) {
            window.location.href = '#conversation/' + surveyId;
        },
        showShortSurvey: function (surveyId) {
            window.location.href = '#hit/' + surveyId;
        },
        addContact: function (surveyId) {

        },
        bindingComplete: function () {

            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            var postalCode = "";

            http.get(brilliance.appbaseurl() + "/Mobile/AppSearch/Search", 'postalCode=' + postalCode, { 'x-brilliance-token': token })
            .then(function (response, textStatus) {
                //that.searchResults(response.SearchResults);
                that.searchResults2 = response.SearchResults;

                $("#carousel").slick({
                    dots: true,
                    speed: 500,
                    mobileFirst: true,
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    infinite: false
                });

                setTimeout(function () { that.initCarousel(that) }, 1000);


            }).fail(brilliance.handleErrors);

        },
        initCarousel: function (that) {
            $("#carousel").slick("slickRemove", 0);
            if (!that.searchResults2 || that.searchResults2.length === 0) {
                $("#carousel").slick("slickAdd", "<div><h3>Inga sökträffar</h3></div>");
            } else {

                that.searchResults2.forEach(function (item) {

                    //var favo = "<i id='favo_" + item.SurveyId + "' class='fa fa-1x fa-heart-o' style='cursor: pointer; margin-left: 20px;'></i>";
                    var mess = "<i id='mess_" + item.SurveyId + "' class='fa fa-1x fa-envelope-o' style='cursor: pointer; margin-left: 20px;'></i>";
                    var prof = "<i id='prof_" + item.SurveyId + "' class='fa fa-1x fa-user-md' style='cursor: pointer; margin-left: 20px;'></i>";

                    var h3 = "<h3>" + item.Name + " " + mess + " " + prof + "</h3>";
                    var img = "<img id='pic_" + item.SurveyId + "' style='cursor: pointer;' width=\"95%\" title=\"\" src=\"" + that.buildUrl(item.SurveyId) + "\" />";

                    var below = "<h3>" + item.Name +", " + item.Age + " år</h3>";

                    $("#carousel").slick("slickAdd", "<div>" + h3 + "<div>" + img + "</div>" + below + "</div>");

                    var vm = ko.dataFor($("#carousel")[0]);

                    $("#favo_" + item.SurveyId).on("click", function () {
                        vm.setFavourite(item.SurveyId);
                    });
                    $("#mess_" + item.SurveyId).on("click", function () {
                        vm.startConversation(item.SurveyId);
                    });
                    $("#prof_" + item.SurveyId).on("click", function () {
                        vm.showShortSurvey(item.SurveyId);
                    });
                    $("#pic_" + item.SurveyId).on("tap", function () {
                        vm.showShortSurvey(item.SurveyId);
                    });
                });

                $("#carousel").slick("slickAdd", "<div><h3>Inga fler sökresultat!</h3><div id='lastSearchHit'></div></div>");
                $("#carousel").on("afterChange", function (event, aslick, currentSlide) {
                    if (aslick.slideCount - 1 === currentSlide) {
                        alert('sista träffen');
                        $("#lastSearchHit").text('laddar fler...');
                    }
                });
            }
        }
    };
});