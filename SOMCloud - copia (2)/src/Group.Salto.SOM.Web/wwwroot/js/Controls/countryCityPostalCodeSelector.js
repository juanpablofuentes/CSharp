var countryCityPostalCodeSelector = countryCityPostalCodeSelector || {};
countryCityPostalCodeSelector = function () {
    var defaults = {
        countriesToSelect: [],
        countrySelectedProperty: "countrySelected",
        citySelectedProperty: "citySelected",
        classes: {
            select: "form-control selecTable",
            header: {
                root: "",
                group: "form-group"
            }
        }
    };
    var settings;
    var countriesToSelect = [];
    var countrySelectedProperty = "countrySelected";
    var citySelectedProperty = "citySelected";
    var stateSelectedProperty = "stateSelected";
    var regionSelectedProperty = "regionSelected";
    var postalCodeSelectedProperty = "postalcodeSelected";
    var instanceIdCountrySelected;
    var instanceIdCitySelected;
    var instanceIdRegionSelected;
    var instanceIdStateSelected;
    var instanceIdPostalCodeSelected;
    var container;
    var instanceIdCountries;
    var instanceIdCities;
    var instanceIdRegions;
    var instanceIdStates;
    var instanceIdPostalCode;
    var urlCities;
    var urlRegions;
    var urlStates;
    var urlPostalCode;
    var defaultOptionText;
    var selectedCountry;
    var selectedCity;
    var selectedState;
    var selectedRegion;
    var selectedPostalCode;
    var countryText = "Country";
    var regionText = "Region";
    var stateText = "State";
    var cityText = "City";
    var postalCodeText = "PostalCode";
    var enablePostalCode = false;

    function loadSkeleton() {

        var inputpostalcode = '';
        var selectpostalcode = '';

        if (enablePostalCode) {
            inputpostalcode = "<input id='" + instanceIdPostalCodeSelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + postalCodeSelectedProperty + "' type='hidden' value='0'>"

            selectpostalcode = "<div class='" + settings.classes.header.group + "'>"
                + "<label class=\"my-1 mr-2\" for=\"slct_" + instanceIdPostalCode + "\">" + postalCodeText + "</label>"
                + "<select id ='slct_" + instanceIdPostalCode + "' class='form-control my-1 mr-sm-2'>"
                + "</select>"
                + "</div>"
        }

        const skeleton =
            "<div class='" + settings.classes.header.root + "'>"
            + "<input id='" + instanceIdCountrySelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + countrySelectedProperty + "' type='hidden' value='0'>"
            + "<input id='" + instanceIdRegionSelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + regionSelectedProperty + "' type='hidden' value='0'>"
            + "<input id='" + instanceIdStateSelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + stateSelectedProperty + "' type='hidden' value='0'>"
            + "<input id='" + instanceIdCitySelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + citySelectedProperty + "' type='hidden' value='0'>"
            + inputpostalcode
            + "<div class='" + settings.classes.header.group + "'>"
            + "<label class=\"my-1 mr-2\" for=\"slct_" + instanceIdCountries + "\">" + countryText + "</label>"
            + "<select id='slct_" + instanceIdCountries + "' class='form-control my-1 mr-sm-2'>"
            + "</select>"
            + "</div>"
            + "<div class='" + settings.classes.header.group + "'>"
            + "<label class=\"my-1 mr-2\" for=\"slct_" + instanceIdCountries + "\">" + regionText + "</label>"
            + "<select id ='slct_" + instanceIdRegions + "' class='form-control my-1 mr-sm-2'>"
            + "</select>"
            + "</div>"
            + "<div class='" + settings.classes.header.group + "'>"
            + "<label class=\"my-1 mr-2\" for=\"slct_" + instanceIdCountries + "\">" + stateText + "</label>"
            + "<select id ='slct_" + instanceIdStates + "' class='form-control my-1 mr-sm-2'>"
            + "</select>"
            + "</div>"
            + "<div class='" + settings.classes.header.group + "'>"
            + "<label class=\"my-1 mr-2\" for=\"slct_" + instanceIdCountries + "\">" + cityText + "</label>"
            + "<select id ='slct_" + instanceIdCities + "' class='form-control my-1 mr-sm-2'>"
            + "</select>"
            + "</div>"
            + selectpostalcode
            + "</div>";

        $(container).html(skeleton);
    }

    function initializeEvents() {
        $("#slct_" + instanceIdCountries).on("change", countryChanged);
        $("#slct_" + instanceIdStates).on("change", stateChanged);
        $("#slct_" + instanceIdRegions).on("change", regionChanged);
        $("#slct_" + instanceIdCities).on("change", cityChanged);
        if (enablePostalCode) {
            $("#slct_" + instanceIdPostalCode).on("change", postalCodeChanged);
        }
    }

    function countryChanged() {
        var element = $("#slct_" + instanceIdCountries + " option:selected");
        $("#slct_" + instanceIdRegions).empty();
        $("#slct_" + instanceIdStates).empty();
        $("#slct_" + instanceIdCities).empty();
        $("#" + instanceIdCountrySelected).val(element.val());
        $("#" + instanceIdRegionSelected).val(0);
        $("#" + instanceIdStateSelected).val(0);
        $("#" + instanceIdCitySelected).val(0);
        if (enablePostalCode) {
            $("#slct_" + instanceIdPostalCode).empty();
            $("#" + instanceIdPostalCodeSelected).val(0);
        }
        if (element.val() != 0) {
            getdata(element.val(), urlRegions, onGetRegions);
        }
    }

    function regionChanged() {
        var element = $("#slct_" + instanceIdRegions + " option:selected");
        $("#slct_" + instanceIdStates).empty();
        $("#slct_" + instanceIdCities).empty();
        $("#" + instanceIdRegionSelected).val(element.val());
        $("#" + instanceIdStateSelected).val(0);
        $("#" + instanceIdCitySelected).val(0);
        if (enablePostalCode) {
            $("#slct_" + instanceIdPostalCode).empty();
            $("#" + instanceIdPostalCodeSelected).val(0);
        }
        if (element.val() != 0) {
            getdata(element.val(), urlStates, onGetStates);
        }
    }

    function stateChanged() {
        var element = $("#slct_" + instanceIdStates + " option:selected");
        $("#slct_" + instanceIdCities).empty();
        $("#" + instanceIdStateSelected).val(element.val());
        $("#" + instanceIdCitySelected).val(0);
        if (enablePostalCode) {
            $("#slct_" + instanceIdPostalCode).empty();
            $("#" + instanceIdPostalCodeSelected).val(0);
        }
        if (element.val() != 0) {
            getdata(element.val(), urlCities, onGetCities);
        }
    }

    function cityChanged() {
        var element = $("#slct_" + instanceIdCities + " option:selected");
        $("#" + instanceIdCitySelected).val(element.val());
        if (enablePostalCode) {
            $("#slct_" + instanceIdPostalCode).empty();
            $("#" + instanceIdPostalCodeSelected).val(0);
            if (element.val() != 0) {
                getdata($("#" + instanceIdCitySelected).val(), urlPostalCode, onPostalCodes);
            }
        }
    }

    function postalCodeChanged()
    {
        var element = $("#slct_" + instanceIdPostalCode + " option:selected");
        $("#" + instanceIdPostalCodeSelected).val(element.val());
    }

    function onGetRegions(regions) {
        app.common.ui.LoadSelectorWithVoidLine("slct_" + instanceIdRegions, regions, defaultOptionText);
        var selector = $("#slct_" + instanceIdRegions);
        if (selectedRegion > 0) {
            selector.val(selectedRegion).change();
            selectedRegion = 0;
        }
        else if (regions.length === 1) {
            selector.val(regions[0].key).change();
        }
    }

    function onGetStates(states) {
        app.common.ui.LoadSelectorWithVoidLine("slct_" + instanceIdStates, states, defaultOptionText);
        var selector = $("#slct_" + instanceIdStates);
        if (selectedState > 0) {
            selector.val(selectedState).change();
            selectedState = 0;
        }
        else if (states.length === 1) {
            selector.val(states[0].key).change();
        }
    }

    function onGetCities(cities) {
        app.common.ui.LoadSelectorWithVoidLine("slct_" + instanceIdCities, cities, defaultOptionText);
        var selector = $("#slct_" + instanceIdCities);
        if (selectedCity > 0) {
            selector.val(selectedCity).change();
            selectedCity = 0;
        }
        else if (cities.length === 1) {
            selector.val(cities[0].key).change();
        }
    }

    function onPostalCodes(postalCodes) {
        app.common.ui.LoadSelectorWithVoidLine("slct_" + instanceIdPostalCode, postalCodes, defaultOptionText);
        var selector = $("#slct_" + instanceIdPostalCode);
        if (selectedPostalCode > 0) {
            selector.val(selectedPostalCode).change();
            selectedPostalCode = 0;
        }
        else if (postalCodes.length === 1) {
            selector.val(postalCodes[0].key).change();
        }
    };

    function setInitialValues() {
        instanceIdCountries = Math.random().toString().replace(".", "");
        instanceIdCities = Math.random().toString().replace(".", "");
        instanceIdRegions = Math.random().toString().replace(".", "");
        instanceIdStates = Math.random().toString().replace(".", "");
        instanceIdPostalCode = Math.random().toString().replace(".", "");
        instanceIdCountrySelected = Math.random().toString().replace(".", "");
        instanceIdCitySelected = Math.random().toString().replace(".", "");
        instanceIdRegionSelected = Math.random().toString().replace(".", "");
        instanceIdStateSelected = Math.random().toString().replace(".", "");
        instanceIdPostalCodeSelected = Math.random().toString().replace(".", "");
        countriesToSelect = settings.countriesToSelect;
        countrySelectedProperty = settings.countrySelectedProperty;
        regionSelectedProperty = settings.regionSelectedProperty;
        stateSelectedProperty = settings.stateSelectedProperty;
        citySelectedProperty = settings.citySelectedProperty;
        postalCodeSelectedProperty = settings.postalCodeSelectedProperty;
        urlCities = settings.urlCities;
        urlRegions = settings.urlRegions;
        urlStates = settings.urlStates;
        urlPostalCode = settings.urlPostalCode;
        defaultOptionText = settings.defaultOptionText;
        selectedCountry = settings.selectedCountry;
        selectedCity = settings.selectedCity;
        selectedState = settings.selectedState;
        selectedRegion = settings.selectedRegion;
        selectedPostalCode = settings.selectedPostalCode;
        countryText = settings.countryText;
        regionText = settings.regionText;
        stateText = settings.stateText;
        cityText = settings.cityText;
        postalCodeText = settings.postalCodeText,
            enablePostalCode = settings.enablePostalCode
    }

    function loadInitialValues() {
        if (selectedCountry > 0) {
            var element = $("#slct_" + instanceIdCountries);
            element.val(selectedCountry);
            selectedCountry = 0;
            countryChanged();
        }
    }

    function getdata(data, url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { id: data },
            success: success
        });
    }

    var init = function (startContainer, options) {
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton();
        app.common.ui.LoadSelectorWithVoidLine("slct_" + instanceIdCountries, countriesToSelect, defaultOptionText);
        loadInitialValues();
        initializeEvents();
    };

    return {
        Init: init,
    };
};