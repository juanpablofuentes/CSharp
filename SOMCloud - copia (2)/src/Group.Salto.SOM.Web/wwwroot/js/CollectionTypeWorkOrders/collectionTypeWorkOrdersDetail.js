var app = app || {};
app.collectionTypeWorkOrders = app.collectionTypeWorkOrders || {};
app.collectionTypeWorkOrders.detail = app.collectionTypeWorkOrders.detail || {};

app.collectionTypeWorkOrders.detail = (function () {

    var init = function (options) {
        initializeTreeView(options.tree);
    };

    function initializeTreeView(options) {
        new treeView().Init("#workOrderTypes",
            {
                values: options.values,
                collectionProperty: 'Childs',
                textProperty: 'Name',
                idProperty: 'Id',
                descriptionProperty: 'Description',
                baseColor: '1E90FF',
                searchPlaceHolder: options.searchPlaceHolder,
                placeHolderText: options.placeHolderText,
                placeHolderDescription: options.placeHolderDescription,
                validationTextMessage: options.validationTextMessage,
                canDeleteMethod: options.canDeleteMethod,
                canDeleteTreeLevelMethod: options.canDeleteTreeLevelMethod,
                stringAddNew: options.stringAddNew,
                btnRemoveRow: options.btnRemoveRow,
                validationDescriptionMessage: options.validationDescriptionMessage,
                additionalProperties: options.additionalProperties
            });
    }

    return {
        Init: init
    };
})();