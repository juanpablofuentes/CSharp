var app = app || {};
app.peopleWork = app.peopleWork || {};
app.peopleWork.constants = app.peopleWork.constants || {};
app.peopleWork.constants = (function () {
    var peopleTabClass = '.people-tab';
    var calendarTabList = 'nav-calendar-li';
    
    return {
        PeopleTabClass: peopleTabClass,
        CalendarTabList: calendarTabList
    };
})();