var app = app || {};
app.ProjectsDetails = app.ProjectsDetails || {};

app.ProjectsDetails = (function () {

    const counter = document.getElementById('CounterId');
    counter.addEventListener('change', function (e) {
        if (e.target.value == '') {
            e.target.value = 0
        }
    });

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["GenericDetailViewModel_StartDate", "GenericDetailViewModel_EndDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.hideTime();
    };

    return {
        CalendarLoad: calendarLoad
    };
})();