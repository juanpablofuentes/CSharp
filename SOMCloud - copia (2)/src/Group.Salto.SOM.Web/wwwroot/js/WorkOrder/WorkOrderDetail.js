var app = app || {};
app.WorkOrderDetail = app.WorkOrderDetail || {};

app.WorkOrderDetail = (function () {
    var subPetitionsGrid, billGrid, billLineGrid, workOrderAssetsGrid;
    var options;

    var init = function (_options) {
        options = _options;
        initializeEvents();
        loadTask(options.general.id);
    };

    var loadTask = function (id) {
        app.common.ui.GetDataById(id, app.config.Urls.getTasksForWorkOrder, taskExecutionCallBack);
    };

    function taskExecutionCallBack(result) {
        app.common.ui.LoadSelectorKeyValue("cmbtask", result, "id", "name", false);
        $('#combo-task_container').toggleClass('d-none');
    }

    function initializeEvents() {
        colorLabelHeaderDetails();
        dhx.attachEvent("onLoadXMLError", my_error_handler);
        $("#formtab").on('click', function () {
            loadAjaxView('?id=' + options.general.id + '&FatherId=' + options.general.fatherId + '&ReferenceOtherService=' + options.general.referenceOtherService + '&GeneratedServiceId=' + options.general.generatedServiceId + '&GeneratedService=' + options.general.generatedService, options.general.urlForm, 'form-placeholder');
        });
        $("#subpetitiontab").on('click', function () {
            loadSubPetitionsGrid(options.general.id);
        });
        $("#deliverynotetab").on('click', function () {
            loaBillGrid(options.general.id);
        });
        $("#assetstab").on('click', function () {
            loadAjaxView('?id=' + options.general.assetId, options.general.urlAsset, 'asset-placeholder', loaWorkOrderAssetsGrid);
        });
        $("#operationstab").on('click', function () {
            loadAjaxView('?id=' + options.general.id, options.general.urlOperations, 'operations-placeholder');
        });
        addBootstrapClassToGrid();
        $('#executiontask').on('click', executeTaskClick);
    }

    function executeTaskClick(evt) {
        evt.preventDefault();
        showSpinner();
        var id = $('#cmbtask').val();
        var woid = options.general.id;
        var executeTask = apiCall(app.config.Urls.executeTask, 'POST', 'json', { id: id, woid: woid });
        executeTask.done(function (data) {
            hideSpinner();
            console.log(data);
        });
        executeTask.error(function (xhr, ajaxOptions, thrownError) {
            hideSpinner();
            toastr.options.closeButton = 'False';
            toastr.options.newestOnTop = 'False';
            var optionsOverride = {};
            toastr['error']("Error", thrownError.message, optionsOverride);
        });
    }

    function openSummary(item) {
        if (item !== null || item !== undefined) {
            app.common.ui.GetDataById(item, app.config.Urls.getSummary, getSummaryData);
        }
    }

    function getSummaryData(result) {
        $('#projectName').text(result.projectName);
        $('#projectObservations').text(result.projectObservations);
        $('#contractObject').text(result.contractName);
        $('#contractObservations').text(result.contractObservations);
        $('#clientSiteName').text(result.finalClientName);
        $('#clientSiteNIF').text(result.finalClientNIF);
        $('#clientSiteTel1').text(result.finalClientTel1);
        $('#clientSiteTel2').text(result.finalClientTel2);
        $('#clientSiteTel3').text(result.finalClientTel3);
        $('#clientSiteFax').text(result.finalClientFax);
        $('#clientSiteObs').text(result.finalClientObservations);
        $('#locationName').text(result.locationName);
        $('#locationZone').text(result.locationZone);
        $('#locationSubZone').text(result.locationSubZone);
        $('#locationCity').text(result.locationCity);
        $('#locationPostalCode').text(result.locationPostalCode);
        $('#locationState').text(result.locationProvince);
        $('#assignQueue').text(result.assignationQueue);
        $('#assignTechnician').text(result.assignationTechnician);
        $('#woIdFather').text(result.workOrderFatherID);
        $('#woType').text(result.workOrderType);
        $('#woCategory').text(result.workOrderCategory);
        $('#woClient').text(result.workOrderClient);
        $('#woClientSite').text(result.workOrderClientSite);
        $('#woStats').text(result.workOrderStatus);
        $('#woExternalStats').text(result.workOrderExternalStatus);
        $('#woRepair').text(result.workOrderRepair);
        $('#timingCreate').text(result.timingCreation);
        $('#timingReception').text(result.timingReception);
        $('#timingAssigned').text(result.timingAssigned);
        $('#timingPerformance').text(result.timingPerformance);
        $('#timingFinalPerformance').text(result.timingFinalPerformance);
        $('#timingCloseClient').text(result.timingCloseClient);
        $('#timingCloseSalto').text(result.timingCloseClientSalto);
        $('#timingAnswerSLA').text(result.timingSLAResponse);
        $('#timingFinalSLA').text(result.timingFinalSLA);
        $('#timingPenaltyFinalSLA').text(result.timingResponsePenalty);
        $('#timingBreachPenaltySLA').text(result.timingBreachPenalty);
        $('#timingStopSLA').text(result.timingStopSLA);
    }

    function my_error_handler(name, xhr) {
        hideSpinner();
        renderSimpleModal('modal-danger', 'modal-sm', 'Error', name);
        location.reload();
    }

    function loadSubPetitionsGrid(Id) {
        if (subPetitionsGrid === undefined) {
            var myMenu = new dhtmlXMenuObject();
            myMenu.setIconsPath("/lib/dhtmlx/suite/codebase/common/imgs/");
            myMenu.renderAsContextMenu();
            myMenu.attachEvent("onClick", onContextMenuClick);
            myMenu.loadStruct('[{ id: "detail", text:"' + options.grid.detailText + '", img: \"details.png\"},{ id: "edit", text: "' + options.grid.editText + '", img: \"edit.png\" }]', function () { });

            subPetitionsGrid = new dhtmlXGridObject('gridboxSubPetitions');
            subPetitionsGrid.setImagePath("/lib/suite/codebase/imgs/dhxgrid_material/");
            subPetitionsGrid.attachEvent("onXLE", showLoading);
            subPetitionsGrid.attachEvent("onXLS", function () { showLoading(true); });

            subPetitionsGrid.enableLightMouseNavigation(true);
            subPetitionsGrid.enableColumnAutoSize(true);
            subPetitionsGrid.enableAutoWidth(true);
            subPetitionsGrid.enableAutoHeight(true);

            subPetitionsGrid.enableContextMenu(myMenu);
            subPetitionsGrid.init();
            subPetitionsGrid.load(app.config.Urls.getSubWO + '?id=' + Id, onSubPetitionsGridLoaded, "json");
            subPetitionsGrid.setSizes();
        }
    }

    function onSubPetitionsGridLoaded() {
        if (subPetitionsGrid.getRowsNum() === 0) {
            $("#nodataSubPetitions").load(options.general.urlNoData);
            subPetitionsGrid.clearAll(true);
        }
    }

    function loaBillGrid(Id) {
        if (billGrid === undefined) {

            billGrid = new dhtmlXGridObject('gridboxBill');
            billGrid.setImagePath("/lib/suite/codebase/imgs/dhxgrid_material/");
            billGrid.attachEvent("onXLE", showLoading);
            billGrid.attachEvent("onXLS", function () { showLoading(true); });

            billGrid.enableColumnAutoSize(true);
            billGrid.enableAutoWidth(true);
            billGrid.enableAutoHeight(true);

            billGrid.init();
            billGrid.attachEvent("onRowSelect", doOnBillRowSelect);
            billGrid.load(app.config.Urls.getBillWO + '?id=' + Id, onBillGridLoaded, "json");
        }
    }

    function loaBillLinesGrid(Id) {
        billLineGrid = new dhtmlXGridObject('gridboxBillLine');
        billLineGrid.enableAutoHeight(true);
        billLineGrid.setImagePath("/lib/suite/codebase/imgs/dhxgrid_material/");
        billLineGrid.attachEvent("onXLE", showLoading);
        billLineGrid.attachEvent("onXLS", function () { showLoading(true); });

        billLineGrid.enableLightMouseNavigation(true);
        billLineGrid.enableColumnAutoSize(true);
        billLineGrid.enableAutoWidth(true);

        billLineGrid.init();
        billLineGrid.load(app.config.Urls.getBillLineWO + '?id=' + Id, "json");
    }

    function onBillGridLoaded() {
        if (billGrid.getRowsNum() === 0) {
            $("#nodataBill").load(options.general.urlNoData);
            $("#gridboxBill").remove();
            billGrid.clearAll(true);
        }
    }

    function loaWorkOrderAssetsGrid(data) {
        if (options.general.assetId > 0) {
            workOrderAssetsGrid = new dhtmlXGridObject('gridboxAssets');
            workOrderAssetsGrid.setImagePath("/lib/suite/codebase/imgs/dhxgrid_material/");
            workOrderAssetsGrid.attachEvent("onXLE", showLoading);
            workOrderAssetsGrid.attachEvent("onXLS", function () { showLoading(true); });
            workOrderAssetsGrid.enableAutoWidth(true);
            workOrderAssetsGrid.enableAutoHeight(true);
            workOrderAssetsGrid.init();
            workOrderAssetsGrid.attachEvent("onRowSelect", doOnAssetRowSelect);
            workOrderAssetsGrid.load(app.config.Urls.getAssetsWorkOrder + '?id=' + options.general.assetId, "json");
        }
    }

    function doOnBillRowSelect(id, ind) {
        loaBillLinesGrid(id);
    }

    function doOnAssetRowSelect(id, ind) {
        loadAjaxView('?id=' + id, options.general.urlAssetsWorkOrderServices, 'assetServices-placeholder');
    }

    function onContextMenuClick(menuitemId, type) {
        var data = subPetitionsGrid.contextID.split("_");
        if (menuitemId === "detail") {
            location.href = options.grid.urlDetail + "/" + data[0];
        }
        else if (menuitemId === "edit") {
            location.href = options.grid.urlEdit + "/" + data[0];
        }
        return true;
    }

    function addBootstrapClassToGrid() {
        $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
            var target = $(e.target).attr("href");
            if (target === '#nav-subpetitions' || target === '#nav-deliverynotes') {
                setdhxContainerSize();
            }
        });

        $('.sidebar-toggler').on('click', function () {
            setdhxContainerSize();
        });
    }

    function setdhxContainerSize() {
        if (subPetitionsGrid) {
            addBootstrapClass();
            subPetitionsGrid.setInitWidths("78,*,*,*,*,*,*,*,*,*,*");
            subPetitionsGrid.setSizes();
        }
        if (billGrid) {
            addBootstrapClass();
            billGrid.setInitWidths("*,*,*,*,*,*,*,*,*,*,*");
            billGrid.setSizes();
        }
        if (billLineGrid) {
            addBootstrapClass();
            billLineGrid.setInitWidths("*,*,*,*,*,*,*,*,*,*,*");
            billLineGrid.setSizes();
        }
    }

    function colorLabelHeaderDetails() {
        $(options.color.labelContainerId).find('div').each(function () {
            var $item = $(this);
            const startBackgroudColor = options.color.containerBgColor;
            const containerOf = $item[0].id;

            if ($item.attr('id') !== "woSummary") {
                app.contrastColor.Init(
                    {
                        callFrom: 'default',
                        parentElem: '#' + $item.parent().attr('id'),
                        elemChild: '#' + containerOf,
                        childColor: startBackgroudColor,
                        defaulClassColor: options.color.defaultClassColor
                    });
            }
        });
    }

    var onEditFormComplet = function () {
        calendarLoad();
    };

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var ids = [];
        $('.form-control.fdates').each(function () {
            ids.push(this.id);
        });
        var datePicker = new dhtmlXCalendarObject(ids);
        datePicker.showTime();
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, true), "%Y-%m-%d %H:%i");
    };

    var postPopUp = function (serviceId) {
        var myForm = document.getElementById('uploadForm');
        var fData = new FormData(myForm);
        fData.append('serviceId', serviceId);
        fData.append('workOrderId', options.general.id);

        $.ajax({
            url: "/WorkOrder/EditForm",
            data: fData,
            processData: false,
            contentType: false,
            type: "POST"

        }).done(function () {
            $('#modalEditForm').modal('hide');
            loadAjaxView('?id=' + options.general.id + '&FatherId=' + options.general.fatherId + '&ReferenceOtherService=' + options.general.referenceOtherService + '&GeneratedServiceId=' + options.general.generatedServiceId + '&GeneratedService=' + options.general.generatedService, options.general.urlForm, 'form-placeholder');
        });
    };

    return {
        Init: init,
        OpenSummary: openSummary,
        LoadAjaxView: loadAjaxView,
        OnEditFormComplet: onEditFormComplet,
        PostPopUp: postPopUp
    };
})();