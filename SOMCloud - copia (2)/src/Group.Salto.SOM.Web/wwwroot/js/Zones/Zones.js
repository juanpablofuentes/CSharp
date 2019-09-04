var app = app || {};
app.zones = app.zones || {};

app.zones = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterZones").toggle();
    };

    var clearFilterFields = function (e) {
        $('#zonesFilterName').val('');
        $('#zonesFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName != $('#zonesFilterName').val() || initalDescription != $('#zonesFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);

        initialName = $('#ActionsFilterName').val();
        initalDescription = $('#ActionsFilterDescription').val();
    }

    return {
        Init: init
    };
})();