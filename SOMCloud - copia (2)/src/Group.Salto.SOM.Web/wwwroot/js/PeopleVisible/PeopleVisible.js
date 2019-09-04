var app = app || {};
app.peopleVisible = app.peopleVisible || {};

app.peopleVisible = (function () {
    var clearFilterFields = function (e) {
        $('#PeopleVisibleFilterName').val('');
        knowledgeComboFilter.Clean();
        companyComboFilter.Clean();
        departmentAutoComplete.Clean();
        workCenterAutoComplete.Clean();
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var toggleFilter = function (e) {
        $("#filterVisiblePeople").toggle();
    };

    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
    }

    var init = function () {
        initializeEvents();
    };

    return {
        Init: init
    };
})();