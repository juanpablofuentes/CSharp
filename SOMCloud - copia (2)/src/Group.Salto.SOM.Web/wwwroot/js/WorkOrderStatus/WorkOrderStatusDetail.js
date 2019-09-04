var app = app || {};
app.workOrderStatus = app.workOrderStatus || {};
app.workOrderStatus.detail = app.workOrderStatus.detail || {};

app.workOrderStatus.detail = (function () {
    var init = function (options) {
        initializeColorPicker(options.colorPickerOptions);
        initializeTextTranslations(options.textTranslationsOptions);
        initializeDescriptionTranslations(options.descriptionsTranslationsOptions);
    };

    function initializeTextTranslations(textTranslationsOptions) {
        var options = {
            selectedItems: textTranslationsOptions.selectedItems,
            column1Text: textTranslationsOptions.column1Text,
            column2Text: textTranslationsOptions.column2Text,
            column3Text: textTranslationsOptions.column3Text,
            inputPlaceHolder: textTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.workOrderStatus.constants.CollectionTranslationTextProperty,
            itemIdProperty: app.workOrderStatus.constants.ItemIdProperty,
            itemTextProperty: app.workOrderStatus.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.workOrderStatus.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.workOrderStatus.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.workOrderStatus.constants.columnFirst,
            colSecond: app.workOrderStatus.constants.columnSecond,
            colLast: app.workOrderStatus.constants.columnLast
        };
        new comboInputListSelector().Init(app.workOrderStatus.constants.TextTranslationsContainerId, options);
    }

    function initializeDescriptionTranslations(descriptionsTranslationsOptions) {
        var options = {
            selectedItems: descriptionsTranslationsOptions.selectedItems,
            column1Text: descriptionsTranslationsOptions.column1Text,
            column2Text: descriptionsTranslationsOptions.column2Text,
            column3Text: descriptionsTranslationsOptions.column3Text,
            inputPlaceHolder: descriptionsTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.workOrderStatus.constants.CollectionTranslationDescriptionProperty,
            itemIdProperty: app.workOrderStatus.constants.ItemIdProperty,
            itemTextProperty: app.workOrderStatus.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.workOrderStatus.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.workOrderStatus.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.workOrderStatus.constants.columnFirst,
            colSecond: app.workOrderStatus.constants.columnSecond,
            colLast: app.workOrderStatus.constants.columnLast
        };
        new comboInputListSelector().Init(app.workOrderStatus.constants.DescriptionsTranslationsContainerId, options);

    }

    function initializeColorPicker(colorPickerOptions) {
        var options = {
            btnSelect: colorPickerOptions.btnSelect,
            btnCancel: colorPickerOptions.btnCancel,
            color: colorPickerOptions.color,
            parent: app.workOrderStatus.constants.ColorPickerParent,
            skin: app.workOrderStatus.constants.ColorPickerSkin,
            input: app.workOrderStatus.constants.ColorPickerInput
        };
        app.colorPicker.Init(options);
    }

    return {
        Init: init
    };
})();