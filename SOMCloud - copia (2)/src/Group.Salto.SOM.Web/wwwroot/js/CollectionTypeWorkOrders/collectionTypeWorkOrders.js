var app = app || {};
app.collectionTypeWorkOrders = app.collectionTypeWorkOrders || {};

app.collectionTypeWorkOrders = (function (event) {
    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterCollectionTypeWorkOrders").toggle();
    };

    var clearFilterFields = function (e) {
        $('#collectionTypeWorkOrdersFilterName').val('');
        $('#collectionTypeWorkOrdersFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#collectionTypeWorkOrdersFilterName').val() || initalDescription !== $('#collectionTypeWorkOrdersFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#collectionTypeWorkOrdersFilterName').val();
        initalDescription = $('#collectionTypeWorkOrdersFilterDescription').val();
    }

    return {
        Init: init
    };
})();