define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app', 'jquery', 'slick'], function (router, http, ko, app, jquery, slick) {
    "use strict";

    return {
        survey: ko.observable(),
        searchResults: [],
        buildUrl: function (id) {
            var token = localStorage.getItem("x-brilliance-token");
            return brilliance.appbaseurl() + "/Mobile/AppPicture/MainPictureDataFor/" + id + "?token=" + token;
        },
        //   rowClick: function (row) {
        //       window.location.href = '#conversation/' + row.SurveyId;
        //   },
        addFavourite: function (surveyId) {
            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            var data = {
                OtherSurveyId: surveyId
            };

            $("#prof_" + surveyId).fadeOut("fast", function () {
                $("#prof_" + surveyId).removeClass('fa-user-plus');
            });


            http.put(brilliance.appbaseurl() + "/Mobile/AppContactivity/PutNewFavourite", data, { 'x-brilliance-token': token })
                   .then(function (response, textStatus) {
                       $("#prof_" + surveyId).addClass('fa-check');
                       $("#prof_" + surveyId).fadeIn();
                   }).fail(function (jqXHR, textStatus, errorThrown) {
                       $("#prof_" + surveyId).addClass('fa-user-plus');
                       $("#prof_" + surveyId).fadeIn();
                   });
        },
        startConversation: function (surveyId) {
            window.location.href = '#conversation/' + surveyId;
        },
        showShortSurvey: function (surveyId) {
            window.location.href = '#hit/' + surveyId;
        },

        bindingComplete: function () {

            $("#carousel").slick({
                dots: true,
                speed: 500,
                mobileFirst: true,
                slidesToShow: 1,
                slidesToScroll: 1,
                infinite: false
            });

            this.loadAndShowSearchHits(0);

        },
        loadAndShowSearchHits: function (nbrOfExistingHits) {
            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            var postalCode = "" + nbrOfExistingHits;
            //if (!that.searchResults || that.searchResults.length === 0) {
            //    postalCode = "0";
            //} else {
            //    postalCode = "" + that.searchResults.length;
            //}
            //var postalCode = "";

            http.get(brilliance.appbaseurl() + "/Mobile/AppSearch/Search", 'postalCode=' + postalCode, { 'x-brilliance-token': token })
                .then(function (response, textStatus) {
                    that.searchResults = response.SearchResults;

                    setTimeout(function () { that.initCarousel(that) }, 1000);

                }).fail(brilliance.handleErrors);
        },
        initCarousel: function (that) {
            if (!that.searchResults || that.searchResults.length === 0) {
                $("#carousel").slick("slickRemove", 0);
                $("#carousel").slick("slickAdd", "<div><h3>Inga sökträffar</h3></div>");
            } else {
                var lastCount = $("#carousel").slick("getSlick").slideCount;
                $("#carousel").slick("slickRemove", lastCount - 1);


                that.searchResults.forEach(function (item) {

                    var mess = "<i id='mess_" + item.SurveyId + "' class='fa fa-1x fa-envelope-o' style='cursor: pointer; margin-left: 20px;'></i>";
                    var prof = "<i id='prof_" + item.SurveyId + "' class='fa fa-1x fa-user-plus' style='cursor: pointer; margin-left: 20px;'></i>";

                    var h3 = "<h3>" + item.Name + " " + mess + " " + prof + "</h3>";
                    var img = "<img id='pic_" + item.SurveyId + "' style='cursor: pointer;' width=\"95%\" title=\"\" src=\"" + that.buildUrl(item.SurveyId) + "\" />";

                    var below = "<h3>" + item.Name + ", " + item.Age + " år</h3>";

                    $("#carousel").slick("slickAdd", "<div>" + h3 + "<div>" + img + "</div>" + below + "</div>");

                    var vm = ko.dataFor($("#carousel")[0]);

                    $("#prof_" + item.SurveyId).on("click", function () {
                        vm.addFavourite(item.SurveyId);
                    });
                    $("#mess_" + item.SurveyId).on("click", function () {
                        vm.startConversation(item.SurveyId);
                    });
                    $("#pic_" + item.SurveyId).on("tap", function () {
                        vm.showShortSurvey(item.SurveyId);
                    });
                });

                $("#carousel").slick("slickAdd", "<div><h3>Inga fler sökresultat!</h3><div id='lastSearchHit'></div></div>");
                $("#carousel").on("afterChange", function (event, aslick, currentSlide) {
                    if (aslick.slideCount - 1 === currentSlide) {
                        //alert('sista träffen');
                        $("#lastSearchHit").text('laddar fler...');

                        that.loadAndShowSearchHits(aslick.slideCount - 1);
                    }
                });
            }
        }
    };
});