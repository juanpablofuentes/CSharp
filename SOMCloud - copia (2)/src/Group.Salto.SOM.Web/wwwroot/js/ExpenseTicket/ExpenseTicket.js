var app = app || {};
app.expenseticket = app.expenseticket || {};

app.expenseticket = (function () {

    var initialAmount = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterExpenseTicket").toggle();
    };

    var clearFilterFields = function (e) {
        $('#ActionsFilterAmount').val('');
        $('#ActionsFilterDescription').val('');
        $('#InitialDate').val('');
        $('#FinalDate').val('');
        $('#peopleContainer').empty();
        $('#statusContainer').empty();
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialAmount != $('#ActionsFilterAmount').val() || initalDescription != $('#ActionsFilterDescription').val()) {
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
    };
})();