var app = app || {};
app.Bill = app.Bill || {};

app.Bill = (function (event) {
    var initialFilterName = '';

    var init = function (options) {
        initializeEvents(options);
        initializeProjectAutocomplete(options.Project);
        initializeItemAutocomplete(options.Item)
        initializeStatusAutocomplete(options.Status)
    };

    var toggleFilter = function () {
        $("#filterBill").toggle();
    };

    var clearFilterFields = function (e) {

        var cultureInfo = getCookie("culture-code").toLowerCase();

        $('#FilterWOId').val('');
        $('#FilterStatus').val('');
        $('#FilterProjectSerie').val('');
        $('#FilterItemId').val('');
        $('#FilterItemId').text('');
        $('#FilterInternalId').val('');
        $('#FilterProjSerie').val('');
        $('#ProjectContainer').val('');
        $('#ProjectContainer').text('');
        var StartEnd = date(cultureInfo);
        var startDate = StartEnd[0];
        var endDate = StartEnd[1];
        $('#FilterStartDate').val(startDate);
        $('#FilterEndDate').val(endDate);
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialFilterName !== $('#FilterName').val()) {
            $('#pager-page').val(1);
        }
        if ($('#ProjectContainer').val() == 0) {
            $('#ProjectContainer').val('');
        }
        if ($('#FilterItemId').val() == 0) {
            $('#FilterItemId').val('');
        }
    };

    function date(dateTimeFormat) {
        var year = new Date().getFullYear().toString();
        var day = new Date().getDate();
        var numdays = new Date().getDay();
        var month = new Date().getMonth() + 1;
        var day1; var month1; var year1;
        var day2; var month2; var year2;
        var last_acmh = new Date(year, month, 0);
        var varlastDate_acmh = last_acmh.getDate();
        var lastday_acmh = last_acmh.getDay();
        var last_pasmh = new Date(year, month - 1, 0);
        var lastDate_pasmh = last_pasmh.getDate();
        var lastday_pasmh = last_pasmh.getDay();
        day1 = day;
        year1 = year;
        month1 = month - 1;
        if (month1 < 1) {
            month1 = 12;
            year1 = year - 1;
        }

        day2 = day;
        year2 = year;
        month2 = month;
        
        month1 = month1 < 10 ? "0" + month1 : month1;
        day1 = day1 < 10 ? "0" + day1 : day1;
        month2 = month2 < 10 ? "0" + month2 : month2;
        day2 = day2 < 10 ? "0" + day2 : day2;
        var startDate = dateTimeFormat !== "en" ? day1 + "/" + month1 + "/" + year1 : month1 + "." + day1 + "." + year1;
        var endDate = dateTimeFormat !== "en" ? day2 + "/" + month2 + "/" + year2 : month2 + "." + day2 + "." + year2;

        return [startDate, endDate];
    }
    
    function initializeEvents(options) {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $('#btnApplyFilter').on('click', (ClickFilterButton));


        var cultureInfo = getCookie("culture-code").toLowerCase();
        var stDate = new Date(options.SDate);
        var enDate = new Date(options.EDate);
        var day1 = stDate.getDate(); var month1 = stDate.getMonth()+1; var year1 = stDate.getFullYear();
        var day2 = enDate.getDate(); var month2 = enDate.getMonth()+1; var year2 = enDate.getFullYear();
        var startDate = cultureInfo !== "en" ? day1 + "/" + month1 + "/" + year1 : month1 + "." + day1 + "." + year1;
        var endDate = cultureInfo !== "en" ? day2 + "/" + month2 + "/" + year2 : month2 + "." + day2 + "." + year2;
        $('#FilterStartDate').val(startDate);
        $('#FilterEndDate').val(endDate);

    }

    function getProjectFilter(text) {
        var data = {
            Text: text,
            Selected: []
        };
        return data;
    }

    function initializeProjectAutocomplete(options) {
        var ProjectCombo = new autocomplete();
        ProjectCombo.Init("#ProjectContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.GetProjectAll,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getProjectFilter,
                ajaxMethodType: app.constants.Post
            });
        selectedProject = ProjectCombo;
        return ProjectCombo;
    }
    
    function initializeItemAutocomplete(options) {
        var ItemCombo = new autocomplete();
        ItemCombo.Init("#FilterItemId",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getItems,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedItem = ItemCombo;
        return ItemCombo;
    }

    function initializeStatusAutocomplete(options) {
        var StatusCombo = new autocomplete();
        StatusCombo.Init("#FilterStatus",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getStates,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 0,
                nColumns: false,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedStatus = StatusCombo;
        return StatusCombo;
    }

    return {
        Init: init
    };
})();