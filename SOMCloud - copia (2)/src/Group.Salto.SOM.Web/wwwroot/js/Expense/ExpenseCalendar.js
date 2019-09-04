var app = app || {};
app.expensecalendar = app.expensecalendar || {};

app.expensecalendar = (function () {

    var init = function () {
        calendarLoad();
        $('#inputGroupFile').on('change', function () {
            $('.custom-file-label').html($('#inputGroupFile').val().split('\\').slice(-1)[0]);
        });
    };

    function calendarLoad() {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["Date"]);
        datePicker.showTime();
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, true), "%Y-%m-%d %H:%i");
    }

    return {
        Init: init
    };
})();