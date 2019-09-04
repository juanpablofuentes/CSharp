var app = app || {};
app.workOrderCategoriesCollection = app.workOrderCategoriesCollection || {};
app.workOrderCategoriesCollection.detail = app.workOrderCategoriesCollection.detail || {};

app.workOrderCategoriesCollection.detail = (function () {
    var init = function (options) {
        initializeCategoriesCombo(options.categoriesCombo);
    };
    function initializeCategoriesCombo(categoriesOptions) {
        var categories = new autoCompleteListSelector();
        categories.Init("#workOrderCategoriesContainer",
            {
                selectedItems: categoriesOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getCategories,
                collectionProperty: 'CategoriesSelected',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 3,
                column1Text: categoriesOptions.column1Text,
                column2Text: '',
                getDataMethod: categoriesOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return categories;
    }

    return {
        Init: init
    };
})();