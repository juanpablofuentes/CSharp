var app = app || {};
app.constants = app.constants || {};

app.constants = (function ()
{
    var post = 'POST';
    var get = 'GET'; 
    var deleteMethodType = 'DELETE'; 
    var clickEvent = 'click';
    var changeEvent = 'change';
    var slct = '#slct_';
    var itemIdProperty = 'Value';
    var itemTextProperty = 'Text';
    var itemIdSecondaryProperty = 'ValueSecondary';
    var itemTextSecondaryProperty = 'TextSecondary';
    var tabulatedChar = "\t";
    var searchMinimumCharacters = 3;
    var autoComplete = 1;
    var queryTypePeopleFilterByCompany = 2;
    var queryTypePeopleWithoutCompanyAndSubContractor = 3;
    var queryTypeWorkCenterFilterByCompany = 4;
    var queryKnowledge = 5;
    var queryToolsType = 6;
    var queryCategories = 7;
    var queryPeopleTechnicians = 8;
    var queryPeopleExpense = 9;
    var queryStates = 10;
    var queryExpense = 11;

    return {
        Post: post,
        Get: get,
        ClickEvent: clickEvent,
        ChangeEvent: changeEvent,
        Slct: slct,
        QueryKnowledge: queryKnowledge,
        QueryTypePeopleWithoutCompanyAndSubContractor: queryTypePeopleWithoutCompanyAndSubContractor,
        QueryTypePeopleFilterByCompany: queryTypePeopleFilterByCompany,
        QueryTypeWorkCenterFilterByCompany: queryTypeWorkCenterFilterByCompany,
        QueryToolsType: queryToolsType,
        QueryPeopleExpense: queryPeopleExpense,
        QueryStates: queryStates,
        QueryExpense: queryExpense,
        AutoComplete: autoComplete,
        Delete: deleteMethodType,
        QueryGetCategories: queryCategories,
        QueryPeopleTechnicians: queryPeopleTechnicians,
        ItemIdProperty: itemIdProperty,
        ItemTextProperty: itemTextProperty,
        ItemIdSecondaryProperty: itemIdSecondaryProperty,
        ItemTextSecondaryProperty: itemTextSecondaryProperty,
        SearchMinimumCharacters: searchMinimumCharacters,
        TabulatedChar: tabulatedChar
    };
})();
var app = app || {};
app.config = app.config || {};

app.config = (function () {
    var urls =
    {
        getToolsType: '/api/ToolsType',
        getPeopleExpense: '/api/ExpenseTicket',
        getStates: '/api/ExpenseTicket/States',
        getExpense: '/api/ExpenseTicket/GetExpense',
        getPostalCodes: '/api/Zones/GetPostalCodesZone',
        getKnowledge: '/api/Knowledge',
        getWorkCenterByCompany: '/api/WorkCenter',
        getLanguages: '/api/Language',
        workOrderStatusCanDelete: '/api/WorkOrderStatus/CanDelete',
        externalWorkOrderStatusCanDelete: '/api/ExternalWorkOrderStatus/CanDelete',
        getCalendarEvents: '/api/CalendarEvents',
        queueCanDelete: '/api/Queue/CanDelete',
        peopleCollectionCanDelete: '/api/PeopleCollection/CanDelete',
        getPeople: '/api/People',
        getCategories: '/api/WorkOrderCategoriesCollection',
        closureCodeCanDelete: '/api/ClosureCode/CanDelete',
        closingCodeCanDelete: '/api/ClosingCode/CanDelete',
        originsCanDelete: '/api/Origins/CanDelete',
        peopleCalendar: '/api/PeopleCalendar',
        calendarEvent: '/api/CalendarEvents',
        saveCalendarEvents: '/SaveEvent',
        getAvariableCalendars: '/api/PeopleCalendar/AvailableCalendars',
        addCalendar: '/api/PeopleCalendar/AddCalendar',
        workOrderCategoryCalendar: '/api/WorkOrderCategoryCalendar',
        getWOCategoryAvariableCalendars: '/api/WorkOrderCategoryCalendar/AvailableCalendars',
        addWOCategoryCalendar: '/api/WorkOrderCategoryCalendar/AddCalendar',
        projectCalendar: '/api/ProjectCalendar',
        getProjectAvailableCalendars: '/api/ProjectCalendar/AvailableCalendars',
        addProjectCalendar: '/api/ProjectCalendar/AddCalendar',
        projectCanDelete: '/api/Project/CanDelete',
        finalClientsCanDelete: '/api/FinalClients/CanDelete',
        CollectionTypeWOCanDelete: '/api/CollectionTypeWorkOrders/CanDelete',
        WorkOrderTypesCanDelete: '/api/CollectionTypeWorkOrders/CanDeleteTreeLevel',
        PredefinedServicesCanDelete: '/api/PredefinedServices/CanDelete',
        extraFieldsCanDelete: '/api/ExtraFields/CanDelete',
        getExtraFields: '/api/ExtraFields',
        getAssetStatuses: '/api/AssetStatuses',
        getModels: '/api/Models',
        getModelsFiltered: '/api/Models/Filter',
        getBrands: '/api/Brands',
        getFamilies: '/api/Families',
        getSubFamilies: '/api/SubFamilies',
        getSubFamiliesFiltered: '/api/SubFamilies/Filter',
        getSites: '/api/Sites',
        getSitesFiltered: '/api/Sites/Filter',
        getSiteUserFiltered: '/api/SiteUser/Filter',
        getFinalClients: '/api/FinalClients',
        finalClientsCalendar: '/api/FinalClientSiteCalendar',
        getFinalClientsAvailableCalendars: '/api/FinalClientSiteCalendar/AvailableCalendars',
        addFinalClientsCalendar: '/api/FinalClientSiteCalendar/AddCalendar',
        getProjects: '/api/Project',
        getWorkOrderCategory: '/api/WorkOrderCategory',
        getState: '/api/State',
        getPeopleTechnicians: '/api/People/PeopleTechnicians',
        workOrderCategoryCanDelete: '/api/WorkOrderCategory/CanDelete',
        pointRateCanDelete: '/api/PointRate/CanDelete',
        workOrderCategoriesCollectionCanDelete: '/api/WorkOrderCategoriesCollection/CanDelete',
        siteUserCanDelete: '/api/SiteUser/CanDelete',
        collectionsExtraFieldCanDelete: '/api/CollectionsExtraField/CanDelete',
        sitesCanDelete: '/api/Sites/CanDelete',
        getAssets: '/api/Assets',
        getProjectRelated: '/api/Project/GetRelatedInfo',
        SiteCalendars: '/api/SitesCalendar',
        getSiteAvailableCalendars: '/api/SitesCalendar/AvailableCalendars',
        AddSiteCalendar: '/api/SitesCalendar/AddCalendar',
        getHasSLADates: '/api/WorkOrderCategory/HasSLADates',
        getSummary: '/api/WorkOrder/GetSummaryInfo',
        CheckPostalCodes: '/api/PostalCodes/CheckPostalCode',
        GetPostalCodeByCity: '/api/PostalCodes/GetPostalCodeByCity',
        getProjectAdvancedSearch: '/api/Project/AdvancedSearch',
        getClientSiteAdvancedSearch: '/api/FinalClients/AdvancedSearch',
        getSiteAdvancedSearch: '/api/Sites/AdvancedSearch',
        getSubWO: '/api/WorkOrder/GetSubWO',
        getAssetsLocationById: '/api/Assets/GetLocationById',
        getBillWO: '/api/WorkOrder/GetBill/',
        getBillLineWO: '/api/WorkOrder/GetBillLine/',
        itemsCanDelete: '/api/Items/CanDelete',
        getTask: '/api/Tasks/GetTask',
        GetTriggerTypeById: '/api/Trigger/GetTriggerTypeById',
        getAssetsWorkOrder: '/api/WorkOrder/GetAssetsWorkOrder',
        getPostalCodeById: '/api/PostalCodes/GetPostalCodeById',
        getMunicipalityById: '/api/Municipality/GetMunicipalityById',
        warehousesCanDelete: '/api/Warehouses/CanDelete',
        getGaugesData: '/api/ServiceGauges/ServiceState',
        getItems: '/api/Items',
        getClient: '/api/Client',
        GetProjectAll: '/api/Project/GetProjectAll',
        GetWoCategoryAll: '/api/WorkOrderCategory/GetWoCategoryAll',
        updateTask: '/api/Tasks/UpdateTask',
        getAllPermissions: '/api/PermissionsTasks/GetAllPermissions',
        getEmptyTask: '/api/Tasks/GetEmptyTask',
        createTask: '/api/Tasks/CreateTask',
        getTasksForWorkOrder: '/api/Tasks/GetTasksForWorkOrder',
        executeTask: '/api/Tasks/ExecuteTask',
        getGaugesEconomic: '/api/ServiceGauges/EconomicRep',
        getConfiguredViews: '/api/WorkOrderFilter/GetConfiguredViews',
        getConfiguredViewTechnicians: '/api/WorkOrderFilter/GetTechnicians',
        getConfiguredViewGetGroups: '/api/WorkOrderFilter/GetGroups',
        MailTemplateCanDelete: '/api/MailTemplate/CanDelete',
        getPerformTaskInfo: '/api/PerformTask/GetInfo',
        getWorkOrderDerivatedByTaskId: '/api/WorkOrderDerivated/GetByTaskId',
        getSupplantTechnician: '/api/PerformTask/GetTechnician'
    };

    return {
        Urls: urls
    };
})();
var app = app || {};
app.common = app.common || {};
app.common.ui = app.common.ui || {};

app.common.ui = (function () {
    var loadSelector = function loadSelector(selectorId, items) {
        var select = $('#' + selectorId);
        select.empty();
        if (items.length > 0) {
            for (var i = 0; i < items.length; i++) {
                var option = "<option value='" + items[i].key + "'>" + items[i].value + "</option>";
                select.append($(option));
            }
        }
        return select;
    };

    var loadSelectorKeyValue = function loadSelector(selectorId, items, key, value, withVoidLine, voidId) {
        var select = $('#' + selectorId);
        select.empty();
        if (items.length > 0) {
            if (withVoidLine) {
                if (voidId === undefined || voidId === null)
                    voidId = 0;
                var defaultOption = "<option value='" + voidId + "'>_</option>";
                select.append($(defaultOption));
            }
            for (var i = 0; i < items.length; i++) {
                var option = "<option value='" + items[i][key] + "'>" + items[i][value] + "</option>";
                select.append($(option));
            }
        }
        return select;
    };

    var loadSelectorWithVoidLine = function (selectorId, items, defaultOptionText) {
        var select = $('#' + selectorId);
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
    };

    var getCookie = function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    };

    var changeCheckbox = function (that) {
        $(that).prop("checked") ? $(that).val("true") : $(that).val("false");
    };

    var getDataById = function (data, url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            async: false,
            data: { id: data },
            success: success,
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.options.closeButton = 'False';
                toastr.options.newestOnTop = 'False';
                var optionsOverride = {};
                toastr['error']("Error", thrownError.message, optionsOverride);
            }
        });
    };

    var getDataByIdWaitResponse = function (data, url) {
        var res = null;
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            async: false,
            data: { id: data },
            success: function (result) {
                res = result;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.options.closeButton = 'False';
                toastr.options.newestOnTop = 'False';
                var optionsOverride = {};
                toastr['error']("Error", thrownError.message, optionsOverride);
            }
        });
        return res;
    };

    return {
        LoadSelector: loadSelector,
        LoadSelectorKeyValue: loadSelectorKeyValue,
        LoadSelectorWithVoidLine: loadSelectorWithVoidLine,
        ChangeCheckbox: changeCheckbox,
        GetDataById: getDataById,
        GetDataByIdWaitResponse: getDataByIdWaitResponse
    };
})();
var app = app || {};
app.common = app.common || {};
app.common.ui = app.common.ui || {};
app.common.ui.constants = app.common.ui.constants || {};
app.common.ui.constants = (function() {

    var sideMenuId = '#side-menu';
    var body = "body";
    var sideBarMenuId = '#sideBarMenu';
    var buttonFilterClass = ".button-filter";
    var buttonClearFilterId = "#btnClear";
    var spanishId = 1;
    var catalanId = 2;
    var englishId = 3;

    return {
        SideMenuId: sideMenuId,
        Body: body,
        SideBarMenuId: sideBarMenuId,
        ButtonFilterClass: buttonFilterClass,
        ButtonClearFilterId: buttonClearFilterId,
        SpanishId: spanishId,
        CatalanId : catalanId,
        EnglishId : englishId,
    };
})();