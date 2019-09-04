var app = app || {};
app.brands = app.brands || {};

app.brands = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterBrands").toggle();
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
        $('#navUrlDocBtn').on("click", navToUrl);

        initialName = $('#ActionsFilterName').val();
        initalDescription = $('#ActionsFilterDescription').val();
    }

    return {
        Init: init
    }
})();