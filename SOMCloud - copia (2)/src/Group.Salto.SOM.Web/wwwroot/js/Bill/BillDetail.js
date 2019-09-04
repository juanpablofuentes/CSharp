var app = app || {};
app.bill = app.bill || {};
app.bill.details = app.bill.details || {};

app.bill.details = (function () {
    
    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["FilterStartDate", "FilterEndDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y/%m/%d");
        datePicker.hideTime();
    };
    return {
        CalendarLoad: calendarLoad
    };
})();