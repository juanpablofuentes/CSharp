var app = app || {};
app.peopleWork = app.peopleWork || {};

app.peopleWork = (function () {
    var settings, urlDepartments, selectedCompany, selectedDepartments, defaultOptionText, $tabCalendar;

    var init = function (options) {
        settings = $.extend({}, options);
        setInitialValues();
        loadInitialValues();
        initializeEvents();
    };

    function setInitialValues() {
        urlDepartments = settings.urlDepartments;
        selectedCompany = settings.selectedCompany;
        selectedDepartments = settings.selectedDepartments;
        defaultOptionText = settings.defaultOptionText;
        $tabCalendar = app.peopleWork.constants.PeopleTabClass;
    }

    function onGetDepartments(departments) {
        loadSelector("#WorkEditViewModel_DepartmentId", departments);
        var selector = $("#WorkEditViewModel_DepartmentId");
        if (selectedDepartments > 0) {
            selector.val(selectedDepartments).change();
            selectedDepartments = 0;
        }
        if (departments.length === 1) {
            selector.val(departments[0].key).change();
        }
    }

    function loadSelector(selectorId, items) {
        var select = $(selectorId);
        select.empty();
        if (items.length > 1) {
            var defaultOption = "<option value='" + 0 + "'>" + defaultOptionText + "</option>";
            select.append($(defaultOption));
        }
        for (var i = 0; i < items.length; i++) {
            var option = "<option value='" + items[i].key + "'>" + items[i].value + "</option>";
            select.append($(option));
        }
        return select;
    }

    var OnChangeCompany = function (e) {
        if ($('#WorkEditViewModel_CompanyId').val() != 0) {
            $('#WorkEditViewModel_SubcontractId').val(0);
            $("#WorkCheckSubcontractorResponsible").prop("checked", false);
        }

        var element = $("#WorkEditViewModel_CompanyId option:selected");
        $("#WorkEditViewModel_DepartmentId").empty();
        $("#WorkEditViewModel_DepartmentId").val(0);
        getdata(element.val(), urlDepartments, onGetDepartments);
    };

    var OnChangeSubcontract = function (e) {
        $('#WorkEditViewModel_CompanyId').val(0).change();
        $('#WorkEditViewModel_DepartmentId').val(0);
        $('#WorkEditViewModel_DepartmentId').empty();
    };

    var OnClickOpenUrl = function (e) {
        navToDoc();
    };

    function initializeEvents() {
        $("#side-menu").addClass("d-none");
        $('#WorkEditViewModel_CompanyId').on("change", OnChangeCompany);
        $('#WorkEditViewModel_SubcontractId').on("change", OnChangeSubcontract);
        $('#navUrlDocBtn').on("click", OnClickOpenUrl);
        $($tabCalendar).on("click", changedTabSelected);

    }

    function changedTabSelected(evt) {
        var sideMenuId = app.common.ui.constants.SideMenuId;
        var displayNone = 'd-none';
        if (evt.currentTarget.id === app.peopleWork.constants.CalendarTabList) {
            $(sideMenuId).removeClass(displayNone);
        } else {
            if ($(sideMenuId).hasClass(displayNone)) {
                $(app.common.ui.constants.SideMenuId).addClass("d-none");
                $(app.common.ui.constants.Body).removeClass("aside-menu-lg-show");
            }
        }
    }

    function loadInitialValues() {

        if (selectedCompany > 0) {
            var element = $("#WorkEditViewModel_CompanyId");
            element.val(selectedCompany);
            selectedCompany = 0;
            OnChangeCompany();
        }
    }

    var getdata = function (data, url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { id: data },
            success: success
        });
    };

    return {
        Init: init
    };
})();