var app = app || {};
app.sla = app.sla || {};

app.RepetitionParameter = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterRepetitionParameter").toggle();
    };

    var clearFilterFields = function (e) {
        $('#ActionsFilterName').val('');
        $('#ActionsFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialDays != $('#ActionsFilterDays').val() || initalDescription != $('#ActionsFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialDays = $('#ActionsFilteDays').val();
        initalDescription = $('#ActionsFilterDescription').val();
    }

    return {
        Init: init
    }
})();