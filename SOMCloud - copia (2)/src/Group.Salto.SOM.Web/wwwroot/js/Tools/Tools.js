var app = app || {};
app.tools = app.tools || {};

app.tools = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterTools").toggle();
    };

    var clearFilterFields = function (e) {
        $('#toolsFilterName').val('');
        $('#toolsFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName != $('#toolsFilterName').val() || initalDescription != $('#toolsFilterDescription').val()) {
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