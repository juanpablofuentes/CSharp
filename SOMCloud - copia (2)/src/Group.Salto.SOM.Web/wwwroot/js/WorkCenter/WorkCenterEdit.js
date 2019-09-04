var app = app || {};
app.WorkCenterEdit = app.WorkCenterEdit || {};

app.WorkCenterEdit = (function () {

    var companyIdFrom = -1;

    var init = function (companyId) {
        companyIdFrom = companyId;
        initializeEvents();
    };

    function disableCompanyCombo()
    {
        if (companyIdFrom.companyId !== -1)
        {
            $('#CompanySelected').attr('disabled', 'disabled');
        }
    }

    function initializeEvents() {
        disableCompanyCombo();
    }

   function editWorkCenter(value, urlDetailCompany, urlIndexWorkCenter) {
        if (value != -1) {
            var link = urlDetailCompany;
            link = link.replace("companyIdFrom", value);
            window.location.href = link;
        }
        else {
            window.location.href = urlIndexWorkCenter;
        }
    };

    return {
        Init: init,
        EditWorkCenter: editWorkCenter
    };

})();