var app = app || {};
app.rol = app.roltenant || {};

app.roltenant = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterRolTenant").toggle();
    };

    var clearFilterFields = function (e) {
        $('#RolTenantFilterName').val('');
        $('#RolTenantFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#RolTenantFilterName').val() || initalDescription !== $('#RolTenantFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#RolTenantFilterName').val();
        initalDescription = $('#RolTenantFilterDescription').val();
    }

    return {
        Init: init
    };
})();