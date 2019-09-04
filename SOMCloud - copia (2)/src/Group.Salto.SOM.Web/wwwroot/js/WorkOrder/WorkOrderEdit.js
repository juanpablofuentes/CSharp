var app = app || {};
app.WorkOrderEdit = app.WorkOrderEdit || {};

app.WorkOrderEdit = (function () {
    var options;
    var project, technician, clientSite, site, asset, userSite, workOrderTypeItems;
    var projectSearch, clientSiteSearch, siteSearch;

    var init = function (_options) {
        options = _options;
        initializeEvents(options);
        calendarLoad(options.calendar);
        initDerivedWorkOrder();
    };

    function initDerivedWorkOrder() {
        if (options.isDerived) {
            $('#' + options.fields.inheritProject).on('click', clickInheritProject);
            $('#' + options.fields.inheritTechnician).on('click', clickInheritTechnician);
            if (options.modeActionType == "EditDerived") {
                clickInheritProject();
                clickInheritTechnician();
            }
        }
    }

    function clickInheritProject() {
        var value = $('#' + options.fields.inheritProject).prop("checked");
        if (value) {
            $('#projectdiv').addClass('d-none');
            $('#workOrdercategorydiv').addClass('d-none');
            $('#workorderedittypediv').addClass('d-none');
        }
        else {
            $('#projectdiv').removeClass('d-none');
            $('#workOrdercategorydiv').removeClass('d-none');
            $('#workorderedittypediv').removeClass('d-none');
        }
    }

    function clickInheritTechnician() {
        var value = $('#' + options.fields.inheritTechnician).prop("checked");
        if (value) {
            $('#techniciandiv').addClass('d-none');
        }
        else {
            $('#techniciandiv').removeClass('d-none');
        }
    }

    var initEdit = function () {
        var cs = clientSite.GetSelectedOption();
        var s = site.GetSelectedOption();

        if (cs !== undefined) {
            site.ChangeData(cs.key, false);
            siteSearch.ChangeData(cs.key);
        }

        if (s !== undefined) {
            clientSite.ChangeData(s.key, false);
            clientSiteSearch.ChangeData(s.key);
            asset.ChangeData(s.key, false);
            userSite.ChangeData(s.key, false);
        }
    };

    function initializeEvents(options) {
        $('#OpenProjectAdvancedSearch').on('click', projectAdvancedSearchClick);
        $('#OpenClientSiteAdvancedSearch').on('click', clientSiteAdvancedSearchClick);
        $('#OpenSiteAdvancedSearch').on('click', siteAdvancedSearchClick);
        $('#' + options.fields.textRepair).on('keypress', validateCharacters);
        $('#' + options.fields.observations).on('keypress', validateCharacters);
        $('#btnSendMessage').on('click', validate);
        initAdvancedSearch();
        initializeProjectAutocomplete(options.projectCombo);
        initializeTechnicianAutocomplete(options.technicianCombo);
        initializeClientSiteAutocomplete(options.clientSiteCombo);
        initializeSiteAutocomplete(options.siteCombo);
        initializeAssetAutocomplete(options.assetCombo);
        initializeUserSiteAutocomplete(options.userSiteCombo);
    }

    function replaceValues(item) {
        $("#" + item).val($("#" + item).val().replace(/</g, "&lt;").replace(/>/g, "&gt;"));
    }

    function validate() {
        replaceValues(options.fields.textRepair);
        replaceValues(options.fields.observations);
    }

    function validateCharacters(e) {
        var code = (e.keyCode ? e.keyCode : e.which);
        console.log(code);
        return ((code == 13) || (code == 60) || (code == 62) || (code == 95) || (code == 189)) ? false : true;
    }

    function initAdvancedSearch() {
        projectSearch = new advancedSearch();
        clientSiteSearch = new advancedSearch();
        siteSearch = new advancedSearch();
    }

    function projectAdvancedSearchClick() {
        projectSearch.Init(options.projectSearch);
    }

    function clientSiteAdvancedSearchClick() {
        clientSiteSearch.Init(options.clientSiteSearch);
    }

    function siteAdvancedSearchClick() {
        siteSearch.Init(options.siteSearch);
    }

    function onProjectSearchChoose(item) {
        options.projectCombo.selectedValues.key = item.id;
        options.projectCombo.selectedValues.value = item.name;
        initializeProjectAutocomplete(options.projectCombo);
        projectChange(options.projectCombo.selectedValues, false);
    }

    function onClientSiteSearchChoose(item) {
        options.clientSiteCombo.selectedValues.key = item.id;
        options.clientSiteCombo.selectedValues.value = item.name;
        initializeClientSiteAutocomplete(options.clientSiteCombo);
    }

    function onSiteSearchChoose(item) {
        options.siteCombo.selectedValues.key = item.id;
        options.siteCombo.selectedValues.value = item.name;
        initializeSiteAutocomplete(options.siteCombo);
    }

    function onAdvancedSearchSelectChange(item) {
        if (item.details[2] !== undefined || item.details[2] !== '') {
            var value = item.details[2].split('|');
            if (value.length == 2) {
                var postalCode = app.common.ui.GetDataByIdWaitResponse(value[0], app.config.Urls.getPostalCodeById);
                var municipality = app.common.ui.GetDataByIdWaitResponse(value[1], app.config.Urls.getMunicipalityById);
                item.details[2] = "(" + postalCode + ") " + municipality;
            }
        }
    }

    function initializeProjectAutocomplete(options) {
        project = new autocomplete();
        project.Init("#ProjectComboFilter",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: options.selectedValues,
                urlData: app.config.Urls.getProjects,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getStatusFilter,
                ajaxMethodType: app.constants.Post,
                setEvent: projectChange,
                onVoidClean: true,
                voidValue: 0,
                removePadding: true
            });

        return project;
    }

    function initializeTechnicianAutocomplete(options) {
        technician = new autocomplete();
        technician.Init("#TechnicianComboFilter",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: options.selectedValues,
                urlData: app.config.Urls.getPeopleTechnicians,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getStatusFilter,
                ajaxMethodType: app.constants.Post,
                onVoidClean: true,
                voidValue: 0,
                removePadding: true
            });
        return technician;
    }

    function initializeClientSiteAutocomplete(options) {
        clientSite = new autocomplete();
        clientSite.Init("#ClientSiteComboFilter",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: options.selectedValues,
                urlData: app.config.Urls.getFinalClients,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getStatusFilter,
                ajaxMethodType: app.constants.Post,
                setEvent: clientSiteChange,
                onVoidClean: true,
                voidValue: 0,
                removePadding: true
            });
        return clientSite;
    }

    function initializeSiteAutocomplete(options) {
        site = new autocomplete();
        site.Init("#SiteIdComboFilter",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: options.selectedValues,
                urlData: app.config.Urls.getSites,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 2,
                nColumns: false,
                getDataMethod: getStatusFilter,
                ajaxMethodType: app.constants.Post,
                setEvent: siteChange,
                onVoidClean: true,
                voidValue: 0,
                removePadding: true
            });
        return site;
    }

    function initializeAssetAutocomplete(options) {
        asset = new autocomplete();
        asset.Init("#AssetComboFilter",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: options.selectedValues,
                urlData: app.config.Urls.getAssets,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 2,
                nColumns: false,
                getDataMethod: getStatusFilter,
                ajaxMethodType: app.constants.Post,
                setEvent: assetChange,
                onVoidClean: true,
                voidValue: 0,
                removePadding: true
            });
        return asset;
    }

    function initializeUserSiteAutocomplete(options) {
        userSite = new autocomplete();
        userSite.Init("#UserSiteComboFilter",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: options.selectedValues,
                urlData: app.config.Urls.getSiteUserFiltered,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getSiteUsersFilter,
                ajaxMethodType: app.constants.Post,
                setEvent: userSiteChange,
                isEnabled: options.enabled,
                onVoidClean: true,
                voidValue: 0,
                removePadding: true
            });
        return userSite;
    }

    function getStatusFilter(text, additionalData) {
        if (additionalData === undefined)
            additionalData = null;
        var data = {
            QueryType: app.constants.AutoComplete,
            QueryTypeParameters: { value: additionalData, Text: text }
        };
        return data;
    }

    function getSiteUsersFilter(text, additionalData) {
        var data = {
            Text: text,
            Selected: additionalData !== undefined ? additionalData : []
        };
        return data;
    }

    var calendarLoad = function (calendarOptions) {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(calendarOptions.objects);
        datePicker.showTime();
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, true), "%Y-%m-%d %H:%i");
    };

    function clientSiteChange(item, initialLoad) {
        if (!initialLoad) {
            if (item !== null && item !== undefined) {
                site.ChangeData(item.key, false);
                siteSearch.ChangeData(item.key);
            }
            else {
                site.ChangeData(null, false);
                siteSearch.ChangeData(null);
            }
        }
    }

    function siteChange(item, initialLoad) {
        if (userSite !== undefined) {
            userSite.Enabled(false);
        }
        if (!initialLoad) {
            if (item !== null && item !== undefined) {
                item.value = null;
                if (clientSite.GetSelectedOption() === undefined) {
                    clientSite.ChangeData(item.key, false);
                    clientSite.SearchByDefault();
                }
                clientSiteSearch.ChangeData(item.key);

                if (asset.GetSelectedOption() === undefined) {
                    asset.ChangeData(item.key, false);
                }
                if (userSite.GetSelectedOption() === undefined) {
                    userSite.ChangeData(item.key, false);
                }
            }
            else {
                if (clientSite.GetSelectedOption() === undefined) {
                    clientSite.ChangeData(null, false);
                }
                clientSiteSearch.ChangeData(null, false);
                if (asset.GetSelectedOption() === undefined) {
                    asset.ChangeData(null, false);
                }
                options.userSiteCombo.selectedValues.key = null;
                options.userSiteCombo.selectedValues.value = null;
                initializeUserSiteAutocomplete(options.userSiteCombo);
            }
        }
    }

    function assetChange(item, initialLoad) {
        if (!initialLoad && item !== null && item !== undefined) {
            if (clientSite.GetSelectedOption() === undefined && site.GetSelectedOption() === undefined) {
                app.common.ui.GetDataById(item.key, app.config.Urls.getAssetsLocationById, assetsLocationResponse);
            }
        }
    }

    function assetsLocationResponse(result) {

        if (result.finalClientId !== 0) {
            options.clientSiteCombo.selectedValues.key = result.finalClientId;
            options.clientSiteCombo.selectedValues.value = result.finalClientName;
            initializeClientSiteAutocomplete(options.clientSiteCombo);
        }

        if (result.locationClientId !== 0) {
            options.siteCombo.selectedValues.key = result.locationClientId;
            options.siteCombo.selectedValues.value = result.locationName;
            initializeSiteAutocomplete(options.siteCombo);
        }

        if (result.siteUserId !== 0) {
            options.userSiteCombo.selectedValues.key = result.siteUserId;
            options.userSiteCombo.selectedValues.value = result.siteUserName;
            initializeUserSiteAutocomplete(options.userSiteCombo);
        }
    }

    function userSiteChange(item, initialLoad) {
    }

    function projectChange(item, initialLoad) {
        if (item !== null && item !== undefined) {
            if (initialLoad) {
                app.common.ui.GetDataById(item.key, app.config.Urls.getProjectRelated, getProjectDataInitial);
            }
            else {
                app.common.ui.GetDataById(item.key, app.config.Urls.getProjectRelated, getProjectDataChange);
            }
        }
        else {
            $("#" + options.fields.workOrderCategory).empty();
            $("#" + options.fields.queue).val(0);
            $("#" + options.fields.workOrderStatus).val(0);
            disableWorkOrderTypes();
        }
    }

    function disableWorkOrderTypes() {
        $("#" + options.fields.workOrderType1).empty();
        $("#" + options.fields.workOrderType1).prop("disabled", true);
        $("#" + options.fields.workOrderType2).empty();
        $("#" + options.fields.workOrderType2).prop("disabled", true);
        $("#" + options.fields.workOrderType3).empty();
        $("#" + options.fields.workOrderType3).prop("disabled", true);
        $("#" + options.fields.workOrderType4).empty();
        $("#" + options.fields.workOrderType4).prop("disabled", true);
    }

    function getProjectDataChange(result) {
        workOrderTypeItems = result.workOrderTypes;
        disableWorkOrderTypes();
        $("#" + options.fields.queue).val(result.queueId);
        $("#" + options.fields.workOrderStatus).val(result.workOrderStatusId);
        app.common.ui.LoadSelectorKeyValue(options.fields.workOrderCategory, result.workOrderCategories, "id", "name", false);
        $("#" + options.fields.workOrderCategory).val(result.workOrderCategories[0].id).change();
        $("#" + options.fields.workOrderType1).prop("disabled", false);
        app.common.ui.LoadSelectorKeyValue(options.fields.workOrderType1, result.workOrderTypes, "id", "name", true);
    }

    function getProjectDataInitial(result) {
        workOrderTypeItems = result.workOrderTypes;
        app.common.ui.LoadSelectorKeyValue(options.fields.workOrderCategory, result.workOrderCategories, "id", "name", false);
        if (options.workOrderCategoryValue !== null && options.workOrderCategoryValue !== 0)
            $("#" + options.fields.workOrderCategory).val(options.workOrderCategoryValue).change();

        if (workOrderTypeItems.length > 0) {
            var data = null;
            var fatherIdIds = [];
            var ids = [];

            var value = options.workOrderTypeValue;
            if (value == 0)
                fillFirstType(0);
            else {
                do {
                    data = findElementOnTree(workOrderTypeItems, value);
                    value = data.fatherId;
                    fatherIdIds.push(value);
                    ids.push(data.id);
                }
                while (data.fatherId != null);

                var fid = fatherIdIds.length - 1;
                var fieldId = 1;

                fillFirstType(ids[fid]);
                fid--;
                fieldId++;

                do {
                    let data = findElementOnTree(workOrderTypeItems, fatherIdIds[fid]);
                    field = getWorkOrderTypeField(fieldId);
                    $("#" + field).prop("disabled", false);
                    if (data !== undefined) {
                        app.common.ui.LoadSelectorKeyValue(field, data.childs, "id", "name", true);
                    }
                    if (ids[fid] === ids[0])
                        $("#" + field).val(ids[fid]).change();
                    else
                        $("#" + field).val(ids[fid]);

                    fid--;
                    fieldId++;
                }
                while ((fieldId - 1) < fatherIdIds.length);
            }
        }
    }

    function fillFirstType(value) {
        var field = getWorkOrderTypeField(1);
        $("#" + field).prop("disabled", false);
        if (workOrderTypeItems !== undefined) {
            app.common.ui.LoadSelectorKeyValue(field, workOrderTypeItems, "id", "name", true);
            if (value != 0)
                $("#" + field).val(value);
        }
    }

    var onWorkOrderTypeChange = function (elem, nextElem) {
        var value = $("#" + elem.id).val();
        if (value != 0) {
            $("#" + nextElem).prop("disabled", false);
            var data = findElementOnTree(workOrderTypeItems, value);
            if (data !== undefined) {
                app.common.ui.LoadSelectorKeyValue(nextElem, data.childs, "id", "name", true);
            }
        }
        else {
            $("#" + nextElem).val(0).change();
            $("#" + nextElem).prop("disabled", true);
            $("#" + nextElem).empty();
        }
    };

    var onWorkOrderCategoryChange = function (elem) {
        var value = $(elem).val();
        app.common.ui.GetDataById(value, app.config.Urls.getHasSLADates, getSLAData);
    };

    function getSLAData(result) {
        $("#" + options.fields.DateResponseSLAVisible).val(result[0]);
        $("#" + options.fields.DateResolutionSLAVisible).val(result[1]);
        $("#" + options.fields.DateUnansweredSLAPenaltyVisible).val(result[2]);
        $("#" + options.fields.DateWithoutPenaltyResolutionSLAVisible).val(result[3]);

        if (result[0]) $('#DateResponseSLA').removeClass('d-none'); else $('#DateResponseSLA').addClass('d-none');
        if (result[1]) $('#DateResolutionSLA').removeClass('d-none'); else $('#DateResolutionSLA').addClass('d-none');
        if (result[2]) $('#DateUnansweredSLAPenalty').removeClass('d-none'); else $('#DateUnansweredSLAPenalty').addClass('d-none');
        if (result[3]) $('#DateWithoutPenaltyResolutionSLA').removeClass('d-none'); else $('#DateWithoutPenaltyResolutionSLA').addClass('d-none');
    }

    function findElementOnTree(valuesOnFind, id) {
        for (var i = 0; i < valuesOnFind.length; i++) {
            if (valuesOnFind[i].id == id) {
                return valuesOnFind[i];
            }
            else if (valuesOnFind[i].childs !== undefined && valuesOnFind[i].childs !== null && valuesOnFind[i].childs.length > 0) {
                var item = findElementOnTree(valuesOnFind[i].childs, id);
                if (item !== undefined) {
                    return item;
                }
            }
        }
    }

    function getWorkOrderTypeField(index) {
        switch (index) {
            case 1:
                return options.fields.workOrderType1;
                break;
            case 2:
                return options.fields.workOrderType2;
                break;
            case 3:
                return options.fields.workOrderType3;
                break;
            case 4:
                return options.fields.workOrderType4;
                break;
        }
    }

    return {
        Init: init,
        OnWorkOrderTypeChange: onWorkOrderTypeChange,
        OnWorkOrderCategoryChange: onWorkOrderCategoryChange,
        OnProjectSearchChoose: onProjectSearchChoose,
        OnClientSiteSearchChoose: onClientSiteSearchChoose,
        OnSiteSearchChoose: onSiteSearchChoose,
        OnAdvancedSearchSelectChange: onAdvancedSearchSelectChange,
        InitEdit: initEdit,
    };
})();