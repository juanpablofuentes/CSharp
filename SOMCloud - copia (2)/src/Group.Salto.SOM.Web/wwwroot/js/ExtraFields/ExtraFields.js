var app = app || {};
app.extraFields = app.extraFields || {};

app.extraFields = (function (event) {
    var initialName = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterExtraFields").toggle();
    };

    var clearFilterFields = function (e) {
        $('#extraFieldsFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#extraFieldsFilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $('#btnApplyFilter').on('click', (ClickFilterButton));

        initialName = $('#extraFieldsFilterName').val();
    }

    return {
        Init: init
    };
})();