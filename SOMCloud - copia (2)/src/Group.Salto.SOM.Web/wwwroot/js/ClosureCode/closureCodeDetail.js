var app = app || {};
app.closureCode = app.closureCode || {};
app.closureCode.detail = app.closureCode.detail || {};

app.closureCode.detail = (function () {

    var init = function (options) {
        initializeTreeView(options.tree);
    };

    function initializeTreeView(options) {
        new treeView().Init("#closingCodes",
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
                stringAddNew: options.stringAddNew,
                btnRemoveRow: options.btnRemoveRow,
                validationDescriptionMessage: options.validationDescriptionMessage,
            });
    }

    return {
        Init: init
    };
})();