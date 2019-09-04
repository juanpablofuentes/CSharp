var app = app || {};
app.calendar = app.calendar || {};
app.calendar.main = app.calendar.main || {};
app.calendar.main = (function () {
    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $(app.calendar.constants.CalendarFilter).toggle();
    };

    var clearFilterFields = function (e) {
        $(app.calendar.constants.CalendarFilterNameFiledId).val('');
        $(app.calendar.constants.CalendarFilterDescriptionFieldId).val('');
        $('#btnApplyFilter').click();
    };

    function initializeEvents() {
        $(app.common.ui.constants.ButtonFilterClass).click(toggleFilter);
        $(app.common.ui.constants.ButtonClearFilterId).click(clearFilterFields);
    }

    return {
        Init: init
    }
})();