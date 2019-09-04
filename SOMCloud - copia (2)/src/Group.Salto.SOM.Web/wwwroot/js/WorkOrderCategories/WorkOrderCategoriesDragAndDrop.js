var app = app || {};
app.WorkOrderCategoriesDragAndDrop = app.WorkOrderCategoriesDragAndDrop || {};

app.WorkOrderCategoriesDragAndDrop = (function () {

    var init = function () {
        doOnLoad();
    };

    var availableCalendars, appliedCalendars;

    function doOnLoad() {
        availableCalendars = new dhtmlXList({
            container: 'data_container1',
            template: "<i class='fa fa-arrows-alt'></i> #name#",
            drag: true
        });

        appliedCalendars = new dhtmlXList({
            container: 'data_container2',
            template: "<i class='fa fa-arrows-alt'></i> #name#",
            drag: true
        });

        availableCalendars.attachEvent("onAfterDrop", function (context, ev) {
            saveData();
        });

        appliedCalendars.attachEvent("onAfterDrop", function (context, ev) {
            saveData();
        });
    }

    function addData(data) {
        data.forEach(function (calendar) {
            if (calendar.position === 0) {
                availableCalendars.add(calendar, calendar.position);
            } else {
                appliedCalendars.add(calendar, calendar.position);
            }
        });
        appliedCalendars.sort('#position#', 'asc');
    }

    function saveData() {
        $('#GenericEditViewModel_CategoryCalendarPreference').val(appliedCalendars.indexById(1) + 1);
        $('#GenericEditViewModel_ClientSiteCalendarPreference').val(appliedCalendars.indexById(2) + 1);
        $('#GenericEditViewModel_ProjectCalendarPreference').val(appliedCalendars.indexById(3) + 1);
        $('#GenericEditViewModel_SiteCalendarPreference').val(appliedCalendars.indexById(4) + 1);
    }

    return {
        Init: init,
        AddData: addData
    };
})();