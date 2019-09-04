var app = app || {};
app.closureCode = app.closureCode || {};
app.closureCode.main = app.closureCode.main || {};

app.closureCode.main = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterClosureCode").toggle();
    };

    var clearFilterFields = function (e) {
        $('#closureCodeFilterName').val('');
        $('#closureCodeFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName !== $('#closureCodeFilterName').val() || initalDescription !== $('#closureCodeFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);

        initialName = $('#closureCodeFilterName').val();
        initalDescription = $('#closureCodeFilterDescription').val();
    }

    return {
        Init: init
    };
})();