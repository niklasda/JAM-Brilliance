define(['plugins/router', 'knockout'], function (router, ko) {
    "use strict";

    return {
        router: router,
        validToken: ko.observable(false)
            /*function () {
            var token = localStorage.getItem("x-brilliance-token");
            if (token && token.length === 36) {
                return true;
            }

            return false;
        }*/,
        activate: function () {
            $(document).on("mobileinit", function () {
                $.mobile.loader.prototype.options.text = "";
                //$.mobile.loader.prototype.options.textOnly = true;
                //$.mobile.loader.prototype.options.textVisible = false;
                //$.mobile.loader.prototype.options.theme = "a";
                //$.mobile.loader.prototype.options.html = "";
            });

            var token = localStorage.getItem("x-brilliance-token");
            if (token && token.length === 36) {
                this.validToken(true);
                router.map([
                    { route: ['', 'home'], title: 'Start', moduleId: 'viewmodels/home', nav: false },
                    { route: 'terms', title: 'Villkor', moduleId: 'viewmodels/terms', nav: false },
                    { route: 'searching', title: 'Sök', moduleId: 'viewmodels/searching', nav: true },
                    { route: 'hit/(:id)', title: 'Träff', moduleId: 'viewmodels/hit', nav: false },
                    { route: 'contact/(:id)', title: 'Kontakt', moduleId: 'viewmodels/contact', nav: false },
                    { route: 'mypage', title: 'Min sida', moduleId: 'viewmodels/mypage', nav: true },
                    { route: 'settings', title: 'Inställningar', moduleId: 'viewmodels/settings', nav: true },
                    { route: 'conversations', title: 'Kontakter', moduleId: 'viewmodels/conversations', nav: true },
                    { route: 'conversation/(:id)', title: 'Konversation', moduleId: 'viewmodels/conversation', nav: false },
                    { route: 'signup1', title: 'Anmälan', moduleId: 'viewmodels/signup1', nav: false },
                    { route: 'signup2', title: 'Anmälan Steg 2', moduleId: 'viewmodels/signup2', nav: false },
                    { route: 'signup3', title: 'Anmälan Steg 3', moduleId: 'viewmodels/signup3', nav: false },
                    { route: 'signup4', title: 'Anmälan Klar', moduleId: 'viewmodels/signup4', nav: false },
                    { route: 'login', title: 'Logga in', moduleId: 'viewmodels/login', nav: false },
                    { route: 'logout/(:action)', title: 'Logga ut', moduleId: 'viewmodels/logout', nav: false }
                ]).buildNavigationModel();
            } else {
                this.validToken(false);
                router.map([
                    { route: ['', 'home'], title: 'Start', moduleId: 'viewmodels/home', nav: false },
                    { route: 'terms', title: 'Villkor', moduleId: 'viewmodels/terms', nav: false },
                    { route: 'searching', title: 'Sök', moduleId: 'viewmodels/searching', nav: false },
                    { route: 'hit/(:id)', title: 'Träff', moduleId: 'viewmodels/hit', nav: false },
                    { route: 'contact/(:id)', title: 'Kontakt', moduleId: 'viewmodels/contact', nav: false },
                    { route: 'mypage', title: 'Min sida', moduleId: 'viewmodels/mypage', nav: false },
                    { route: 'settings', title: 'Inställningar', moduleId: 'viewmodels/settings', nav: false },
                    { route: 'conversations', title: 'Kontakter', moduleId: 'viewmodels/conversations', nav: false },
                    { route: 'conversation/(:id)', title: 'Konversation', moduleId: 'viewmodels/conversation', nav: false },
                    { route: 'signup1', title: 'Anmälan', moduleId: 'viewmodels/signup1', nav: false },
                    { route: 'signup2', title: 'Anmälan Steg 2', moduleId: 'viewmodels/signup2', nav: false },
                    { route: 'signup3', title: 'Anmälan Steg 3', moduleId: 'viewmodels/signup3', nav: false },
                    { route: 'signup4', title: 'Anmälan Klar', moduleId: 'viewmodels/signup4', nav: false },
                    { route: 'login', title: 'Logga in', moduleId: 'viewmodels/login', nav: false },
                    { route: 'logout/(:action)', title: 'Logga ut', moduleId: 'viewmodels/logout', nav: false }
                ]).buildNavigationModel();
            }
            return router.activate();
        }
    };
});