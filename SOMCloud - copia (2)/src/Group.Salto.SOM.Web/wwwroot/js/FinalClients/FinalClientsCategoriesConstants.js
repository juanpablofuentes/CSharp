var app = app || {};
app.finalclients = app.finalclients || {};
app.finalclients.constants = app.finalclients.constants || {};
app.finalclients.constants = (function () {
    var finalClientsTabClass = '.finalClient-tab';
    var calendarTabList = 'nav-calendar-li';
    $('#side-menu').removeClass('d-none');

    return {
        FinalClientsTabClass: finalClientsTabClass,
        CalendarTabList: calendarTabList
    };
})();