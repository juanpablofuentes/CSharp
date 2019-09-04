var app = app || {};
app.billingrules = app.billingrules || {};

app.billingrules = (function (event) {
    var CurrentTaskId;
    var getBillingRulesByTaskUrl, deleteBillingRuleUrl, deleteBillingRuleItemUrl, txtReadMore, txtReadLess = '';
   
    var init = function (options) {
        getBillingRulesByTaskUrl = options.getBillingRulesByTaskUrl;
        deleteBillingRuleUrl = options.deleteBillingRuleUrl;
        deleteBillingRuleItemUrl = options.deleteBillingRuleItemUrl;
        txtReadMore = options.txtReadMore;
        txtReadLess = options.txtReadLess;
    };
    
    function billingRulesTabLoad(evt) {
        if (evt) {
            evt.preventDefault();
        }
        CurrentTaskId = $(this).data('taskid');
        loadBillingRulesByTask(CurrentTaskId);
    }

    function loadBillingRulesByTask(id) {
        showSpinner();
        $('div[id^="t-' + CurrentTaskId + '-br-"]').each(function () {
            $(this).remove();
        });
        var url = getBillingRulesByTaskUrl;
        var getBillingRules = apiCall(url, 'GET', 'json', { id: id });
        getBillingRules.done(function (res) {
            var cont = $('#container-billingrules-' + CurrentTaskId);
            loadBillingRules(res.data, cont);
            hideSpinner();
        }).fail(function (err) {
            hideSpinner();
        });
    }

    function readMore(selector) {
        $(selector).readmore({
            embedCSS: false,
            moreLink: '<div class="row"><div class="col text-center"><a href="#"><i class="fa fa-chevron-down"></i> ' + txtReadMore + '</a></div></div>',
            lessLink: '<div class="row"><div class="col text-center"><a href="#"><i class="fa fa-chevron-up"></i> ' + txtReadLess + '</a></div></div>',
            collapsedHeight: 125,
            speed: 500,
            afterToggle: function (trigger, element, expanded) {
                if (!expanded) {
                    $('html, body').animate({
                        scrollTop: $('#' + element[0].id).offset().top
                    }, 1000, "linear");
                }
            }
        });
    }

    function loadBillingRules(data, container) {
        var rulesEditId = '#t-' + CurrentTaskId + '-Rule-Edit';
        var rulesItemContainerId = '#container-billingruleitems-' + CurrentTaskId;

        var billingruleSectionId = '#t-' + CurrentTaskId + '-BillingRule';
        var billingruleitemSectionId = '#t-' + CurrentTaskId + '-BillingRuleItem';
        var ruleItemEditId = '#t-' + CurrentTaskId + '-RuleItem-Edit';
        var ruleConditionId = '#t-' + CurrentTaskId + '-BR-Condition';
        var ruleErpSystemId = '#t-' + CurrentTaskId + '-BR-ErpSystem';
        var itemNameId = '#t-' + CurrentTaskId + '-BRI-Name';
        var itemUnitsId = '#t-' + CurrentTaskId + '-BRI-Units';

        var addRuleId = '#br-rule-add-btn';
        var editRuleId = '#br-rule-edit-' + CurrentTaskId;
        var deleteRuleId = '#br-rule-delete-' + CurrentTaskId;
        var addRuleItemId = '#bri-item-add-btn';
        var editRuleItemId = '#bri-item-edit-' + CurrentTaskId;
        var deleteRuleItemId = '#bri-item-delete-' + CurrentTaskId;

        var billingruleAddBtn = container.find(addRuleId);
        var billingruleAddData = [{ key: 'data-task', value: CurrentTaskId }];
        addButtonData(billingruleAddBtn, billingruleAddData);
        billingruleAddBtn.on('click', function () {
            var rule = {
                type: 'create',
                id: 0,
                task: $(this).data('task')
            };

            $('#billingRuleModal').modal('show');
            app.billingRuleModal.OpenBillingRuleModal(rule);
        });
                  
        var billingruleTemplate = container.find(billingruleSectionId);
        var billingRulesArray = data;

        if (billingRulesArray.length == 0)
            billingruleTemplate.addClass('d-none');
        else {

            if (billingruleTemplate.hasClass('d-none'))
                billingruleTemplate.removeClass('d-none');
            
            for (var i = 0; i < billingRulesArray.length; i++) {
                var current = billingRulesArray[i];

                var billingruleSection = billingruleTemplate.clone();
                billingruleSection.attr('id', 't-' + CurrentTaskId + '-br-' + current.id);

                var billingruleFields = billingruleSection.find(rulesEditId);
                billingruleFields.attr('id', 't-' + CurrentTaskId + '-br-' + current.id + '-fields');


                var billingruleEditBtn = billingruleFields.find(editRuleId);
                var billingruleEditData = [
                    { key: 'data-ruleid', value: current.id },
                    { key: 'data-taskid', value: CurrentTaskId },
                    { key: 'data-condition', value: current.condition },
                    { key: 'data-erp', value: current.erpSystemInstance.id }];
                addButtonData(billingruleEditBtn, billingruleEditData);
                billingruleEditBtn.on('click', function () {
                    var rule = {
                        type: 'edit',
                        id: $(this).data('ruleid'),
                        task: $(this).data('taskid'),
                        condition: $(this).data('condition'),
                        erp: $(this).data('erp')                       
                    };

                    $('#billingRuleModal').modal('show');
                    app.billingRuleModal.OpenBillingRuleModal(rule);
                });

                var billingruleDeleteBtn = billingruleFields.find(deleteRuleId);
                var billingruleDeleteData = [{ key: 'data-ruleid', value: current.id }];
                addButtonData(billingruleDeleteBtn, billingruleDeleteData);
                billingruleDeleteBtn.on('click', function () {
                    showSpinner();

                    var ruleid = $(this).data('ruleid');
                    $("#confirmationDeleteFlows").modal("toggle");
                    $("#confirmationDeleteFlowsConfirmSave").on("click", function () {
                        deleteBillingRule(ruleid);
                        ruleid = null;
                        $("#confirmationDeleteFlows").modal("toggle");
                        $("#confirmationDeleteFlowsconfirmationDeleteFlows").off("click");
                    });

                    hideSpinner();                    
                });

                var billingruleFieldsCondition = billingruleFields.find(ruleConditionId);
                billingruleFieldsCondition.attr('id', 't-' + CurrentTaskId + '-br-' + + current.id + '-fields' + '-c');
                billingruleFieldsCondition.append(current.condition);

                var billingruleFieldsErp = billingruleFields.find(ruleErpSystemId);
                billingruleFieldsErp.attr('id', 't-' + CurrentTaskId + '-br-' + + current.id + '-fields' + '-e');
                billingruleFieldsErp.append(current.erpSystemInstance.name);

                var billingruleitemList = billingruleFields.find(rulesItemContainerId);
                var billingruleitemAddBtn = billingruleitemList.find(addRuleItemId);
                var billingruleitemAddData = [{ key: 'data-billingruleid', value: current.id }];
                addButtonData(billingruleitemAddBtn, billingruleitemAddData);
                billingruleitemAddBtn.on('click', function () {
                    var ruleitem = {
                        type: 'create',
                        billingRuleId: $(this).data('billingruleid'),
                        id: 0
                    };

                    $('#billingRuleItemModal').modal('show');
                    app.billingRuleItemModal.OpenBillingRuleItemModal(ruleitem);
                });

                var billingruleItemTemplate = billingruleitemList.find(billingruleitemSectionId);
                if (current.items.length == 0)
                    billingruleItemTemplate.addClass('d-none');
                else {
                    for (var z = 0; z < current.items.length; z++) {
                        var item = current.items[z];

                        var billingruleitemSection = billingruleItemTemplate.clone();
                        var billingruleitemFields = billingruleitemSection.find(ruleItemEditId);
                        billingruleitemFields.attr('id', 't-' + CurrentTaskId + '-bri-' + current.id + '-item-' + item.id);


                        var billingruleitemEditBtn = billingruleitemFields.find(editRuleItemId);
                        var billingruleitemEditData = [
                            { key: 'data-itemid', value: item.id },
                            { key: 'data-billingruleid', value: item.billingRuleId },
                            { key: 'data-units', value: item.units },
                            { key: 'data-billingitemid', value: item.item.id },
                            { key: 'data-billingitemname', value: item.item.name }];
                        addButtonData(billingruleitemEditBtn, billingruleitemEditData);
                        billingruleitemEditBtn.on('click', function () {
                            var ruleitem = {
                                type: 'edit',
                                billingRuleId: $(this).data('billingruleid'),
                                id: $(this).data('itemid'),
                                units: $(this).data('units'),
                                itemid: $(this).data('billingitemid'),
                                itemname: $(this).data('billingitemname')                              
                            };

                            $('#billingRuleItemModal').modal('show');
                            app.billingRuleItemModal.OpenBillingRuleItemModal(ruleitem);
                        });
                                                
                        var billingruleDeleteBtn = billingruleitemFields.find(deleteRuleItemId);
                        var billingruleDeleteData = [{ key: 'data-itemid', value: item.id }];
                        addButtonData(billingruleDeleteBtn, billingruleDeleteData);
                        billingruleDeleteBtn.on('click', function () {
                            showSpinner();

                            var itemid = $(this).data('itemid');
                            $("#confirmationDeleteFlows").modal("toggle");
                            $("#confirmationDeleteFlowsConfirmSave").on("click", function () {
                                deleteBillingRuleItem(itemid);
                                itemid = null;
                                $("#confirmationDeleteFlows").modal("toggle");
                                $("#confirmationDeleteFlowsconfirmationDeleteFlows").off("click");
                            });

                            hideSpinner();
                        });

                        var billingruleitemFieldsItem = billingruleitemFields.find(itemNameId);
                        billingruleitemFieldsItem.attr('id', 't-' + CurrentTaskId + '-bri-' + current.id + '-item-' + item.id + '-i');
                        billingruleitemFieldsItem.append(item.item.name);

                        var billingruleitemFieldsUnits = billingruleitemFields.find(itemUnitsId);
                        billingruleitemFieldsUnits.attr('id', 't-' + CurrentTaskId + '-bri-' + current.id + '-item-' + item.id + '-u');
                        billingruleitemFieldsUnits.append(item.units);

                        billingruleitemList.prepend(billingruleitemSection);
                    }
                    billingruleItemTemplate.remove();

                    if (current.items.length > 6)
                        billingruleitemList.addClass('read-more');  
                }

                container.prepend(billingruleSection);
            }
        }
        billingruleTemplate.addClass('d-none');
        container.data('hasdata', true);
        readMore('.read-more');
    }

    function deleteBillingRule(ruleId) {
        var url = deleteBillingRuleUrl;
        var deleteBillignRuleApi = apiCall(url, 'DELETE', 'json', { id: ruleId });
        deleteBillignRuleApi.done(function (res) {
            var billingruletabId = '#nav-billingrules-' + CurrentTaskId;
            $('a[href="' + billingruletabId + '"]').click();
        });
    }

    function deleteBillingRuleItem(itemId) {
        var url = deleteBillingRuleItemUrl;
        var deleteBillignRuleItemApi = apiCall(url, 'DELETE', 'json', { id: itemId });
        deleteBillignRuleItemApi.done(function (res) {
            var billingruletabId = '#nav-billingrules-' + CurrentTaskId;
            $('a[href="' + billingruletabId + '"]').click();
        });
    }

    function addButtonData(button, data) {
        for (var i in data) {
            button.attr(data[i].key, data[i].value)
        }
    }
     
    return {
        Init: init,
        BillingRulesTabLoad: billingRulesTabLoad,
        LoadBillingRulesByTask: loadBillingRulesByTask
    };
})();