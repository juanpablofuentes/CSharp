var app = app || {};
app.expensetype = app.expensetype || {};

app.expensetype = (function () {

    var initialType = '';
    

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterExpenseType").toggle();
    };

    var clearFilterFields = function (e) {
        $('#ActionsFilterType').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialType != $('#ActionsFilterType').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);
        $('#navUrlDocBtn').on("click", navToUrl);

        initialName = $('#ActionsFilterType').val();
        
    }

    return {
        Init: init
    }
})();