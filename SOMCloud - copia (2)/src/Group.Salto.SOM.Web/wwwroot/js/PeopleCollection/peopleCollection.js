var app = app || {};
app.peopleCollection = app.peopleCollection || {};
app.peopleCollection.main = app.peopleCollection.main || {};

app.peopleCollection.main = (function () {

    var initialName = '';
    var initalDescription = '';

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterPeopleCollection").toggle();
    };

    var clearFilterFields = function (e) {
        $('#peopleCollectionFilterName').val('');
        $('#peopleCollectionFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function (e) {
        if (initialName !== $('#peopleCollectionFilterName').val() || initalDescription !== $('#peopleCollectionFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $('#btnApplyFilter').click(clickFilterButton);

        initialName = $('#workOrderStatusFilterName').val();
        initalDescription = $('#workOrderStatusFilterDescription').val();
    }

    return {
        Init: init
    };
})();