var app = app || {};
app.peopleCollection = app.peopleCollection || {};
app.peopleCollection.constants = app.peopleCollection.constants || {};

app.peopleCollection.constants = (function () {
    var peopleContainerId = '#peopleContainer';
    var administratorsContainerId = '#administratorContainer';
    var peopleCollectionProperty = 'People';
    var administratorCollectionProperty = 'PeopleAdministrator';
    var itemIdProperty = 'Value';
    var itemTextProperty = 'Text';
    var itemIdSecondaryProperty = 'ValueSecondary';
    var itemTextSecondaryProperty = 'TextSecondary';
    var searchMinimumCharacters = 3;
    return {
        PeopleContainerId: peopleContainerId,
        AdministratorsContainerId: administratorsContainerId,
        PeopleCollectionProperty: peopleCollectionProperty,
        AdministratorCollectionProperty: administratorCollectionProperty,
        ItemIdProperty: itemIdProperty,
        ItemTextProperty: itemTextProperty,
        ItemIdSecondaryProperty: itemIdSecondaryProperty,
        ItemTextSecondaryProperty: itemTextSecondaryProperty,
        SearchMinimumCharacters: searchMinimumCharacters
    };
})();