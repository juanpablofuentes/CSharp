var app = app || {};
app.rol = app.rol || {};

app.rol = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterRol").toggle();
    };

    var clearFilterFields = function (e) {
        $('#RolFilterName').val('');
        $('#RolFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName != $('#RolFilterName').val() || initalDescription != $('#RolFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#RolFilterName').val();
        initalDescription = $('#RolFilterDescription').val();
    }

    return {
        Init: init
    }
})();