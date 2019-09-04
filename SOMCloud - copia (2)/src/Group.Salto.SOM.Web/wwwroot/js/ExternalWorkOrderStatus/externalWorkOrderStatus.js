var app = app || {};
app.externalWorkOrderStatus = app.externalWorkOrderStatus || {};
app.externalWorkOrderStatus.main = app.externalWorkOrderStatus.main || {};

app.externalWorkOrderStatus.main = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterExternalWorkOrderStatus").toggle();
    };

    var clearFilterFields = function (e) {
        $('#externalWorkOrderStatusFilterName').val('');
        $('#externalWorkOrderStatusFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName !== $('#externalWorkOrderStatusFilterName').val() || initalDescription !== $('#externalWorkOrderStatusFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);

        initialName = $('#externalWorkOrderStatusFilterName').val();
        initalDescription = $('#externalWorkOrderStatusFilterDescription').val();
    }

    return {
        Init: init
    };
})();