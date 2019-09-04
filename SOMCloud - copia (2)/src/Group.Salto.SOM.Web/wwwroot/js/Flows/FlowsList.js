var app = app || {};
app.flows = app.flows || {};

app.flows = (function (event) {
    var initialSerialNumber = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterFlows").toggle();
    };

    var clearFilterFields = function (e) {
        $('#flowsFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialSerialNumber !== $('#flowsFilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $('#btnApplyFilter').on('click', (ClickFilterButton));

        initialSerialNumber = $('#flowsFilterName').val();
    }
    return {
        Init: init
    };
})();