var app = app || {};
app.action = app.action || {};

app.action = (function () {
    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $('span', this).toggleClass('fa fa-row-up fa fa-row-down');
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