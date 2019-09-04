var app = app || {};
app.workorderfilter = app.workorderfilter || {};

app.workorderfilter = (function () {

    var project, workOrderCategory, state, responsibles, arrAppliedFilters = [];
    var options;
    var params = '';
    var excel = {};
    var configurationViewId = 0;

    var clearFilterFieldsGeneric = function (e) {
        breadcrumbAppliedFilters(false);
        removeSessionStorage();
        $("#WorkOrderId").val('');
        $("#LocationCode").val('');
        $("#InternalIdentifier").val('');
        $("#ResolutionDateSla").val('');
        $("#SerialNumber").val('');
        $("#CreationDate").val('');
        $("#ActionDateDate").val('');
        $("#CreationStartDate").val('');
        $("#ActionDateStartDate").val('');
        $("#CreationEndDate").val('');
        $("#ActionDateEndDate").val('');
        app.multiselect.refresh(options.grid.workOrderFilterStatus);
        app.multiselect.refresh(options.grid.workOrderFilterQueue);
        project.RemoveAllSelectedValues();
        workOrderCategory.RemoveAllSelectedValues();
        state.RemoveAllSelectedValues();
        responsibles.RemoveAllSelectedValues();
    };

    var clearFilterFieldsToggle = function (e) {
        clearFilterFieldsGeneric();
        toggleFilter();
    };

    var clearFilterFieldsNoToggle = function (e) {
        clearFilterFieldsGeneric();
        reloadGrid(true);
    };

    var toggleFilter = function (e) {
        $("#filterWorkOrder").toggle("slow");
    };

    function initializeEvents() {
        $('.button-filter').on('click', toggleFilter);
        $('#btnClear').click(clearFilterFieldsToggle);
        $('#removeQuickFilters').click(clearFilterFieldsNoToggle);
        dhx.attachEvent("onLoadXMLError", my_error_handler);
        $('#exporttoexcel').on('click', exportToExcel);
        $('#exportalltoexcel').on('click', exportAllToExcel);
        $('#menu-burger').on('click', function (e) {
            let elem = e.target.parentElement.id;
            reziseGrid(elem);
        });
        $('.sidebar-minimizer').on('click', function (e) {
            reziseGrid('sidebar-minimized');
        });
        $('#cmbViewConfiguration').on('change', changeViewConfiguration);
        $('#editViewConfiguration').on('click', editViewConfiguration);
    }

    function changeViewConfiguration() {
        configurationViewId = $("#cmbViewConfiguration").val();
        clearFilterFieldsGeneric();
        initialValues();
        removeBreadcrumbAppliedFilters();
        loadWorkOrderGrid(options.grid);
    }

    function editViewConfiguration() {
        var id = $("#cmbViewConfiguration").val();
        if (id !== "0") {
            location.href = options.configurationView.urlConfigurationView + '?id=' + id;
        }
    }

    function initialValues() {
        $('#CreationStartDate').val("01/01/2018");
    }

    function my_error_handler(name, xhr) {
        hideSpinner();
        renderSimpleModal('modal-danger', 'modal-sm', 'Error', name);
        location.reload();
    }

    var init = function (_options) {
        options = _options;
        initializeEvents();
        initialValues();
        loadViews();
        loadQuickFilters();
        initializeProjectCombo();
        initializeWorkOrderCategory();
        initializeStateCombo();
        initializeResponsiblesCombo();
        loadWorkOrderGrid(_options.grid);
    };

    var loadViews = function () {
        app.common.ui.GetDataById(0, app.config.Urls.getConfiguredViews, loadViewsCallBack);
    };

    function loadViewsCallBack(result) {
        app.common.ui.LoadSelectorKeyValue("cmbViewConfiguration", result, "id", "name", true, -1);
        var data = result.find(r => r.isLocked == true);
        if (data !== undefined) {
            $("#cmbViewConfiguration").val(data.id);
        }

        var viewParam = sessionStorage.getItem("params");
        if (viewParam != null) {
            var data = viewParam.split('&');
            for (var i = 0; i < data.length; i++) {
                if (app.common.validations.StringContains(data[i], 'ConfigurationViewId')) {
                    var elem = data[i].split('=');
                    $("#cmbViewConfiguration").val(elem[1]);
                }
            }
        }
    }

    function loadQuickFilters()
    {
        params = sessionStorage.getItem("params");
        if (params != null) {
            var data = params.split('&');
            for (var i = 0; i < data.length; i++) {
                var elem = data[i].split('=');
                if (app.common.validations.StringContains(data[i], 'WorkOrderId')) {
                    $('#WorkOrderId').val(elem[1]);
                    setWorkOrderId(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'InternalIdentifier')) {
                    $('#InternalIdentifier').val(elem[1]);
                    setInternalIdentifier(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'SerialNumber')) {
                    $('#SerialNumber').val(elem[1]);
                    setSerialNumber(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'LocationCode')) {
                    $('#LocationCode').val(elem[1]);
                    setLocationCode(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'ResolutionDateSla')) {
                    $('#ResolutionDateSla').val(elem[1]);
                    setResolutionDateSla(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'CreationStartDate')) {
                    $('#CreationStartDate').val(elem[1]);
                    setCreationStartDate(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'CreationEndDate')) {
                    $('#CreationEndDate').val(elem[1]);
                    setCreationEndDate(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'CreationDate')) {
                    $('#CreationDate').val(elem[1]);
                    setCreationEndDate(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'ActionDateStartDate')) {
                    $('#ActionDateStartDate').val(elem[1]);
                    setActionDateStartDate(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'ActionDateEndDate')) {
                    $('#ActionDateEndDate').val(elem[1]);
                    setActionDateEndDate(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'ActionDateDate')) {
                    $('#ActionDateDate').val(elem[1]);
                    setActionDateDate(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'WorkOrderStatus')) {
                    app.multiselect.LoadByIds(elem[1], options.grid.workOrderFilterStatus);
                    setWorkOrderStatus(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'WorkOrderQueue')) {
                    app.multiselect.LoadByIds(elem[1], options.grid.workOrderFilterQueue);
                    setQueue(elem[1]);
                }
                else if (app.common.validations.StringContains(data[i], 'ProjectIds')) {
                    let json = sessionStorage.getItem('projectjson');
                    if (json != null) {
                        options.projectCombo.selectedItems = JSON.parse(json);
                        setProject(elem[1]);
                    }
                }
                else if (app.common.validations.StringContains(data[i], 'WorkOrderCategoryIds')) {
                    let json = sessionStorage.getItem('workOrderCategoryjson');
                    if (json != null) {
                        options.workOrderCategoryCombo.selectedItems = JSON.parse(json);
                        setWorkOrderCategory(elem[1]);
                    }
                }
                else if (app.common.validations.StringContains(data[i], 'StateIds')) {
                    let json = sessionStorage.getItem('statejson');
                    if (json != null) {
                        options.statesCombo.selectedItems = JSON.parse(json);
                        setState(elem[1]);
                    }
                }

                else if (app.common.validations.StringContains(data[i], 'ResponsiblesIds')) {
                    let json = sessionStorage.getItem('responsiblesjson');
                    if (json != null) {
                        options.responsobleCombo.selectedItems = JSON.parse(json);
                        setResponsibles(elem[1]);
                    }
                }
            }
        }
    }

    var getParams = function (isQuickFilter) {
        removeBreadcrumbAppliedFilters();
        removeSessionStorage();
        params = 'dhx_no_header=1';
        datePicker.loadUserLanguage('en');
        excel = {};
        excel.WorkOrderSearch = {};
        excel.dhx_no_header = 1;

        params += "&isQuickFilter=" + isQuickFilter;
        excel.isQuickFilter = isQuickFilter;

        if (configurationViewId != 0) {
            params += '&ConfigurationViewId=' + configurationViewId;
            excel.ConfigurationViewId = configurationViewId;
        }
       
        if (!app.common.validations.IsEmpty($('#WorkOrderId').val())) {
            let woid = $('#WorkOrderId').val();
            params += '&WorkOrderId=' + woid;
            setWorkOrderId(woid);
        }

        if (!app.common.validations.IsEmpty($('#InternalIdentifier').val())) {
            let internalIdentifier = $('#InternalIdentifier').val();
            params += '&InternalIdentifier=' + internalIdentifier;
            setInternalIdentifier(internalIdentifier);
        }

        if (!app.common.validations.IsEmpty($('#SerialNumber').val())) {
            let serialNumber = $('#SerialNumber').val();
            params += '&SerialNumber=' + serialNumber;
            setSerialNumber(serialNumber);
        }

        if (!app.common.validations.IsEmpty($('#LocationCode').val())) {
            var locationCode = $('#LocationCode').val();
            params += '&LocationCode=' + locationCode;
            setLocationCode(locationCode);
        }

        var resolutionDateSla = $('#ResolutionDateSla').val();
        if (!app.common.validations.IsEmpty(resolutionDateSla)) {
            var datesla = moment.parseZone(resolutionDateSla).local().format("YYYY-MM-DD");
            params += '&ResolutionDateSla=' + datesla.toString();
            setResolutionDateSla(datesla);
        }

        var creationStartDate = $('#CreationStartDate').val();
        if (!app.common.validations.IsEmpty(creationStartDate)) {
            let datevalue = moment.parseZone(creationStartDate).local().format("YYYY-MM-DD");
            params += '&CreationStartDate=' + datevalue;
            setCreationStartDate(datevalue);
        }

        var creationEndDate = $('#CreationEndDate').val();
        if (!app.common.validations.IsEmpty(creationEndDate)) {
            let datevalue = moment.parseZone(creationEndDate).local().format("YYYY-MM-DD");
            params += '&CreationEndDate=' + datevalue;
            setCreationEndDate(datevalue);
        }

        var creationDate = $('#CreationDate').val();
        if (!app.common.validations.IsEmpty(creationDate)) {
            let datevalue = moment.parseZone(creationDate).local().format("YYYY-MM-DD");
            params += '&CreationDate=' + datevalue;
            setCreationDate(datevalue);
        }

        var actionDateStartDate = $('#ActionDateStartDate').val();
        if (!app.common.validations.IsEmpty(actionDateStartDate)) {
            let datevalue = moment.parseZone(actionDateStartDate).local().format("YYYY-MM-DD");
            params += '&ActionDateStartDate=' + datevalue;
            setActionDateStartDate(datevalue);
        }

        var actionDateEndDate = $('#ActionDateEndDate').val();
        if (!app.common.validations.IsEmpty(actionDateEndDate)) {
            let datevalue = moment.parseZone(actionDateEndDate).local().format("YYYY-MM-DD");
            params += '&ActionDateEndDate=' + datevalue;
            setActionDateEndDate(datevalue);
        }

        var actionDateDate = $('#ActionDateDate').val();
        if (!app.common.validations.IsEmpty(actionDateDate)) {
            let datevalue = moment.parseZone(actionDateDate).local().format("YYYY-MM-DD");
            params += '&ActionDateDate=' + datevalue;
            setActionDateDate(datevalue);
        }

        var multiSelectWorkOrderStatusIds = app.multiselect.GetSelectedIds(options.grid.workOrderFilterStatus);
        if (!app.common.validations.IsEmpty(multiSelectWorkOrderStatusIds)) {
            params += '&WorkOrderStatus=' + multiSelectWorkOrderStatusIds;
            setWorkOrderStatus(multiSelectWorkOrderStatusIds);
        }

        var multiSelectWorkOrderQueueIds = app.multiselect.GetSelectedIds(options.grid.workOrderFilterQueue);
        if (!app.common.validations.IsEmpty(multiSelectWorkOrderQueueIds)) {
            params += '&WorkOrderQueue=' + multiSelectWorkOrderQueueIds;
            setQueue(multiSelectWorkOrderQueueIds);
        }

        var projectIds = project.GetSelectedIds();
        if (!app.common.validations.IsEmpty(projectIds)) {
            let json = project.GetSelectedValues();
            sessionStorage.setItem('projectjson', JSON.stringify(json));
            params += '&ProjectIds=' + projectIds;
            setProject(projectIds);
        }

        var workOrderCategoryIds = workOrderCategory.GetSelectedIds();
        if (!app.common.validations.IsEmpty(workOrderCategoryIds)) {
            let json = workOrderCategory.GetSelectedValues();
            sessionStorage.setItem('workOrderCategoryjson', JSON.stringify(json));
            params += '&WorkOrderCategoryIds=' + workOrderCategoryIds;
            setWorkOrderCategory(workOrderCategoryIds);
        }

        var stateIds = state.GetSelectedIds();
        if (!app.common.validations.IsEmpty(stateIds)) {
            let json = state.GetSelectedValues();
            sessionStorage.setItem('statejson', JSON.stringify(json));
            params += '&StateIds=' + stateIds;
            setState(stateIds)
        }

        var responsiblesIds = responsibles.GetSelectedIds();
        if (!app.common.validations.IsEmpty(responsiblesIds)) {
            let json = responsibles.GetSelectedValues();
            sessionStorage.setItem('responsiblesjson', JSON.stringify(json));
            params += '&ResponsiblesIds=' + responsiblesIds;
            setResponsibles(responsiblesIds);
        }

        if (window.s_col !== undefined) {
            params += "&DefaultOrder=false&orderBy=" + window.s_col;
            excel.DefaultOrder = false;
            excel.orderBy = window.s_col;
        }
        else {
            params += "&DefaultOrder=true";
            excel.DefaultOrder = true;
        }

        if (window.isAscending === undefined) {
            window.isAscending = "asc";
        }
        params += "&isAscending=" + (window.isAscending === "asc");
        excel.isAscending = (window.isAscending === "asc");

        var cultureInfo = getCookie("culture-code").toLowerCase();
        datePicker.loadUserLanguage(cultureInfo);

        sessionStorage.setItem('params', params);
        return params;
    };

    var getInitParams = function () {
        excel = {};
        excel.WorkOrderSearch = {};
        excel.DefaultOrder = true;
        params = '';

        if (!app.common.validations.IsEmpty($('#WorkOrderSearch_SearchString').val())) {
            fixedParams();
            var searchstring = $('#WorkOrderSearch_SearchString').val();
            params += '&WorkOrderSearch.SearchString=' + searchstring;
            excel.WorkOrderSearch.SearchString = searchstring;

            if (!app.common.validations.IsEmpty($('#WorkOrderSearch_SearchType').val())) {
                var searchtype = $('#WorkOrderSearch_SearchType').val();
                params += '&WorkOrderSearch.SearchType=' + searchtype;
                excel.WorkOrderSearch.SearchType = searchtype;
                if (searchtype != '0') {
                    params += "&isQuickFilter=true";
                    var texts = options.textWorkOrderSearch;
                    var index;
                    if (searchtype == 'WorkOrder') {
                        index = 0;
                    } else if (searchtype == 'Location') {
                        index = 1;
                    } else if (searchtype == 'Active') {
                        index = 2;
                    }
                    arrAppliedFilters.push(texts[index]);
                }
            }
        }
        else {
            params = sessionStorage.getItem("params");
            if (params == null) {
                fixedParams();
                if (configurationViewId <= 0) {
                    datePicker.loadUserLanguage('en');
                    var creationStartDate = $('#CreationStartDate').val();
                    if (creationStartDate !== '') {
                        let datevalue = moment.parseZone(creationStartDate).local().format("YYYY-MM-DD");
                        params += '&CreationStartDate=' + datevalue;
                        excel.CreationStartDate = datevalue;
                        let filterName = $('#CreationStartDate').data('filter');
                        arrAppliedFilters.push(filterName);
                    }
                    var cultureInfo = getCookie("culture-code").toLowerCase();
                    datePicker.loadUserLanguage(cultureInfo);
                }
            }
            else {
                params = params.replace("dhx_no_header=1", "dhx_no_header=0");
            }
        }
        breadcrumbAppliedFilters(true);

        sessionStorage.setItem('params', params);
        return params;
    };

    var search = function () {
        toggleFilter();
        $("#cmbViewConfiguration").val(-1);
        reloadGrid(true);
    };

    function fixedParams() {
        params = '';
        params = 'DefaultOrder=true';

        if (configurationViewId != 0) {
            params += '&ConfigurationViewId=' + configurationViewId;
            excel.ConfigurationViewId = configurationViewId;
        }
    }

    function initializeProjectCombo() {

        var projectContainerId = '#projectContainer';
        project = new autoCompleteListSelector();
        project.Init(projectContainerId,
            {
                selectedItems: options.projectCombo.selectedItems,
                urlPrincipalCombo: app.config.Urls.getProjects,
                collectionProperty: 'Projects',
                itemIdProperty: app.constants.ItemIdProperty,
                itemTextProperty: app.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.constants.ItemTextSecondaryProperty,
                minimumCharacters: 3,
                column1Text: options.projectCombo.column1Text,
                column2Text: options.projectCombo.column2Text,
                getDataMethod: options.projectCombo.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return project;
    }

    function initializeWorkOrderCategory() {
        var workOrderContainerId = '#worOrderCategoryContainer';
        workOrderCategory = new autoCompleteListSelector();
        workOrderCategory.Init(workOrderContainerId,
            {
                selectedItems: options.workOrderCategoryCombo.selectedItems,
                urlPrincipalCombo: app.config.Urls.getWorkOrderCategory,
                collectionProperty: 'WorkOrderCategories',
                itemIdProperty: app.constants.ItemIdProperty,
                itemTextProperty: app.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.constants.ItemTextSecondaryProperty,
                minimumCharacters: 0,
                column1Text: options.workOrderCategoryCombo.column1Text,
                column2Text: options.workOrderCategoryCombo.column2Text,
                getDataMethod: options.workOrderCategoryCombo.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return workOrderCategory;
    }

    function initializeStateCombo() {
        var stateContainerId = '#StatesContainer';
        state = new autoCompleteListSelector();
        state.Init(stateContainerId,
            {
                selectedItems: options.statesCombo.selectedItems,
                urlPrincipalCombo: app.config.Urls.getState,
                collectionProperty: 'States',
                itemIdProperty: app.constants.ItemIdProperty,
                itemTextProperty: app.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.constants.ItemTextSecondaryProperty,
                minimumCharacters: 0,
                column1Text: options.statesCombo.column1Text,
                column2Text: options.statesCombo.column2Text,
                getDataMethod: options.statesCombo.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return state;
    }

    function initializeResponsiblesCombo() {
        var responsiblesContainerId = '#ResponsiblesContainer';
        responsibles = new autoCompleteListSelector();
        responsibles.Init(responsiblesContainerId,
            {
                selectedItems: options.responsobleCombo.selectedItems,
                urlPrincipalCombo: app.config.Urls.getPeopleTechnicians,
                collectionProperty: 'Responsibles',
                itemIdProperty: app.constants.ItemIdProperty,
                itemTextProperty: app.constants.ItemTextProperty,
                itemIdSecondaryProperty: app.constants.ItemIdSecondaryProperty,
                itemTextSecondaryProperty: app.constants.ItemTextSecondaryProperty,
                minimumCharacters: 0,
                column1Text: options.responsobleCombo.column1Text,
                column2Text: options.responsobleCombo.column2Text,
                getDataMethod: options.responsobleCombo.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return responsibles;
    }

    function getWorkOrderFilter(text) {
        var data = {
            QueryType: app.constants.AutoComplete,
            QueryTypeParameters: {
                Text: text
            }
        };
        return data;
    }

    function getPeopleTechniciansFilter(text) {
        var data = {
            QueryType: app.constants.QueryPeopleTechnicians,
            QueryTypeParameters: {
                Text: text
            }
        };
        return data;
    }

    function loadWorkOrderGrid(optionGrid) {
        var myMenu = new dhtmlXMenuObject();
        myMenu.setIconsPath("/lib/dhtmlx/suite/codebase/common/imgs/");
        myMenu.renderAsContextMenu();
        myMenu.attachEvent("onClick", onContextMenuClick);
        myMenu.loadStruct('[{ id: "detail", text:"' + options.grid.detailText + '", img: \"details.png\"},' +
            '{ id: "edit", text: "' + options.grid.editText + '", img: \"edit.png\" }]', function () { });

        myGrid = new dhtmlXGridObject('gridbox');
        myGrid.setImagePath("/lib/dhtmlx/suite/codebase/imgs/dhxgrid_material/");
        myGrid.enableAutoHeight(true);
        myGrid.enableAutoWidth(false);
        myGrid.enableColumnAutoSize(false);
        myGrid.setSizes();
        if (myGrid.setColspan) {
            myGrid.attachEvent("onBeforeSorting", customColumnSort);
        }
        myGrid.enablePaging(true, optionGrid.paginationNumber, 5, "pagingArea", true, "recinfoArea");
        myGrid.setPagingSkin("bricks");
        myGrid.attachEvent("onXLE", showLoading);
        myGrid.attachEvent("onXLS", function () { showLoading(true) });
        myGrid.attachEvent("onRowDblClicked", onRowDblClicked);
        myGrid.attachEvent("onRowCreated", function (rId, rObj, rXml) {
            var columnsNum = myGrid.getColumnsNum() - 1;
            if (myGrid.getColLabel(columnsNum) === optionGrid.slaid) {
                var color = rObj._attrs.data[columnsNum];
                myGrid.setRowColor(rId, color);
            }
        });
        myGrid.attachEvent("onBeforePageChanged", function (ind, count) {
            myGrid.leaving_page = ind;
            return true;
        });
        myGrid.attachEvent("onPageChanged", function (ind, fInd, lInd) {
            excel.PosStart = fInd;
            var last_row = this.rowsBufferOutSize * myGrid.leaving_page - 1;
            var first_row = last_row - this.rowsBufferOutSize + 1;
            for (i = first_row; i <= last_row; i++) {
                myGrid.rowsBuffer[i] = null;
            }
        });
        myGrid.attachEvent("onKeyPress", onKeyPressed);
        myGrid.enableContextMenu(myMenu);
        myGrid.enableMultiselect(true);
        myGrid.init();
        myGrid.enableBlockSelection();
        myGrid.sortRows(2, "server", "des");
        myGrid.splitAt(0);
        var params = getInitParams();

        myGrid.load(optionGrid.url + "?" + params, "json").then(function () {
            myGrid.setSortImgState(true, 2, "desc");
            myGrid.setSizes();
            $('#menu-burger').on('click', function () {
                myGrid.setSizes();
            });
        });
    }

    function onKeyPressed(code, ctrl, shift) {
        if (code == 67 && ctrl) {
            if (!myGrid._selectionArea) return alert(options.grid.WorkOrderSelectBlockArea);
            myGrid.setCSVDelimiter(app.constants.tabulatedChar);
            myGrid.copyBlockToClipboard();
        }
        if (code == 86 && ctrl) {
            myGrid.setCSVDelimiter(app.constants.tabulatedChar);
            myGrid.pasteBlockFromClipboard();
        }
        return true;
    }

    function exportToExcel() {
        excel.ExportToExcel = true;
        excel.ExportAllToExcel = false;
        GenerateExcelObject(options.grid.urlToExcel, excel);
    }

    function exportAllToExcel() {
        excel.ExportToExcel = true;
        excel.ExportAllToExcel = true;
        GenerateExcelObject(options.grid.urlToExcel, excel);
    }

    function reloadGrid(isQuickFilter) {
        showLoading(true);
        myGrid.changePage(1);
        var params = getParams(isQuickFilter);

        breadcrumbAppliedFilters(true);
        myGrid.clearAndLoad(options.grid.url + "?" + params, "json");
        if (window.isAscending) {
            myGrid.setSortImgState(true, window.s_col, window.isAscending);
        }
    }

    function breadcrumbAppliedFilters(status) {
        if (status && $('#appliedFilters')) {
            var $appliedFilters = $('#appliedFilters');
            var $containerAppliedFilters = $('#containerAppliedFilters');
            var filters = arrAppliedFilters.join(', ');
            var txtTitle = "<strong>" + options.appliedFiltersTitle + " </strong>" + filters;
            if (arrAppliedFilters.length > 0) {
                $containerAppliedFilters.removeClass('d-none');
                $appliedFilters.append(txtTitle);
            }
        } else {
            removeBreadcrumbAppliedFilters();
        }
    }

    function removeBreadcrumbAppliedFilters() {
        $('#containerAppliedFilters').addClass('d-none');
        $('#appliedFilters').html('');
        arrAppliedFilters = [];
    }

    function removeSessionStorage() {
        sessionStorage.removeItem('params');
        sessionStorage.removeItem('projectjson');
        sessionStorage.removeItem('workOrderCategoryjson');
        sessionStorage.removeItem('statejson');
        sessionStorage.removeItem('responsiblesjson');
    }

    function customColumnSort(ind) {
        var a_state = myGrid.getSortingState();
        window.s_col = ind;
        window.isAscending = (a_state[1] === "des") ? "asc" : "des";
        reloadGrid(false);
        return true;
    }

    function showLoading(fl) {
        if (fl === true) {
            showSpinner();
        } else {
            hideSpinner();
        }
    }

    function onContextMenuClick(menuitemId, type) {
        var data = myGrid.contextID.split("_");
        if (menuitemId === "detail") {
            location.href = options.grid.urlDetail + "/" + data[0];
        }
        else if (menuitemId === "edit") {
            location.href = options.grid.urlEdit + "/" + data[0];
        }
        return true;
    }

    function onRowDblClicked(rowId) {
        location.href = options.grid.urlDetail + "/" + rowId;
    }

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        datePicker = new dhtmlXCalendarObject(["ResolutionDateSla", "CreationStartDate", "CreationEndDate", "CreationDate", "ActionDateStartDate", "ActionDateEndDate", "ActionDateDate"]);
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y-%m-%d %H:%i");
        datePicker.hideTime();
    };

    function setWorkOrderId(value) {
        excel.WorkOrderId = value;
        let filterName = $('#WorkOrderId').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setInternalIdentifier(value) {
        excel.InternalIdentifier = value;
        let filterName = $('#InternalIdentifier').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setSerialNumber(value) {
        excel.SerialNumber = value;
        let filterName = $('#SerialNumber').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setLocationCode(value) {
        excel.LocationCode = value;
        let filterName = $('#LocationCode').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setResolutionDateSla(datevalue) {
        excel.ResolutionDateSla = datevalue.toString();
        let filterName = $('#ResolutionDateSla').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setCreationStartDate(datevalue) {
        excel.CreationStartDate = datevalue.toString();
        let filterName = $('#CreationStartDate').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setCreationEndDate(datevalue) {
        excel.CreationEndDate = datevalue;
        let filterName = $('#CreationEndDate').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setCreationDate(datevalue) {
        excel.CreationDate = datevalue;
        let filterName = $('#CreationDate').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setActionDateStartDate(datevalue) {
        excel.ActionDateStartDate = datevalue;
        let filterName = $('#ActionDateStartDate').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setActionDateEndDate(datevalue) {
        excel.ActionDateEndDate = datevalue;
        let filterName = $('#ActionDateEndDate').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setActionDateDate(datevalue) {
        excel.ActionDateDate = datevalue;
        let filterName = $('#ActionDateDate').data('filter');
        arrAppliedFilters.push(filterName);
    }

    function setWorkOrderStatus(ids) {
        excel.WorkOrderStatus = ids;
        let filterName = options.grid.workOrderFilterStatusText;
        arrAppliedFilters.push(filterName);
    }

    function setQueue(ids) {
        excel.WorkOrderQueue = ids;
        let filterName = options.grid.workOrderFilterQueueText;
        arrAppliedFilters.push(filterName);
    }

    function setProject(ids) {
        excel.ProjectIds = ids;
        let filterName = options.projectCombo.column2Text;
        arrAppliedFilters.push(filterName);
    }

    function setWorkOrderCategory(ids) {
        excel.WorkOrderCategoryIds = ids;
        let filterName = options.workOrderCategoryCombo.column2Text;
        arrAppliedFilters.push(filterName);
    }

    function setState(ids) {
        excel.StateIds = ids;
        let filterName = options.statesCombo.column2Text;
        arrAppliedFilters.push(filterName);
    }

    function setResponsibles(ids) {
        excel.ResponsiblesIds = ids;
        let filterName = options.responsobleCombo.column2Text;
        arrAppliedFilters.push(filterName);
    }

    return {
        Init: init,
        Search: search,
        CalendarLoad: calendarLoad,
        GetWorkOrderFilter: getWorkOrderFilter,
        GetPeopleTechniciansFilter: getPeopleTechniciansFilter
    };
})();