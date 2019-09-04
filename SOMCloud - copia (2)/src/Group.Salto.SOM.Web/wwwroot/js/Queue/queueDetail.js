var app = app || {};
app.queue = app.queue || {};
app.queue.detail = app.queue.detail || {};

app.queue.detail = (function () {
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
            collectionProperty: app.queue.constants.CollectionTranslationTextProperty,
            itemIdProperty: app.queue.constants.ItemIdProperty,
            itemTextProperty: app.queue.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.queue.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.queue.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.queue.constants.columnFirst,
            colSecond: app.queue.constants.columnSecond,
            colLast: app.queue.constants.columnLast
        };
        new comboInputListSelector().Init(app.queue.constants.TextTranslationsContainerId, options);
    }

    function initializeDescriptionTranslations(descriptionsTranslationsOptions) {
        var options = {
            selectedItems: descriptionsTranslationsOptions.selectedItems,
            column1Text: descriptionsTranslationsOptions.column1Text,
            column2Text: descriptionsTranslationsOptions.column2Text,
            column3Text: descriptionsTranslationsOptions.column3Text,
            inputPlaceHolder: descriptionsTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.queue.constants.CollectionTranslationDescriptionProperty,
            itemIdProperty: app.queue.constants.ItemIdProperty,
            itemTextProperty: app.queue.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.queue.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.queue.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.queue.constants.columnFirst,
            colSecond: app.queue.constants.columnSecond,
            colLast: app.queue.constants.columnLast
        };
        new comboInputListSelector().Init(app.queue.constants.DescriptionsTranslationsContainerId, options);

    }

    return {
        Init: init
    };
})();