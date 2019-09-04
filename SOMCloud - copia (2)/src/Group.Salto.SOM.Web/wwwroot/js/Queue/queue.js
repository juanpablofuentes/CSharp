var app = app || {};
app.queue = app.queue || {};
app.queue.main = app.queue.main || {};

app.queue.main = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterQueue").toggle();
    };

    var clearFilterFields = function (e) {
        $('#queueFilterName').val('');
        $('#queueFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName !== $('#queueFilterName').val() || initalDescription !== $('#queueFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);

        initialName = $('#queueFilterName').val();
        initalDescription = $('#queueFilterDescription').val();
    }

    return {
        Init: init
    };
})();