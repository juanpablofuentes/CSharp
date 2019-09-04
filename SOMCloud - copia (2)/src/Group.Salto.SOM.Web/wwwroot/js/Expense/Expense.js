var app = app || {};
app.expense = app.expense || {};

app.expense = (function () {

    var initialAmount = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterExpense").toggle();
    };

    var clearFilterFields = function (e) {
        $('#ActionsFilterAmount').val('');
        $('#ActionsFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName != $('#ActionsFilterAmount').val() || initalDescription != $('#ActionsFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);
        $('#navUrlDocBtn').on("click", navToUrl);

        initialAmount = $('#ActionsFilterAmount').val();
        initalDescription = $('#ActionsFilterDescription').val();
    }

    return {
        Init: init
    }
})();