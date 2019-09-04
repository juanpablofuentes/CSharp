var app = app || {};
app.client = app.client || {};

app.client = (function () {

    var initialCorporateName = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $('span', this).toggleClass('fa fa-row-up fa fa-row-down');
    };

    var clearFilterFields = function (e) {
        $('#ClientsFilterCorporateName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialCorporateName !== $('#ClientsFilterCorporateName').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialCorporateName = $('#ClientsFilterCorporateName').val();
    }

    return {
        Init: init
    };
})();
