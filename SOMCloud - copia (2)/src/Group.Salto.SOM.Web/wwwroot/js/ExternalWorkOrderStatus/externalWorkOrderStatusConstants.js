var app = app || {};
app.externalWorkOrderStatus = app.externalWorkOrderStatus || {};
app.externalWorkOrderStatus.constants = app.externalWorkOrderStatus.constants || {};
app.externalWorkOrderStatus.constants = (function () {
    var colorPickerInput = 'colorPickerInput';
    var colorPickerParent = 'colorPicker';
    var colorPickerSkin = 'material';
    var collectionTranslationTextProperty = 'TextTranslations';
    var collectionTranslationDescriptionProperty = 'DescriptionTranslations';
    var itemIdProperty = 'Value';
    var itemTextProperty = 'Text';
    var itemIdSecondaryProperty = 'ValueSecondary';
    var itemTextSecondaryProperty = 'TextSecondary';
    var textTranslationsContainerId = '#nameTranslationsContainer';
    var descriptionsTranslationsContainerId = '#descriptionTranslationsContainer';
    return {
        ColorPickerInput: colorPickerInput,
        ColorPickerParent: colorPickerParent,
        ColorPickerSkin: colorPickerSkin,
        CollectionTranslationTextProperty: collectionTranslationTextProperty,
        CollectionTranslationDescriptionProperty: collectionTranslationDescriptionProperty,
        ItemIdProperty: itemIdProperty,
        ItemTextProperty: itemTextProperty,
        ItemIdSecondaryProperty: itemIdSecondaryProperty,
        ItemTextSecondaryProperty: itemTextSecondaryProperty,
        TextTranslationsContainerId: textTranslationsContainerId,
        DescriptionsTranslationsContainerId: descriptionsTranslationsContainerId,
    };
})();