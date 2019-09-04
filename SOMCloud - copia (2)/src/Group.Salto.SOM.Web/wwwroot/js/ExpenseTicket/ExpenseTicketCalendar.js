var app = app || {};
app.expenseticketcalendar = app.expenseticketcalendar || {};

app.expenseticketcalendar = (function () {
    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["InitialDate", "FinalDate"]);
        datePicker.showTime();
        datePicker.loadUserLanguage(cultureInfo);
        switch (cultureInfo.toLowerCase()) {
            case "es":
                datePicker.setDateFormat("%d/%m/%Y %H:%i");
                break;
            case "ca":
                datePicker.setDateFormat("%d/%m/%Y %H:%i");
                break;
            default:
                datePicker.setDateFormat("%m.%d.%Y %H:%i");
                break;
        }
    };

    return {
        CalendarLoad: calendarLoad
    };
})();