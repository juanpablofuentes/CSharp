var app = app || {};
app.ContractDetail = app.ContractDetail || {};

app.ContractDetail = (function () {

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["StartDate", "EndDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y-%m-%d %H:%i");
        datePicker.hideTime();
    };

    return {
        CalendarLoad: calendarLoad
    };
})();