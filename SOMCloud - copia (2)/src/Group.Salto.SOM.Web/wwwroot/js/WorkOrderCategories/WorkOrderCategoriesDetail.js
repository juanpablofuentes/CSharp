var app = app || {};
app.WorkOrderCategoriesDetail = app.WorkOrderCategoriesDetail || {};

app.WorkOrderCategoriesDetail = (function () {
    var settings, $tabCalendar;

    var init = function (options) {
        changedTabSelected();
        settings = $.extend({}, options);
        setInitialValues();
        initializeEvents();
    };

    function setInitialValues() {
        $tabCalendar = app.WorkOrderCategories.constants.PeopleTabClass;
    }

    var onClickOpenUrl = function (e) {
        navToDoc();
    };

    function initializeEvents() {
        $('#navUrlDocBtn').on("click", onClickOpenUrl);
        $($tabCalendar).on("click", changedTabSelected);
    }

    function changedTabSelected(evt) {
        var sideMenuId = app.common.ui.constants.SideMenuId;
        var displayNone = 'd-none';
        if (evt && evt.currentTarget.id === app.WorkOrderCategories.constants.CalendarTabList) {
            $(sideMenuId).removeClass(displayNone);
        } else {
            if (!$(sideMenuId).hasClass(displayNone)) {
                $(app.common.ui.constants.SideMenuId).addClass("d-none");
            }
            if ($('#sideBarMenu').is(':visible')) {
                //$('#sideBarMenu').slideToggle("slow");
            }
        }
    }

    return {
        Init: init
    };
})();