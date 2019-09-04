var app = app || {};
app.module = app.module || {};

app.module = (function () {

    var initialName = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $('span', this).toggleClass('fa fa-row-up fa fa-row-down');
    };

    var clearFilterFields = function (e) {
        $('#ModuleFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#ModuleFilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#ModuleFilterName').val();
    }

    return {
        Init: init
    };
})();