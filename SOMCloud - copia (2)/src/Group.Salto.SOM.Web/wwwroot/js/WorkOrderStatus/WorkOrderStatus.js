var app = app || {};
app.workOrderStatus = app.workOrderStatus || {};
app.workOrderStatus.main = app.workOrderStatus.main || {};

app.workOrderStatus.main = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterWorkOrderStatus").toggle();
    };

    var clearFilterFields = function (e) {
        $('#workOrderStatusFilterName').val('');
        $('#workOrderStatusFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName !== $('#workOrderStatusFilterName').val() || initalDescription !== $('#workOrderStatusFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);

        initialName = $('#workOrderStatusFilterName').val();
        initalDescription = $('#workOrderStatusFilterDescription').val();
    }

    return {
        Init: init
    };
})();