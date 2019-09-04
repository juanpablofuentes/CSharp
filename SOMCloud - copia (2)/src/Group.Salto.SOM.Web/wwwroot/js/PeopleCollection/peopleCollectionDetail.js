var app = app || {};
app.peopleCollection = app.peopleCollection || {};
app.peopleCollection.detail = app.peopleCollection.detail || {};

app.peopleCollection.detail = (function () {

    var init = function (options) {
        initializePeopleCombo(options.peopleCombo);
        initializeAdminCombo(options.administratorCombo);
    };

    function initializeAdminCombo(options) {
        var responsibles = new autoCompleteListSelector();
        responsibles.Init(app.peopleCollection.constants.AdministratorsContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getPeople,
                collectionProperty: app.peopleCollection.constants.AdministratorCollectionProperty,
                itemIdProperty: app.peopleCollection.constants.ItemIdProperty,
                itemTextProperty: app.peopleCollection.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.peopleCollection.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.peopleCollection.constants.itemTextSecondaryProperty,
                minimumCharacters: app.peopleCollection.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return responsibles;
    }

    function initializePeopleCombo(peopleOptions) {
        var peopleCombo = new autoCompleteListSelector();
        peopleCombo.Init(app.peopleCollection.constants.PeopleContainerId,
            {
                selectedItems: peopleOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getPeople,
                collectionProperty: app.peopleCollection.constants.PeopleCollectionProperty,
                itemIdProperty: app.peopleCollection.constants.ItemIdProperty,
                itemTextProperty: app.peopleCollection.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.peopleCollection.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.peopleCollection.constants.itemTextSecondaryProperty,
                minimumCharacters: app.peopleCollection.constants.SearchMinimumCharacters,
                column1Text: peopleOptions.column1Text,
                column2Text: peopleOptions.column2Text,
                getDataMethod: peopleOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return peopleCombo;
    }

    return {
        Init: init
    };
})();