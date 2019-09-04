var app = app || {};
app.WorkOrderCategories = app.WorkOrderCategories || {};
app.WorkOrderCategories.constants = app.WorkOrderCategories.constants || {};
app.WorkOrderCategories.constants = (function () {
    var peopleTabClass = '.WOCategory-tab';
    var calendarTabList = 'nav-calendar-li';
    
    return {
        PeopleTabClass: peopleTabClass,
        CalendarTabList: calendarTabList
    };
})();