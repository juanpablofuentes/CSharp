var app = app || {};
app.peopleapi = app.peopleapi || {};

app.peopleapi = (function () {  
    var elementSelectedProp = '';
    var elementFilteredProp = '';
    var valueSelected = '';

    var successPeopleByCompany = function (result) {
        if (result) {
            app.common.ui.LoadSelector(elementFilteredProp, result);
            if (valueSelected !== '') {
                $('#' + elementFilteredProp).val(valueSelected);
            }
        }
    };

    var loadPeopleByCompany = function () {
        var companyId = $('#' + elementSelectedProp).val();
        var uri = app.config.Urls.getPeople;
        if (companyId !== '') {
            var data = { QueryType: app.constants.QueryTypePeopleFilterByCompany, QueryTypeParameters: { Value: companyId } };
            return $.ajax({
                url: uri,
                type: app.constants.Post,
                dataType: 'json',
                cache: false,
                data: data
            });
        }
    }; 

    var selectPeopleByCompany = function () {
        loadPeopleByCompany().done(function (data) { successPeopleByCompany(data); });
    };

    var init = function (options) {
        elementSelectedProp = options.elementSelected;
        elementFilteredProp = options.elementFiltered;
        valueSelected = options.valueSelected;

        selectPeopleByCompany();
        $('#' + elementSelectedProp).on(app.constants.ChangeEvent, selectPeopleByCompany);
    }; 

    return {
        Init: init
    };
})();