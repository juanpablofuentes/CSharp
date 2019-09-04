var app = app || {};
app.subContract = app.subContract || {};
app.subContract.main = app.subContract.main || {};

app.subContract.main = (function () {

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterSubContracts").toggle();
    };

    var clearFilterFields = function (e) {
        $('#SubContractsFilterName').val('');
        $('#SubContractsFilterDescription').val('');
        $('#btnApplyFilter').click();
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
    }

    return {
        Init: init
    }
})();