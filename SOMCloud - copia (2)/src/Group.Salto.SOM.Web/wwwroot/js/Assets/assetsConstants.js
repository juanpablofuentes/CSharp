var app = app || {};
app.assets = app.assets || {};
app.assets.constants = app.assets.constants || {};

app.assets.constants = (function () {
    var assetStatusesContainerId = '#AssetStatusesContainer';
    var modelsContainerId = '#ModelsContainer';
    var brandsContainerId = '#BrandsContainer';
    var familiesContainerId = '#FamiliesContainer';
    var subFamiliesContainerId = '#SubFamiliesContainer';
    var sitesContainerId = '#SitesContainer';
    var finalClientsContainerId = '#FinalClientsContainer';
    var assetsStatusesCollectionProperty = 'StatusesSelected';
    var modelsCollectionProperty = 'ModelsSelected';
    var brandsCollectionProperty = 'BrandsSelected';
    var familiesCollectionProperty = 'FamiliesSelected';
    var subFamiliesCollectionProperty = 'SubFamiliesSelected';
    var sitesCollectionProperty = 'SitesSelected';
    var finalClientsCollectionProperty = 'FinalClientsSelected';
    var itemIdProperty = 'Value';
    var itemTextProperty = 'Text';
    var itemIdSecondaryProperty = 'ValueSecondary';
    var itemTextSecondaryProperty = 'TextSecondary';
    var searchMinimumCharacters = 3;
    return {
        AssetStatusesContainerId: assetStatusesContainerId,
        ModelsContainerId: modelsContainerId,
        BrandsContainerId: brandsContainerId,
        FamiliesContainerId: familiesContainerId,
        SubFamiliesContainerId: subFamiliesContainerId,
        SitesContainerId: sitesContainerId,
        FinalClientsContainerId: finalClientsContainerId,
        AssetsStatusesCollectionProperty: assetsStatusesCollectionProperty,
        ModelsCollectionProperty: modelsCollectionProperty,
        BrandsCollectionProperty: brandsCollectionProperty,
        FamiliesCollectionProperty: familiesCollectionProperty,
        SubFamiliesCollectionProperty: subFamiliesCollectionProperty,
        SitesCollectionProperty: sitesCollectionProperty,
        FinalClientsCollectionProperty: finalClientsCollectionProperty,
        ItemIdProperty: itemIdProperty,
        ItemTextProperty: itemTextProperty,
        ItemIdSecondaryProperty: itemIdSecondaryProperty,
        ItemTextSecondaryProperty: itemTextSecondaryProperty,
        SearchMinimumCharacters: searchMinimumCharacters
    };
})();