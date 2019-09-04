var countryCitySelector = countryCitySelector || {};
countryCitySelector = function () {
    var defaults = {
        countriesToSelect: [],
        countrySelectedProperty: "countrySelected",
        citySelectedProperty: "citySelected",
        classes: {
            select: "form-control selecTable",
            header: {
                root: "",
                group: "form-group",
                horizontal: "form-group row"
            }
        }
    };
    var settings, countriesToSelect = [];
    var countrySelectedProperty = "countrySelected", citySelectedProperty = "citySelected", stateSelectedProperty = "stateSelected", regionSelectedProperty = "regionSelected", countryText = "Country",
        regionText = "Region", stateText = "State", cityText = "Municipality", defaultOptionText = "-";
    var selectedCountry, selectedCity, selectedState, selectedRegion, instanceIdCountrySelected, instanceIdCitySelected, instanceIdRegionSelected, instanceIdStateSelected, container, instanceIdCountries,
        instanceIdCities, instanceIdRegions, instanceIdStates, urlCities, urlRegions, urlStates;

    function loadSkeleton() {
        var hiddenInputs =
            "<input id='" + instanceIdCountrySelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + countrySelectedProperty + "' type='hidden' value='0'>"
            + "<input id='" + instanceIdCitySelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + citySelectedProperty + "' type='hidden' value='0'>"
            + "<input id='" + instanceIdStateSelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + stateSelectedProperty + "' type='hidden' value='0'>"
            + "<input id='" + instanceIdRegionSelected + "' data-val='true' data-val-required='The CountrySelected field is required.' name='" + regionSelectedProperty + "' type='hidden' value='0'>";

        const skeleton =
            "<div class='" + settings.classes.header.root + "'>"
            + hiddenInputs
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
            + "</div>";

        const skeletonHorizontal =
            "<div class='" + settings.classes.header.root + "'>"
            + hiddenInputs
            + "<div class='" + settings.classes.header.horizontal + "'>"
            + "<label class=\"pl-0 col-12 col-sm-3 col-form-label\" for=\"slct_" + instanceIdCountries + "\">" + countryText + "</label>"
            + "<div class='col-12 col-sm-9'>"
            + "<select id='slct_" + instanceIdCountries + "' class='form-control'></select>"
            + "</div>"
            + "</div>"

            + "<div class='" + settings.classes.header.horizontal + "'>"
            + "<label class=\"pl-0 col-12 col-sm-3 col-form-label\" for=\"slct_" + instanceIdCountries + "\">" + regionText + "</label>"
            + "<div class='col-12 col-sm-9'>"
            + "<select id ='slct_" + instanceIdRegions + "' class='form-control'></select>"
            + "</div>"
            + "</div>"

            + "<div class='" + settings.classes.header.horizontal + "'>"
            + "<label class=\"pl-0 col-12 col-sm-3 col-form-label\" for=\"slct_" + instanceIdCountries + "\">" + stateText + "</label>"
            + "<div class='col-12 col-sm-9'>"
            + "<select id ='slct_" + instanceIdStates + "' class='form-control'></select>"
            + "</div>"
            + "</div>"

            + "<div class='" + settings.classes.header.horizontal + "'>"
            + "<label class=\"pl-0 col-12 col-sm-3 col-form-label\" for=\"slct_" + instanceIdCountries + "\">" + cityText + "</label>"
            + "<div class='col-12 col-sm-9'>"
            + "<select id ='slct_" + instanceIdCities + "' class='form-control'></select > "
            + "</div>"
            + "</div>"
            + "</div>";

        if (displayHorizontal) {
            $(container).html(skeletonHorizontal);

        } else {
            $(container).html(skeleton);
        }
    }

    function loadSelector(selectorId, items) {
        var select = $(selectorId);
        select.empty();
        if (items !== undefined && items !== null) {
            if (items.length > 1) {
                var defaultOption = "<option value='" + 0 + "'>" + defaultOptionText + "</option>";
                select.append($(defaultOption));
            }
            for (var i = 0; i < items.length; i++) {
                var option = "<option value='" + items[i].key + "'>" + items[i].value + "</option>";
                select.append($(option));
            }
        }
        return select;
    }

    function initializeEvents() {
        $("#slct_" + instanceIdCountries).on("change", countryChanged);
        $("#slct_" + instanceIdStates).on("change", stateChanged);
        $("#slct_" + instanceIdRegions).on("change", regionChanged);
        $("#slct_" + instanceIdCities).on("change", cityChanged);
    }

    function countryChanged() {
        var element = $("#slct_" + instanceIdCountries + " option:selected");
        $("#slct_" + instanceIdCities).empty();
        $("#slct_" + instanceIdRegions).empty();
        $("#slct_" + instanceIdStates).empty();
        $("#" + instanceIdCountrySelected).val(element.val());
        $("#" + instanceIdCitySelected).val(0);
        $("#" + instanceIdRegionSelected).val(0);
        $("#" + instanceIdStateSelected).val(0);
        if (element.val() !== 0) {
            getdata(element.val(), urlRegions, onGetRegions);
        }
        if (settings.onChangeCountry !== undefined) {
            settings.onChangeCountry(element.val());
        }
    }

    function stateChanged() {
        var element = $("#slct_" + instanceIdStates + " option:selected");
        $("#slct_" + instanceIdCities).empty();
        $("#" + instanceIdCitySelected).val(0);
        $("#" + instanceIdStateSelected).val(element.val());
        if (element.val() !== 0) {
            getdata(element.val(), urlCities, onGetCities);
        }
        if (settings.onChangeState !== undefined) {
            settings.onChangeState(element.val());
        }
    }

    function regionChanged() {
        var element = $("#slct_" + instanceIdRegions + " option:selected");
        $("#slct_" + instanceIdCities).empty();
        $("#slct_" + instanceIdStates).empty();
        $("#" + instanceIdCitySelected).val(0);
        $("#" + instanceIdRegionSelected).val(element.val());
        $("#" + instanceIdStateSelected).val(0);
        if (element.val() !== 0) {
            getdata(element.val(), urlStates, onGetStates);
        }
        if (settings.onChangeRegion !== undefined) {
            settings.onChangeRegion(element.val());
        }
    }

    function cityChanged() {
        var element = $("#slct_" + instanceIdCities + " option:selected");
        $("#" + instanceIdCitySelected).val(element.val());
        if (settings.onChangeCity !== undefined) {
            settings.onChangeCity(element.val());
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

    function onGetRegions(regions) {
        loadSelector("#slct_" + instanceIdRegions, regions);
        var selector = $("#slct_" + instanceIdRegions);
        if (selectedRegion > 0) {
            selector.val(selectedRegion).change();
            selectedRegion = 0;
        }
        else if (regions.length === 1) {
            selector.val(regions[0].key).change();
        }
    }

    function onGetCities(cities) {
        loadSelector("#slct_" + instanceIdCities, cities);
        var selector = $("#slct_" + instanceIdCities);
        if (selectedCity > 0) {
            selector.val(selectedCity).change();
            selectedCity = 0;
        }
        else if (cities.length === 1) {
            selector.val(cities[0].key).change();
        }
    }

    function onGetStates(states) {
        loadSelector("#slct_" + instanceIdStates, states);
        var selector = $("#slct_" + instanceIdStates);
        if (selectedState > 0) {
            selector.val(selectedState).change();
            selectedState = 0;
        }
        else if (states.length === 1) {
            selector.val(states[0].key).change();
        }
    }

    function setInitialValues() {
        instanceIdCountries = Math.random().toString().replace(".", "");
        instanceIdCities = Math.random().toString().replace(".", "");
        instanceIdRegions = Math.random().toString().replace(".", "");
        instanceIdStates = Math.random().toString().replace(".", "");
        instanceIdCountrySelected = Math.random().toString().replace(".", "");
        instanceIdCitySelected = Math.random().toString().replace(".", "");
        instanceIdRegionSelected = Math.random().toString().replace(".", "");
        instanceIdStateSelected = Math.random().toString().replace(".", "");
        countriesToSelect = settings.countriesToSelect;
        countrySelectedProperty = settings.countrySelectedProperty;
        citySelectedProperty = settings.citySelectedProperty;
        urlCities = settings.urlCities;
        urlRegions = settings.urlRegions;
        urlStates = settings.urlStates;
        defaultOptionText = settings.defaultOptionText;
        regionSelectedProperty = settings.regionSelectedProperty;
        stateSelectedProperty = settings.stateSelectedProperty;
        selectedCountry = settings.selectedCountry;
        selectedCity = settings.selectedCity;
        selectedState = settings.selectedState;
        selectedRegion = settings.selectedRegion;
        countryText = settings.countryText;
        regionText = settings.regionText;
        stateText = settings.stateText;
        cityText = settings.cityText;
        displayHorizontal = settings.displayHorizontal;
    }

    function loadInitialValues() {
        if (selectedCountry > 0) {
            var element = $("#slct_" + instanceIdCountries);
            element.val(selectedCountry);
            selectedCountry = 0;
            countryChanged();
        }
    }

    var init = function (startContainer, options) {
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton();
        loadSelector("#slct_" + instanceIdCountries, countriesToSelect);
        loadInitialValues();
        initializeEvents();
    };

    return {
        Init: init,
        LoadSelector: loadSelector,
    };
};