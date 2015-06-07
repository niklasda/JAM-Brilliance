requirejs.config({
    paths: {
        'text': '../Scripts/text',
        'durandal': '../Scripts/durandal',
        'plugins': '../Scripts/durandal/plugins',
        'transitions': '../Scripts/durandal/transitions',
        'knockout': '../Scripts/knockout-3.3.0',
        'knockout-validation': '../Scripts/knockout.validation',
        'bootstrap': '../Scripts/bootstrap',
        'jquery': '../Scripts/jquery-2.1.4',
        'jqm': '../Scripts/jquery.mobile-1.4.5',
        'jqui': '../Scripts/jquery-ui-1.11.4',
        'slick': '../slick/slick'
    }
});

define(['durandal/system', 'durandal/app', 'durandal/viewLocator'], function (system, app, viewLocator) {
    "use strict";

    system.debug(true);

    app.title = 'Brilliance';

    app.configurePlugins({
        router: true,
        dialog: true
    });

    app.start().then(function () {
        viewLocator.useConvention();
        app.setRoot('viewmodels/shell');
    });
});