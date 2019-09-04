var app = app || {};
app.people = app.people || {};

app.people = (function () {
    var options;
    var initialName = '';
    var initalIsActive = '';

    var init = function (_options) {
        options = _options;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterPeople").toggle();
    };

    var clearFilterFields = function (e) {
        $('#PeopleFilterName').val('');
        $('#PeopleFilterIsActive').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#PeopleFilterName').val() || initalIsActive !== $('#PeopleFilterIsActive').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);
        $('#exporttoexcel').on('click', exportToExcel);
        $('#exportalltoexcel').on('click', exportAllToExcel);

        initialName = $('#PeopleFilterName').val();
        initalIsActive = $('#PeopleFilterIsActive').val();
    }

    function exportToExcel() {
        $("#ExportAllToExcel").val("False");
        var form = $("#filter-form").html();
        GenerateExcelForm(options.urlExcel, form);
    }

    function exportAllToExcel() {
        $("#ExportAllToExcel").val("True");
        var form = $("#filter-form").html();
        GenerateExcelForm(options.urlExcel, form);
    }

    return {
        Init: init
    };
})();