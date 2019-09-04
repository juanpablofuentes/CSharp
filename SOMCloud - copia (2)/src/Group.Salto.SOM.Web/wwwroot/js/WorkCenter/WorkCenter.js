var app = app || {};
app.workcenter = app.workcenter || {};

app.workcenter = (function () {

    var initialName = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterWorkCenter").toggle();
    };

    var clearFilterFields = function (e) {
        $('#WorkCenterFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName !== $('#WorkCenterFilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);
        initialName = $('#WorkCenterFilterName').val();
    }

    return {
        Init: init
    };
})();