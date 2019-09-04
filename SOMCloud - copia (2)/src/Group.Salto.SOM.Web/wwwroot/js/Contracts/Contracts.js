var app = app || {};
app.Contracts = app.Contracts || {};

app.Contracts = (function () {
    var initialObject = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $('span', this).toggleClass('fa fa-row-up fa fa-row-down');
    };

    var clearFilterFields = function (e) {
        $('#ContractsFilterObject').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialObject != $('#ContractsFilterObject').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialObject = $('#ContractsFilterObject').val();
    }

    return {
        Init: init
    }
})();