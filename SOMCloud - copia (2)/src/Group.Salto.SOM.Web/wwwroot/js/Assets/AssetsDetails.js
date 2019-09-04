var app = app || {};
app.assets = app.assets || {};
app.assets.detail = app.assets.detail || {};

app.assets.detail = (function () {

    var selectedFamily, selectedBrand, selectedClient, selectedLocation, fromSiteId;
    var returnIndexFromSiteUrl, returnIndexUrl;

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["SecondaryDetailViewModel_Warranty_StdStartDate", "SecondaryDetailViewModel_Warranty_StdEndDate",
                                                   "SecondaryDetailViewModel_Warranty_BlnStartDate", "SecondaryDetailViewModel_Warranty_BlnEndDate",
                                                   "SecondaryDetailViewModel_Warranty_ProStartDate", "SecondaryDetailViewModel_Warranty_ProEndDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y-%m-%d %H:%i");
        datePicker.hideTime();
    };

    function CancelCreateEditAsset() {        
        if (fromSiteId)
        {
            var link = returnIndexFromSiteUrl;
            link = link.replace("fromSiteId", fromSiteId);
            window.location.href = link;
        }
        else {
            window.location.href = returnIndexUrl;
        }
    };

    var init = function (options) {
        fromSiteId = options.fromSiteId;
        returnIndexFromSiteUrl = options.urlIndexAssetsFromSiteId;
        returnIndexUrl = options.urlIndexAssets;    
        $("#CancelEditAsset").on('click', CancelCreateEditAsset);
        initializeModelsAutocomplete(options.Models);
        initializeBrandsAutocomplete(options.Brands);
        initializeFamiliesAutocomplete(options.Families);
        initializeSubFamiliesAutocomplete(options.SubFamilies);
        initializeFinalClientsAutocomplete(options.FinalClients);
        initializeLocationsAutocomplete(options.Locations);
        initializeUsersAutocomplete(options.Users);
    };

    function initializeFamiliesAutocomplete(options) {
        var familiesCombo = new autocomplete();
        familiesCombo.Init("#familiesContainer",
        {
            hasDefaultItem: options.hasDefaultItem,
            initValue: { key: options.key, value: options.value },
            urlData: app.config.Urls.getFamilies,
            selectedItemProperty: options.itemIdProperty,
            selectedTextProperty: options.itemTextProperty,
            minimumCharacters: 1,
            nColumns: false,
            getDataMethod: options.getDataMethod,
            ajaxMethodType: app.constants.Post
            });
        selectedFamily = familiesCombo;
        return familiesCombo;
    }

    function getSubFamilyFilter(text) {
        var selectedOption = selectedFamily.GetSelectedOption();
        var data = {
            Text: text,
            Selected: (selectedOption !== undefined) ? selectedOption.key : []
        }
        return data;
    };

    function initializeSubFamiliesAutocomplete(options) {
        var subfamiliesCombo = new autocomplete();
        subfamiliesCombo.Init("#subfamiliesContainer",
        {
            hasDefaultItem: options.hasDefaultItem,
            initValue: { key: options.key, value: options.value },
            urlData: app.config.Urls.getSubFamiliesFiltered,
            selectedItemProperty: options.itemIdProperty,
            selectedTextProperty: options.itemTextProperty,
            minimumCharacters: 1,
            nColumns: false,
            getDataMethod: getSubFamilyFilter,
            ajaxMethodType: app.constants.Post,
        });
        return subfamiliesCombo;
    }

    function initializeBrandsAutocomplete(options) {
        var brandsCombo = new autocomplete();
        brandsCombo.Init("#brandsContainer",
        {
            hasDefaultItem: options.hasDefaultItem,
            initValue: { key: options.key, value: options.value },
            urlData: app.config.Urls.getBrands,
            selectedItemProperty: options.itemIdProperty,
            selectedTextProperty: options.itemTextProperty,
            minimumCharacters: 1,
            nColumns: false,
            getDataMethod: options.getDataMethod,
            ajaxMethodType: app.constants.Post,
        });
        selectedBrand = brandsCombo;
        return brandsCombo;
    }

    function getModelsFilter(text) {
        var selectedOption = selectedBrand.GetSelectedOption();
        var data = {
            Text: text,
            Selected: (selectedOption !== undefined) ? selectedOption.key : []
        }
        return data;
    };

    function initializeModelsAutocomplete(options) {
        var modelsCombo = new autocomplete();
        modelsCombo.Init("#modelsContainer",
        {
            hasDefaultItem: options.hasDefaultItem,
            initValue: { key: options.key, value: options.value },
            urlData: app.config.Urls.getModelsFiltered,
            selectedItemProperty: options.itemIdProperty,
            selectedTextProperty: options.itemTextProperty,
            minimumCharacters: 1,
            nColumns: false,
            getDataMethod: getModelsFilter,
            ajaxMethodType: app.constants.Post,
        });
        return modelsCombo;
    }

    function initializeFinalClientsAutocomplete(options) {
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
        if (fromSiteId) {
            $("#finalclientsContainer :input").each(function () { $(this).prop('readonly', true) });
        }
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

    function initializeLocationsAutocomplete(options) {
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
        if (fromSiteId) {
            $("#locationsContainer :input").each(function () { $(this).prop('readonly', true) }); 
        }
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

    function initializeUsersAutocomplete(options) {
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
        Init: init,
        CalendarLoad: calendarLoad
    };
})();