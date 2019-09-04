var app = app || {};
app.Symptom = app.Symptom || {};
app.Symptom.main = app.Symptom.main || {};

app.Symptom.main = (function () {

    var init = function () {
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterSymptom").toggle();
    };

    var clearFilterFields = function (e) {
        $('#SymptomFilterName').val('');
        $('#SymptomFilterDescription').val('');
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