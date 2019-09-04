var app = app || {};
app.finalclients = app.finalclients || {};

app.finalclients = (function () {
    var settings, $tabCalendar;

    var init = function (options) {
        changedTabSelected();
        settings = $.extend({}, options);
        setInitialValues();
        initializeEvents();
    };

    function setInitialValues() {
        $tabCalendar = app.finalclients.constants.FinalClientsTabClass;        
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
        if (evt && evt.currentTarget.id === app.finalclients.constants.CalendarTabList) {
            $(sideMenuId).removeClass(displayNone);
        } else {
            if (!$(sideMenuId).hasClass(displayNone)) {
                $(app.common.ui.constants.SideMenuId).addClass("d-none");
                $(app.common.ui.constants.Body).removeClass("aside-menu-lg-show");
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