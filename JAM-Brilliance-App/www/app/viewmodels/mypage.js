define(['plugins/router', 'plugins/http', 'knockout', 'durandal/app'], function (router, http, ko, app) {
    "use strict";

    return {
        errorMessage: ko.observable(),
        //survey: ko.observable(),
        name: ko.observable(),
        postalCode: ko.observable(),
        city: ko.observable(),
        year: ko.observable(),
        isMale: ko.observable(),
        age: ko.observable(),
        note1: ko.observable(),
        imgurl: ko.observable(),
        editable: ko.observable(false),
        newPostalCode: ko.observable(),
        newLookedupCity: ko.observable(""),

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

            var token = localStorage.getItem("x-brilliance-token");
            var that = this;

            http.get(brilliance.appbaseurl() + "/Mobile/AppSurvey/GetMyShortSurvey", '', { 'x-brilliance-token': token })
               .then(function (response, textStatus) {
                   that.name(response.Survey.Name);
                   that.postalCode(response.Survey.PostalCode);
                   that.city(response.Survey.City);
                   that.year(response.Survey.Year);
                   that.isMale(response.Survey.IsMale);
                   that.age(response.Survey.Age);
                   that.note1(response.Survey.Note1);
                   that.imgurl(brilliance.appbaseurl() + "/Mobile/AppPicture/MainPictureData/?token=" + token);

                   that.newPostalCode.subscribe(function (newPostalCodeValue) {
                       var city = '';
                       if (newPostalCodeValue) {
                           newPostalCodeValue = newPostalCodeValue.replace(/\s+/g, '');
                           if (newPostalCodeValue.length === 5) {
                               city = that.getCityFromPostalCode(newPostalCodeValue);
                           }

                           that.newPostalCode(newPostalCodeValue);
                       }

                       that.newLookedupCity(city);
                   });

               }).fail(function (jqXHR, textStatus, errorThrown) {
                   that.errorMessage(textStatus);
               });
        },
        edit: function () {
            this.editable(true);
        },
        save: function () {
            this.editable(false);
        },
        upload: function() {
            var options = new FileUploadOptions();
            options.fileKey = "file";
            options.fileName = imageURI.substr(imageURI.lastIndexOf('/') + 1);
            options.mimeType = "image/jpeg";

            var params = new Object();
            params.value1 = "test";
            params.value2 = "param";

            options.params = params;
            options.chunkedMode = false;

            var ft = new FileTransfer();
            ft.upload(imageURI, "http://yourdomain.com/upload.php", win, fail, options);
        }
    };
});