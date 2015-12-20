define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app'], function (router, http, ko, app) {
    "use strict";

    return {
        name: ko.observable(),
        heads: ko.observableArray(),
        buildUrl: function (id) {
            var token = localStorage.getItem("x-brilliance-token");
            return brilliance.appbaseurl() + "/Mobile/AppPicture/MainPictureDataFor/" + id + "?token=" + token;
        },
        rowClick: function (row) {
            window.location.href = '#conversation/' + row.OtherSurveyId;
        },
        activate: function () {

            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            http.get(brilliance.appbaseurl() + "/Mobile/AppContactivity/GetConversationHeads", '', { 'x-brilliance-token': token })
                .then(function (response, textStatus) {
                    that.heads(response.Heads);
                }).fail(brilliance.handleErrors);
        }

        //http.remove(brilliance.appbaseurl() + "/Mobile/AppContactivity/DeleteFavourite", data, { 'x-brilliance-token': token })
        //    .then(function (response, textStatus) {
        //       that.newMessageBody(response.Message);
        //        //that.otherSurveyName(response.Conversation.OtherSurveyName);

        //    }).fail(brilliance.handleErrors);
    };
});