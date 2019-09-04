var app = app || {};
app.warehouseMovements = app.warehouseMovements || {};

app.warehouseMovements = (function () {
    var initialFilterItems = '';

    var init = function (options) {
        initializeEvents();
        initializeItemsAutocomplete(options);
        toggleFilter();
    };

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["StartDate", "EndDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y-%m-%d %H:%i");
        datePicker.hideTime();
    };

    var initializeItemsAutocomplete = function (options) {
        var itemsAuto = new autoCompleteListSelector();
        itemsAuto.Init("#itemsFilterContainer",
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getItems,
                collectionProperty: 'Items',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 2,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return itemsAuto;
    }

    var toggleFilter = function (e) {
        $("#filterWarehouseMovements").toggle();
    };

    var clearFilterFields = function (e) {
        $('#FilterItems').val('');//filters
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialFilterItems !== $('#FilterItems').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $('#btnApplyFilter').on('click', (ClickFilterButton));
    }

    return {
        Init: init,
        CalendarLoad: calendarLoad
    };
})();