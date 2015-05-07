define(['plugins/router', 'plugins/http', 'durandal/app', 'knockout'], function (router, http, app, ko) {
    "use strict";

    return {
        amMan: ko.observable(true),
        amWoman: ko.observable(false),
        wantMan: ko.observable(false),
        wantWoman: ko.observable(true),
        activate: function () {
        },

        onClick: function () {

            var data = {
                amMan: this.amMan(),
                amWoman: this.amWoman(),
                wantMan: this.wantMan(),
                wantWoman: this.wantWoman()
            };

            brilliance.signup1 = data;

            return true;
        }
    };
});



