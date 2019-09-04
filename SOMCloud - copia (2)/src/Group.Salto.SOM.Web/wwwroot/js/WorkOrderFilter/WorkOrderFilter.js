var app = app || {};
app.WorkOrderFilter = app.WorkOrderFilter || {};

app.WorkOrderFilter = (function () {
    var options, availableColumns, selectedColumns, selectedColumncolumnsJson;
    var viewId = 0;

    var init = function (_options) {
        options = _options;
        initializeEvents(options);
        calendarLoad();
        loadView();
    };

    function loadView() {
        var list = $(".viewlist")
        for (var i = 0; i < list.length; i++) {
            var value = list[i].attributes["data-load"].value;
            if (value === 'true')
                $(".viewlist")[i].click();
        }
    }

    function initializeEvents(options) {
        $('#EditDatesConfirmSave').on('click', editDatesConfirmSaveClick);
        $('#EditDatesConfirmCancel').on('click', editDatesConfirmCancelClick);
        $('#EditMultiSelectConfirmSave').on('click', editMultiSelectConfirmSaveClick);
        $('#EditMultiSelectConfirmCancel').on('click', editMultiSelectConfirmCancelClick);
        $(".deleteButton").on('click', clickDeleteButton);
        $("#confirmationModalConfirmCancel").on("click", clickConfirmationModalConfirmCancel);
    }

    function editDatesConfirmSaveClick() {
        var columnid = $("#modalcolumnid").val();
        var data = selectedColumncolumnsJson.find(c => c.ColumnId == columnid);
        data.FilterStartDate = $("#FilterInitialDate").val();
        data.FilterEndDate = $("#FilterEndDate").val();
        data.ToolTip = options.general.dateToolTip.replace('{InitialDate}', data.FilterStartDate).replace('{EndDate}', data.FilterEndDate);
        $('#EditDatesConfirmCancel').click();
    }

    function editDatesConfirmCancelClick() {
        $("#modalcolumnid").val('');
        $("#FilterInitialDate").val('');
        $("#FilterEndDate").val('');
    }

    function editMultiSelectConfirmSaveClick() {
        var columnid = $("#modalcolumnid").val();
        var data = selectedColumncolumnsJson.find(c => c.ColumnId == columnid);
        var ids = app.multiselect.GetSelectedIds(options.general.modalMultiSelect);
        var labels = app.multiselect.GetSelectedLabel(options.general.modalMultiSelect);
        data.FilterValues = ids;
        data.ToolTip = labels;
        $('#EditMultiSelectConfirmCancel').click();
    }

    function editMultiSelectConfirmCancelClick() {
        $("#modalcolumnid").val('');
        $("#multiselectcontainer").html('');
    }

    function dateSelectedColumnClick(e) {
        var columnid = $(e).data("columnid");
        $("#modalcolumnid").val(columnid);
        var data = selectedColumncolumnsJson.find(c => c.ColumnId == columnid);
        $("#FilterInitialDate").val(data.FilterStartDate);
        $("#FilterEndDate").val(data.FilterEndDate);
    }

    function multiSelectSelectedColumnClick(e) {
        var columnid = $(e).data("columnid");
        $("#modalcolumnid").val(columnid);
        var data = selectedColumncolumnsJson.find(c => c.ColumnId == columnid);
        var filter = '';
        if (data.FilterValues !== undefined && !data.FilterValues === null)
            filter = data.FilterValues;
        loadAjaxView('?id=' + columnid + '&filter=' + filter, options.general.urlMultiSelect, 'multiselectcontainer', multiSelectComplet);
    }

    function multiSelectComplet(result) {
        app.multiselect.Init(options.general.modalMultiSelect);
        var columnid = $("#modalcolumnid").val();
        var data = selectedColumncolumnsJson.find(c => c.ColumnId == columnid);
        if (data.FilterValues != null) {
            var ids = data.FilterValues.split(",");
            app.multiselect.LoadByIds(ids, options.general.modalMultiSelect);
        }
    }

    function viewListClick(e) {
        $("#viewFilterContainer").html('');
        viewId = e.attributes["data-id"].value;
        loadAjaxView('?id=' + viewId, options.general.urlViewFilters, 'viewFilterContainer', loadViewListData, true);
    }

    var createNewView = function () {
        $("#viewFilterContainer").html('');
        loadAjaxView('?id=0', options.general.urlViewFilters, 'viewFilterContainer', loadViewListData, true);
    };

    function loadViewListData(result) {
        loadDragAndDropColumns();
        var availableColumnsValue = $("#availableColumnsValue").val();
        addAvailableData(availableColumnsValue);
        var selectedColumnsValue = $("#selectedColumnsValue").val();
        addSelectedData(selectedColumnsValue);
        stateButtonSave();
        hideSpinner();

        var technicians = apiCall(app.config.Urls.getConfiguredViewTechnicians, 'GET', 'json', { id: viewId });
        technicians.done(function (res) {
            app.multiselect.LoadMultiSelect('WorkOrderFilterTechnicians', 'Technicians_Items', res);
        });

        var group = apiCall(app.config.Urls.getConfiguredViewGetGroups, 'GET', 'json', { id: viewId });
        group.done(function (res) {
            app.multiselect.LoadMultiSelect('WorkOrderFilterGroups', 'Groups_Items', res);
        });
    }

    function loadDragAndDropColumns() {
        availableColumns = new dhtmlXList({
            container: 'availableColumnsContainer',
            template: "#TranslatedName#",
            drag: true,
            type: {
                css: "d-and-d"
            }
        });

        selectedColumns = new dhtmlXList({
            container: 'selectedColumnsContainer',
            drag: true,
            type: {
                template: function (obj) {
                    if (obj.Modal === 1) {
                        return "<span>" + obj.TranslatedName + "</span><i id='sc" + obj.ColumnId + "' data-columnid='" + obj.ColumnId + "' class='fa fa-pencil viewEdit float-right mt-1' data-toggle='modal' data-target='#EditDatesModal' onclick='app.WorkOrderFilter.DateSelectedColumnClick(this)'></i>";
                    }
                    else if (obj.Modal === 2) {
                        return "<span>" + obj.TranslatedName + "</span><i id='sc" + obj.ColumnId + "' data-columnid='" + obj.ColumnId + "' class='fa fa-pencil viewEdit float-right mt-1' data-toggle='modal' data-target='#EditMultiSelectModal' onclick='app.WorkOrderFilter.MultiSelectSelectedColumnClick(this)'></i>";
                    }
                    else {
                        return "<span class='viewEdit' data-columnid='" + obj.ColumnId + "'>" + obj.TranslatedName + "</span>";
                    }
                },
                css: "d-and-d"
            },
            tooltip: {
                template: "<b>#ToolTip#</b>"
            }
        });

        selectedColumns.attachEvent("onBeforeDrop", function (context, ev) {
            var item = context.from.data.pull[context.start];
            selectedColumncolumnsJson.push(item);
            context.html = "<div style='background-color:white; font-family:Tahoma; padding:10px;'>" + item.Name + "</div>";
            return true;
        });

        availableColumns.attachEvent("onAfterDrop", function (context, ev) {
            stateButtonSave();
        });

        selectedColumns.attachEvent("onAfterDrop", function (context, ev) {
            stateButtonSave();
        });
    }

    function addAvailableData(data) {
        var columns = JSON.parse(data);
        var position = 1;
        columns.forEach(function (column) {
            availableColumns.add(column, position++);
        });
    }

    function addSelectedData(data) {
        selectedColumncolumnsJson = JSON.parse(data);
        var position = 1;
        selectedColumncolumnsJson.forEach(function (column) {
            selectedColumns.add(column, position++);
        });
    }

    var saveView = function () {
        if (!validateForm()) return;
        if ($("#Id").val() === "0") document.forms["workOrderfilter"].action = options.general.urlCreate;
        else document.forms["workOrderfilter"].action = options.general.urlEdit;
        var columns = selectedColumncolumnsJson;
        var data = $(".viewEdit");
        var input = '';

        for (var i = 0; i < data.length; i++) {
            var column = columns.find(c => c.ColumnId === $(data[i]).data('columnid'));
            var filterValues = column.FilterValues !== null ? column.FilterValues : '';
            var startDate = column.FilterStartDate !== null ? column.FilterStartDate : '';
            var endDate = column.FilterEndDate !== null ? column.FilterEndDate : '';
            input += '<input type="text" id="SelectedColumns_' + i + '__Name" name="SelectedColumns[' + i + '].Name" value="' + column.Name + '">';
            input += '<input type="text" id="SelectedColumns_' + i + '__ColumnId" name="SelectedColumns[' + i + '].ColumnId" value="' + column.ColumnId + '">';
            input += '<input type="text" id="SelectedColumns_' + i + '__FilterValues" name="SelectedColumns[' + i + '].FilterValues" value="' + filterValues + '">';
            input += '<input type="text" id="SelectedColumns_' + i + '__FilterStartDate" name="SelectedColumns[' + i + '].FilterStartDate" value="' + startDate + '">';
            input += '<input type="text" id="SelectedColumns_' + i + '__FilterEndDate" name="SelectedColumns[' + i + '].FilterEndDate" value="' + endDate + '">';
        }
        $("#hiddenrows").html(input);
        document.forms["workOrderfilter"].submit();
        $("#hiddenrows").html('');
    };

    function stateButtonSave() {
        if (validateForm()) {
            $("#btnsaveview").prop("disabled", false);
        }
        else {
            $("#btnsaveview").prop("disabled", true);
        }
    }

    function validateForm() {
        return ($("#Name").val() !== '' && selectedColumns.serialize().length > 0);
    }

    function onChangeName(item) {
        stateButtonSave();
    }

    var calendarLoad = function () {
        var cultureInfo = getCookie("culture-code").toLowerCase();
        var datePicker = new dhtmlXCalendarObject(["FilterInitialDate", "FilterEndDate"]);
        datePicker.showTime();
        datePicker.loadUserLanguage(cultureInfo);
        datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, true), "%Y-%m-%d %H:%i");
    };

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal("toggle");
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: options.general.urlDelete,
                    type: 'DELETE',
                    dataType: 'json',
                    cache: false,
                    data: { id: id },
                    success: function () {
                        location.reload();
                    },
                });
                $(modalId).modal("toggle");
            });
    }

    var clickDeleteButton = function (e) {
        e.preventDefault();
        var id = $(this).attr('id');
        deleteElement(id, "#confirmationModal", "#confirmationModalConfirmSave");
    };

    var clickConfirmationModalConfirmCancel = function () {
        $("#confirmationModal").modal("toggle");
        $("#confirmationModalConfirmSave").off("click");
    };

    return {
        Init: init,
        ViewListClick: viewListClick,
        DateSelectedColumnClick: dateSelectedColumnClick,
        MultiSelectSelectedColumnClick: multiSelectSelectedColumnClick,
        CalendarLoad: calendarLoad,
        SaveView: saveView,
        CreateNewView: createNewView,
        OnChangeName: onChangeName
    };
})();