var app = app || {};
app.workcenterapi = app.workcenterapi || {};

app.workcenterapi = (function () {  
    var elementSelectedProp = '';
    var elementFilteredProp = '';
    var valueSelected = '';

    var successWorkCenterByCompany = function (result) {
        if (result) {
            app.common.ui.LoadSelector(elementFilteredProp, result);
            if (valueSelected !== '') {
                $('#' + elementFilteredProp).val(valueSelected);
            }
        }
    };

    var loadWorkCenterByCompany = function () {
        var companyId = $('#' + elementSelectedProp).val();
        var uri = app.config.Urls.getWorkCenterByCompany;
        if (companyId !== '') {
            var data = { QueryType: app.constants.QueryTypeWorkCenterFilterByCompany, QueryTypeParameters: { Value: companyId } };
            return $.ajax({
                url: uri,
                type: app.constants.Post,
                dataType: 'json',
                cache: false,
                data: data
            });
        }
    }; 

    var selectWorkCenterByCompany = function () {
        loadWorkCenterByCompany().done(function (data) { successWorkCenterByCompany(data); });
    };

    var init = function (options) {
        elementSelectedProp = options.elementSelected;
        elementFilteredProp = options.elementFiltered;
        valueSelected = options.valueSelected;

        selectWorkCenterByCompany();
        $('#' + elementSelectedProp).on(app.constants.ChangeEvent, selectWorkCenterByCompany);
    }; 

    return {
        Init: init
    };
})();