var app = app || {};
app.extrafields = app.extrafields || {};
app.extrafields.constants = app.extrafields.constants || {};

app.extrafields.constants = (function () {
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
    var cssColFirts = "col-3 pr-0";
    var cssColSecond = "col p-0";
    var cssColLast = "col-2 p-0";
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
        columnFirst: cssColFirts,
        columnSecond: cssColSecond,
        columnLast: cssColLast
    };
})();