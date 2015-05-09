$(document).ready(function () {
    "use strict";

    $("td[data-action='highlightCell']").css('cursor', 'pointer');

    $("td[data-action='highlightCell']").hover(function () {
        $(this).siblings("td[data-action='highlightCell']").andSelf().css('background', '#F5FFDB');
    }, function () {
        $(this).siblings("td[data-action='highlightCell']").andSelf().css('background', '');
    });

    $("td[data-action='highlightCell']").click(function () {
        document.location.href = $(this).parent().data('url');
    });
});