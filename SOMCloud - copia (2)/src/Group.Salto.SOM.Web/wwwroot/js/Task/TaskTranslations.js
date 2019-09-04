var app = app || {};
app.TaskTranslations = app.TaskTranslations || {};

app.TaskTranslations = (function (options) {
    var init = function (options) {
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
            collectionProperty: app.TaskConstants.CollectionTranslationTextProperty,
            itemIdProperty: app.TaskConstants.ItemIdProperty,
            itemTextProperty: app.TaskConstants.ItemTextProperty,
            itemIdSecondaryProperty: app.TaskConstants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.TaskConstants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.TaskConstants.columnFirst,
            colSecond: app.TaskConstants.columnSecond,
            colLast: app.TaskConstants.columnLast
        };
        new comboInputListSelectorFromJson().Init(app.TaskConstants.TextTranslationsContainerId, options);
    }

    function initializeDescriptionTranslations(descriptionsTranslationsOptions) {
        var options = {
            selectedItems: descriptionsTranslationsOptions.selectedItems,
            column1Text: descriptionsTranslationsOptions.column1Text,
            column2Text: descriptionsTranslationsOptions.column2Text,
            column3Text: descriptionsTranslationsOptions.column3Text,
            inputPlaceHolder: descriptionsTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.TaskConstants.CollectionTranslationDescriptionProperty,
            itemIdProperty: app.TaskConstants.ItemIdProperty,
            itemTextProperty: app.TaskConstants.ItemTextProperty,
            itemIdSecondaryProperty: app.TaskConstants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.TaskConstants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.TaskConstants.columnFirst,
            colSecond: app.TaskConstants.columnSecond,
            colLast: app.TaskConstants.columnLast
        };
        new comboInputListSelectorFromJson().Init(app.TaskConstants.DescriptionsTranslationsContainerId, options);

    }

    return {
        Init: init
    };
})();