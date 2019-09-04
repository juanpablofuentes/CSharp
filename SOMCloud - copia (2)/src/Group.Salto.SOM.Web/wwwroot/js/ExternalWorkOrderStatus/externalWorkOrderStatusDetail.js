var app = app || {};
app.externalWorkOrderStatus = app.externalWorkOrderStatus || {};
app.externalWorkOrderStatus.detail = app.externalWorkOrderStatus.detail || {};

app.externalWorkOrderStatus.detail = (function () {

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
            collectionProperty: app.externalWorkOrderStatus.constants.CollectionTranslationTextProperty,
            itemIdProperty: app.externalWorkOrderStatus.constants.ItemIdProperty,
            itemTextProperty: app.externalWorkOrderStatus.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.externalWorkOrderStatus.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.externalWorkOrderStatus.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
        };
        new comboInputListSelector().Init(app.externalWorkOrderStatus.constants.TextTranslationsContainerId, options);
    }

    function initializeDescriptionTranslations(descriptionsTranslationsOptions) {
        var options = {
            selectedItems: descriptionsTranslationsOptions.selectedItems,
            column1Text: descriptionsTranslationsOptions.column1Text,
            column2Text: descriptionsTranslationsOptions.column2Text,
            column3Text: descriptionsTranslationsOptions.column3Text,
            inputPlaceHolder: descriptionsTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.externalWorkOrderStatus.constants.CollectionTranslationDescriptionProperty,
            itemIdProperty: app.externalWorkOrderStatus.constants.ItemIdProperty,
            itemTextProperty: app.externalWorkOrderStatus.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.externalWorkOrderStatus.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.externalWorkOrderStatus.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
        };
        new comboInputListSelector().Init(app.externalWorkOrderStatus.constants.DescriptionsTranslationsContainerId, options);

    }

    function initializeColorPicker(colorPickerOptions) {
        var options = {
            btnSelect: colorPickerOptions.btnSelect,
            btnCancel: colorPickerOptions.btnCancel,
            color: colorPickerOptions.color,
            parent: app.externalWorkOrderStatus.constants.ColorPickerParent,
            skin: app.externalWorkOrderStatus.constants.ColorPickerSkin,
            input: app.externalWorkOrderStatus.constants.ColorPickerInput,
        };
        app.colorPicker.Init(options);
    }

    return {
        Init: init
    };
})();