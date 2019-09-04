var app = app || {};
app.customer = app.customer || {};

app.customer = (function () {

    var initialName = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $('span', this).toggleClass('fa fa-row-up fa fa-row-down');
    };

    var clearFilterFields = function (e) {
        $('#CustomerFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#CustomerFilterName').val()) {
            $('#pager-page').val(1);
        }
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#CustomerFilterName').val();
    }

    return {
        Init: init
    };
})();
