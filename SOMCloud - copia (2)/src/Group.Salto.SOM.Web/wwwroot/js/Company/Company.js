var app = app || {};
app.company = app.company || {};
app.company.main = app.company.main || {};

app.company.main = (function () {

    var initialName = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterCompany").toggle();
    };

    var clearFilterFields = function (e) {
        $('#CompanyFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#CompanyFilterName').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#CompanyFilterName').val();
    }

    return {
        Init: init
    };
})();
