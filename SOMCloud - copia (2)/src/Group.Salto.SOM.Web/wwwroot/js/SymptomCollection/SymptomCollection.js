var app = app || {};
app.SymptomCollection = app.SymptomCollection || {};
app.SymptomCollection.main = app.SymptomCollection.main || {};

app.SymptomCollection.main = (function () {

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterSymptomCollection").toggle();
    };

    var clearFilterFields = function (e) {
        $('#SymptomCollectionFilterName').val('');
        $('#SymptomCollectionFilterElement').val('');
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