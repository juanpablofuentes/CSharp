var app = app || {};
app.sitePostalCodeControl = app.sitePostalCodeControl || {};
app.sitePostalCodeControl  = function () {
    var postalCode;
    var cityId;
    var muniId;
    var stateId;
    var regionId;
    var countryId;
    var settings;
    var countries;
    var urlGetCities;
    var urlGetPostalCodesByCityId;
    var urlCheckPostalCode;
    var urlGetCity;
    var urlGetMunicipality;
    var countrySelectedProperty;
    var citySelectedProperty;
    var stateSelectedProperty;
    var regionSelectedProperty;
    var urlCities;
    var urlRegions;
    var urlStates;
    var defaultOptionText = "-";
    var countryText;
    var regionText;
    var stateText;
    var cityText;
    var editPostalCode = 0;

    var getdata = function (_data, _url, _success) {
        $.ajax({
            url: _url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { id: _data },
            success: _success
        });
    }

    var setInitialValues = function () {
        urlGetCities = settings.urlGetCities;
        urlGetPostalCodesByCityId = settings.urlGetPostalCodesByCityId;
        urlCheckPostalCode = settings.urlCheckPostalCode;
        urlGetCity = settings.urlGetCity;
        urlGetMunicipality = settings.urlGetMunicipality;
        countries = settings.countries;
        countrySelectedProperty = settings.countrySelectedProperty;
        citySelectedProperty = settings.citySelectedProperty;
        stateSelectedProperty = settings.stateSelectedProperty;
        regionSelectedProperty = settings.regionSelectedProperty;
        urlCities = settings.urlCities;
        urlRegions = settings.urlRegions;
        urlStates = settings.urlStates;
        defaultOptionText = settings.defaultOptionText;
        countryText = settings.countryText;
        regionText = settings.regionText;
        stateText = settings.stateText;
        cityText = settings.cityText;
        countryId = settings.selectedCountry;
        regionId = settings.selectedRegion;
        stateId = settings.selectedState;
        cityId = settings.selectedCity;
        muniId = settings.selectedMunicipality;
        postalCode = settings.postalCode;
    }
    
    var init = function (options) {
        settings = $.extend({}, options);
        setInitialValues();

        $('#PostalCodeConfirm').change(function () {
            editPostalCode = 1;
            postalCode = $('#PostalCodeConfirm').val();
            var url = urlCheckPostalCode;
            getdata(postalCode, url, getCity);
        });

        $('#municipalities_combo').change(function () {
            if ($('#municipalities_combo').val() != 0) {
                cityId = $('#municipalities_combo').val();
                $('[name="GeolocationDetailViewModel.MunicipalitySelected"]').val(cityId);
                var url = urlGetPostalCodesByCityId;
                getdata(cityId, url, inputPostalCode);
            }
            
        });
    };

    var inputPostalCode = function (PostalCode) {
        if (PostalCode) {
            $('#PostalCodeConfirm').val(PostalCode.postalCode);
        }
    }  

    var getCity = function (PostalCode) {
        if (PostalCode != null) {
            cityId = PostalCode.cityId;
            $('[name="GeolocationDetailViewModel.MunicipalitySelected"]').val(cityId);
            var url = urlGetCity;
            getdata(cityId, url, getMunicipality);
        }
    }

    var getMunicipality = function (City) {
        if (City != null) {
            muniId = City.municipalityId;
            var url = urlGetMunicipality;
            getdata(muniId, url, reloadCombos);
        }
    }
    
    var reloadCombos = function (Municipality) {
        if (Municipality != null) {
            stateId = Municipality.stateId;
        }
        regionId = $('[name="GeolocationDetailViewModel.RegionSelected"]').val();
        countryId = $('[name="GeolocationDetailViewModel.CountrySelected"]').val();

        new countryCitySelector().Init("#countrySelector",
            {
                countriesToSelect: countries,
                countrySelectedProperty: countrySelectedProperty,
                citySelectedProperty: citySelectedProperty,
                stateSelectedProperty: stateSelectedProperty,
                regionSelectedProperty: regionSelectedProperty,
                urlCities: urlCities,
                urlRegions: urlRegions,
                urlStates: urlStates,
                defaultOptionText: defaultOptionText,
                selectedCountry: countryId,
                selectedCity: muniId,
                selectedState: stateId,
                selectedRegion: regionId,
                countryText: countryText,
                regionText: regionText,
                stateText: stateText,
                cityText: cityText,
                onChangeCountry: app.sitePostalCodeControl.onChangeCountry,
                onChangeRegion: app.sitePostalCodeControl.onChangeRegion,
                onChangeState: app.sitePostalCodeControl.onChangeState,
                onChangeCity: app.sitePostalCodeControl.onChangeCity,
            });
    }

    var onChangeCountry = function (id) {
        countryId = id;
        checkPostalCodeDisabled();
        if (editPostalCode == 1 || id == 0) {
            clearFields();
        }
    }

    var onChangeRegion = function (id) {
        regionId = id;
        checkPostalCodeDisabled();
        if (editPostalCode == 1 || id == 0) {
            clearFields();
        }
    }

    var onChangeState = function (id) {
        stateId = id;
        checkPostalCodeDisabled();
        if (editPostalCode == 1 || id == 0) {
            clearFields();
        }
    }

    var onChangeMunicipality = function (id) {
        muniId = id;
        if (editPostalCode == 1 || id == 0) {
            clearFields();
            editPostalCode = 0;
        }
        checkPostalCodeDisabled();
        loadMunicipalities(id);
    }

    var checkPostalCodeDisabled = function () {
        if ($('[name="GeolocationDetailViewModel.RegionSelected"]').val() == 0) {
            $('#PostalCodeConfirm').prop('disabled', true);
        } else {
            $('#PostalCodeConfirm').prop('disabled', false);
        }
    }

    var loadMunicipalities = function (id) {
        var url = urlGetCities;
        getdata(id, url, loadComboCities);
    }

    var loadComboCities = function (cities) {
        loadSelector("#municipalities_combo", cities);
        cityId = $('[name="GeolocationDetailViewModel.MunicipalitySelected"]').val();
        $("#municipalities_combo").val(cityId).change();
    };

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

    var clearFields = function () {
        $('#PostalCodeConfirm').val(0);
        $('#municipalities_combo').empty();
        $('[name="GeolocationDetailViewModel.MunicipalitySelected"]').val(0);
    }

    return {
        Init: init,
        LoadMunicipalities: loadMunicipalities,
        onChangeCountry: onChangeCountry,
        onChangeRegion: onChangeRegion,
        onChangeState: onChangeState,
        onChangeCity: onChangeMunicipality,
    };
}();