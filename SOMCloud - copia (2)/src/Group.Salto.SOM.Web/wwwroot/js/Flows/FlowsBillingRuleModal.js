var app = app || {};
app.billingRuleModal = app.billingRuleModal || {};

app.billingRuleModal = (function (event) {    
    var getErpSystemsUrl, postCreateBillingRuleUrl, postUpdateBillingRuleUrl = '';

    var init = function (options) {
        getErpSystemsUrl = options.getErpSystemsUrl;
        postCreateBillingRuleUrl = options.postCreateBillingRuleUrl;
        postUpdateBillingRuleUrl = options.postUpdateBillingRuleUrl;
    };

    var openBillingRuleModal = function (rule) {
        $('#billingrule-rule-condition').empty();
        $('#billingrule-rule-erpsystems').empty();

        loadBillingRuleValues(rule);
    }

    var loadBillingRuleValues = function (rule) {
        var getAllErpSystems = apiCall(getErpSystemsUrl, 'GET', 'json');

        getAllErpSystems.done(function (systems) {
            setBillingRuleModalValues(systems, rule);
        });
    };

    var setBillingRuleModalValues = function (systems, rule) {
        app.common.ui.LoadSelectorKeyValue('billingrule-rule-erpsystems', systems, 'id', 'name', false);

        $('#billingrule-rule-id').val(rule.id);
        $('#billingrule-rule-task').val(rule.task);
        $('#billingrule-rule-condition').val(rule.condition);
        if (rule.erp > 0) {
            $('#billingrule-rule-erpsystems').val(rule.erp).change();
        }
        $('#billingrule-rule-erpsystems').change();

        if (rule.type ==='edit')
            $('#billing-rule-submit').bind('click', postUpdateModalBillingRuleData);          
        else
            $('#billing-rule-submit').bind('click', postCreateModalBillingRuleData);     
    }
    
    var postUpdateModalBillingRuleData = function (ev) {
        ev.preventDefault();

        var billingRuleDto = getModalData();

        $.post(postUpdateBillingRuleUrl, billingRuleDto).done(function (data) {
            var billingruletabId = '#nav-billingrules-' + CurrentTaskId;
            $('a[href="' + billingruletabId + '"]').click();
        });
    };

    var postCreateModalBillingRuleData = function (ev) {
        ev.preventDefault();

        var billingRuleDto = getModalData();

        $.post(postCreateBillingRuleUrl, billingRuleDto).done(function (data) {
            var billingruletabId = '#nav-billingrules-' + CurrentTaskId;
            $('a[href="' + billingruletabId + '"]').click();
        });
    }

    function getModalData() {
        var dto = {
            'Id': $('#billingrule-rule-id').val(),
            'TaskId': $('#billingrule-rule-task').val(),
            'Condition': $('#billingrule-rule-condition').val(),
            'ErpSystemInstance.Name': $('#billingrule-rule-erpsystems :selected').text(),
            'ErpSystemInstance.Id': $('#billingrule-rule-erpsystems :selected').val(),
        };
        return dto;
    }

    return {
        Init: init,
        PostCreateModalBillingRuleData: postCreateModalBillingRuleData,
        PostUpdateModalBillingRuleData: postUpdateModalBillingRuleData,
        OpenBillingRuleModal: openBillingRuleModal
    };
})();