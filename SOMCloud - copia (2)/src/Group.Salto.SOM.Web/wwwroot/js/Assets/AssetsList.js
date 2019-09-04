var app = app || {};
app.assets = app.assets || {};

app.assets = (function (event) {
    var initialSerialNumber = '';
    var selectedClient, selectedLocation, selectedAssets, selectedFamily, selectedBrand, selectedCSites;

    var init = function (options) {
        initializeEvents();
        initializeStatusAutocomplete(options.Status);
        initializeModelsAutocomplete(options.Models);
        initializeBrandsAutocomplete(options.Brands);
        initializeFamiliesAutocomplete(options.Families);
        initializeSubFamiliesAutocomplete(options.SubFamilies);
        initializeSitesAutocomplete(options.Sites);
        initializeFinalClientsAutocomplete(options.FinalClients);
        initializeTransferFinalClientsAutocomplete(options.TransferFinalClients);
        initializeTransferSitesAutocomplete(options.TransferLocations);
        initializeTransferUsersAutocomplete(options.TransferUsers);
    };

    var toggleFilter = function (e) {
        if ($('#filterAssets').is(":hidden")) {
            $("#filterAssets").show("slow");
        } else {
            $("#filterAssets").slideUp("slow");
        }
    };

    var clearFilterFields = function (e) {
        $('#assetsFilterSerialNumber').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialSerialNumber !== $('#assetsFilterSerialNumber').val()) {
            $('#pager-page').val(1);
        }
    };

    var ClickTransferButton = function (e) {
        var table = $("table");
        var divAssets = $("#assetsIds");
        table.find('input[type="checkbox"]:checked').each(function (index) {
            divAssets.append('<input type="hidden" value="' + this.value + '" name="AssetsId[' + index + ']"/>');
        });
    };

    var enableTransferButton = function (e) {
        var table = $("table");
        var selected = table.find('input[type="checkbox"]:checked').length;
        if (selected < 1) {
            $('#btnTransfer').prop("disabled", true);
            $('#collapseTransfer').collapse("hide");
        }
        else {
            $('#btnTransfer').removeAttr("disabled");
        }
    };

    function initializeEvents() {
        $('.button-filter').on('click', toggleFilter);
        $('#btnClear').on('click', clearFilterFields);
        $('#btnApplyFilter').on('click', ClickFilterButton);
        $('#btnTransfer').on('click', ClickTransferButton);
        $("table").find('input[name^="SelectedIds"]').on('change', enableTransferButton);

        initialSerialNumber = $('#assetsFilterSerialNumber').val();
    }

    function initializeStatusAutocomplete(options) {
        var statusAuto = new autoCompleteListSelector();
        statusAuto.Init(app.assets.constants.AssetStatusesContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getAssetStatuses,
                collectionProperty: app.assets.constants.AssetsStatusesCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return statusAuto;
    }

    function initializeBrandsAutocomplete(options) {
        var brandsAuto = new autoCompleteListSelector();
        brandsAuto.Init(app.assets.constants.BrandsContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getBrands,
                collectionProperty: app.assets.constants.BrandsCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedBrand = brandsAuto;
        return brandsAuto;
    }

    function getModelsFilter(text) {
        var selectedOptions = selectedBrand.GetSelectedIds();
        var arrayOptions = (selectedOptions == undefined) ? [] : JSON.parse("[" + selectedOptions + "]");

        var data = {
            Text: text,
            Selected: arrayOptions
        }
        return data;
    };

    function initializeModelsAutocomplete(options) {
        var modelsAuto = new autoCompleteListSelector();
        modelsAuto.Init(app.assets.constants.ModelsContainerId,
            {
                disabled: 'disabled',
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getModelsFiltered,
                collectionProperty: app.assets.constants.ModelsCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: getModelsFilter,
                ajaxMethodType: app.constants.Post
            });

        return modelsAuto;
    }

    function initializeFamiliesAutocomplete(options) {
        var familiesAuto = new autoCompleteListSelector();
        familiesAuto.Init(app.assets.constants.FamiliesContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getFamilies,
                collectionProperty: app.assets.constants.FamiliesCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedFamily = familiesAuto;
        return familiesAuto;
    }

    function getSubFamilyFilter(text) {
        var selectedOptions = selectedFamily.GetSelectedIds();
        var arrayOptions = (selectedOptions == undefined) ? [] : JSON.parse("[" + selectedOptions + "]");

        var data = {
            Text: text,
            Selected: arrayOptions
        }
        return data;
    };

    function initializeSubFamiliesAutocomplete(options) {
        var subFamiliesAuto = new autoCompleteListSelector();
        subFamiliesAuto.Init(app.assets.constants.SubFamiliesContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getSubFamiliesFiltered,
                collectionProperty: app.assets.constants.SubFamiliesCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: getSubFamilyFilter,
                ajaxMethodType: app.constants.Post
            });
        return subFamiliesAuto;
    }

    function initializeFinalClientsAutocomplete(options) {
        var finalClientsAuto = new autoCompleteListSelector();
        finalClientsAuto.Init(app.assets.constants.FinalClientsContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getFinalClients,
                collectionProperty: app.assets.constants.FinalClientsCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedCSites = finalClientsAuto;
        return finalClientsAuto;
    }

    function getSitesFilter(text) {
        var selectedOptions = selectedCSites.GetSelectedIds();
        var arrayOptions = (selectedOptions == undefined) ? [] : JSON.parse("[" + selectedOptions + "]");

        var data = {
            Text: text,
            Selected: arrayOptions
        }
        return data;
    };

    function initializeSitesAutocomplete(options) {
        var sitesAuto = new autoCompleteListSelector();
        sitesAuto.Init(app.assets.constants.SitesContainerId,
            {
                selectedItems: options.selectedItems,
                urlPrincipalCombo: app.config.Urls.getSitesFiltered,
                collectionProperty: app.assets.constants.SitesCollectionProperty,
                itemIdProperty: app.assets.constants.ItemIdProperty,
                itemTextProperty: app.assets.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.assets.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.assets.constants.itemTextSecondaryProperty,
                minimumCharacters: app.assets.constants.SearchMinimumCharacters,
                column1Text: options.column1Text,
                column2Text: options.column2Text,
                getDataMethod: getSitesFilter,
                ajaxMethodType: app.constants.Post
            });
        return sitesAuto;
    }

    function initializeTransferFinalClientsAutocomplete(options) {
        var clientsCombo = new autocomplete();
        clientsCombo.Init("#finalclientsContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getFinalClients,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 2,
                nColumns: false,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post,
            });
        selectedClient = clientsCombo;
        return clientsCombo;
    }

    function getSiteLocationsFilter(text) {
        var selectedOption = selectedClient.GetSelectedOption();
        var data = {
            Text: text,
            Selected: (selectedOption !== undefined) ? selectedOption.key : []
        }
        return data;
    };

    function initializeTransferSitesAutocomplete(options) {
        var locationsCombo = new autocomplete();
        locationsCombo.Init("#locationsContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getSitesFiltered,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getSiteLocationsFilter,
                ajaxMethodType: app.constants.Post
            });
        selectedLocation = locationsCombo;
        return locationsCombo;
    }

    function getSiteUsersFilter(text) {
        var selectedOption = selectedLocation.GetSelectedOption();
        var data = {
            Text: text,
            Selected: (selectedOption !== undefined) ? selectedOption.key : []
        }
        return data;
    };

    function initializeTransferUsersAutocomplete(options) {
        var usersCombo = new autocomplete();
        usersCombo.Init("#usersContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getSitesFiltered,
                urlData: app.config.Urls.getSiteUserFiltered,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getSiteUsersFilter,
                ajaxMethodType: app.constants.Post
            });
        return usersCombo;
    }

    return {
        Init: init
    };
})();