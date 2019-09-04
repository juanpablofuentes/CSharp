var app = app || {};
app.assetstatuses = app.assetstatuses || {};

app.assetstatuses = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterAssetStatuses").toggle();
    };

    var clearFilterFields = function (e) {
        $('#ActionsFilterName').val('');
        $('#ActionsFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName != $('#ActionsFilterName').val() || initalDescription != $('#ActionsFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#ActionsFilterName').val();
        initalDescription = $('#ActionsFilterDescription').val();
    }

    return {
        Init: init
    }
})();