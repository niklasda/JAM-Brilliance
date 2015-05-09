function shakeIt(elem) {
    "use strict";
    $(elem).effect('shake', { times: 2, distance: 5, direction: 'up' }, 1000);
}

function checkMessages(url) {
    "use strict";
    $.ajax({
        type: "POST",
        url: url,
        datatype: "json",
        success: function (data) {
            if ($("#MessagesLink").length && $("#MessagesCountTop").length &&
                    data === parseInt(data, 10)) {
                var originalData = $("#MessagesCountTop").html();

                if (data > 0) {
                    $('#MessagesCountTop').html(data);

                    if (originalData != data) {
                        setTimeout(function () {
                            shakeIt('#MessagesLink');
                        }, 500);
                    }
                } else {
                    $('#MessagesCount').html('&nbsp;&nbsp;');
                }

                setTimeout(function () {
                    checkMessages(url);
                }, 6000);
            }
        }
    });
}

function checkCounts(url) {
    "use strict";
    $.ajax({
        type: "POST",
        url: url,
        datatype: "json",
        success: function (data) {
            if ($("#MessagesCount").length &&
                    data.messagesCount === parseInt(data.messagesCount, 10) && data.messagesCount > 0) {
                $('#MessagesCount').html(data.messagesCount);
            } else {
                $('#MessagesCount').html("&nbsp;&nbsp;");
            }

            if ($("#VisitorsCount").length &&
                    data.visitorsCount === parseInt(data.visitorsCount, 10) && data.visitorsCount > 0) {
                $('#VisitorsCount').html(data.visitorsCount);
            } else {
                $('#VisitorsCount').html("&nbsp;&nbsp;");
            }

            if ($("#FansCount").length &&
                    data.fansCount === parseInt(data.fansCount, 10) && data.fansCount > 0) {
                $('#FansCount').html(data.fansCount);
            } else {
                $('#FansCount').html("&nbsp;&nbsp;");
            }

            if ($("#OnlineCount").length &&
                    data.onlineUsersCount === parseInt(data.onlineUsersCount, 10) && data.onlineUsersCount > 0) {
                $('#OnlineCount').html(data.onlineUsersCount);
            } else {
                $('#OnlineCount').html("&nbsp;&nbsp;");
            }

            setTimeout(function () { checkCounts(url); }, 7000);
        }
    });
}

function checkAdminCounts(url) {
    "use strict";
    $.ajax({
        type: "POST",
        url: url,
        datatype: "json",
        success: function (data) {
            if ($("#SupportMessagesCount").length &&
                    data.supportMessagesCount === parseInt(data.supportMessagesCount, 10) && data.supportMessagesCount > 0) {
                $('#SupportMessagesCount').html(data.supportMessagesCount);
            } else {
                $('#SupportMessagesCount').html("&nbsp;&nbsp;");
            }

            if ($("#AnonSupportMessagesCount").length &&
                    data.anonSupportMessagesCount === parseInt(data.anonSupportMessagesCount, 10) && data.anonSupportMessagesCount > 0) {
                $('#AnonSupportMessagesCount').html(data.anonSupportMessagesCount);
            } else {
                $('#AnonSupportMessagesCount').html("&nbsp;&nbsp;");
            }

            if ($("#PicturesWaitingCount").length &&
                    data.picturesWaitingCount === parseInt(data.picturesWaitingCount, 10) && data.picturesWaitingCount > 0) {
                $('#PicturesWaitingCount').html(data.picturesWaitingCount);
            } else {
                $('#PicturesWaitingCount').html("&nbsp;&nbsp;");
            }

            if ($("#AbuseReportsCount").length &&
                    data.abuseReportsCount === parseInt(data.abuseReportsCount, 10) && data.abuseReportsCount > 0) {
                $('#AbuseReportsCount').html(data.abuseReportsCount);
            } else {
                $('#AbuseReportsCount').html("&nbsp;&nbsp;");
            }

            setTimeout(function () { checkAdminCounts(url); }, 7000);
        }
    });
}