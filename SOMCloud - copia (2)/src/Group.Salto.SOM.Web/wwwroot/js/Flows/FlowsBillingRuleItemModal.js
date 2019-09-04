var app = app || {};
app.billingRuleItemModal = app.billingRuleItemModal || {};

app.billingRuleItemModal = (function (event) {
    var postCreateBillingRuleItemUrl, postUpdateBillingRuleItemUrl, ItemSelect = '';

    var init = function (options) {
        postCreateBillingRuleItemUrl = options.postCreateBillingRuleItemUrl;
        postUpdateBillingRuleItemUrl = options.postUpdateBillingRuleItemUrl;
    };

    var openBillingRuleItemModal = function (item) {
        $('#billingruleitem-rule-id').val('');
        $('#billingruleitem-item-id').val('');
        $('#billingruleitem-item-units').val('');

        loadBillingRuleItemValues(item);
    }

    var loadBillingRuleItemValues = function (item) {
        var ItemCombo = new autocomplete();        
        ItemCombo.Init("#billingruleitem-item-name",
        {
            hasDefaultItem: false,
            initValue: { key: item.itemid, value: item.itemname },
            urlData: app.config.Urls.getItems,
            selectedItemProperty: 'ItemId.value',
            selectedTextProperty: 'ItemId.value',
            minimumCharacters: 2,
            nColumns: false,
            getDataMethod: getItemsFilter,
            ajaxMethodType: app.constants.Post
            });
        ItemSelect = ItemCombo;

        setBillingRuleItemModalValues(item);
    };

    var setBillingRuleItemModalValues = function (item) {
        $('#billingruleitem-rule-id').val(item.billingRuleId);
        $('#billingruleitem-item-id').val(item.id);
        $('#billingruleitem-item-units').val(item.units);
        
        if (item.type === 'edit')
            $('#billingruleitem-btn-submit').bind('click', postUpdateModalBillingRuleItemData);
        else
            $('#billingruleitem-btn-submit').bind('click', postCreateModalBillingRuleItemData);
    }

    var postUpdateModalBillingRuleItemData = function (ev) {
        ev.preventDefault();

        var billingRuleItemDto = getModalData();

        $.post(postUpdateBillingRuleItemUrl, billingRuleItemDto).done(function (data) {
            var billingruletabId = '#nav-billingrules-' + CurrentTaskId;
            $('a[href="' + billingruletabId +'"]').click();
        });
    };

    var postCreateModalBillingRuleItemData = function (ev) {
        ev.preventDefault()

        var billingRuleItemDto = getModalData();

        $.post(postCreateBillingRuleItemUrl, billingRuleItemDto).done(function (data) {
            var billingruletabId = '#nav-billingrules-' + CurrentTaskId;
            $('a[href="' + billingruletabId + '"]').click();
        });
    }

    function getModalData() {
        var billingitem = ItemSelect.GetSelectedOption();
        var dto = {
            'Id': $('#billingruleitem-item-id').val(),
            'BillingRuleId': $('#billingruleitem-rule-id').val(),
            'Units': $('#billingruleitem-item-units').val(),
            'Item.Id': billingitem.key,
            'Item.Name': billingitem.value,
        };
        return dto;
    }

    function getItemsFilter(text) {
        var data = {
            QueryType: app.constants.AutoComplete,
            QueryTypeParameters: {
                Text: text
            }
        }
        return data;
    };

    return {
        Init: init,
        PostCreateModalBillingRuleItemData: postCreateModalBillingRuleItemData,
        PostUpdateModalBillingRuleItemData: postUpdateModalBillingRuleItemData,
        OpenBillingRuleItemModal: openBillingRuleItemModal
    };
})();