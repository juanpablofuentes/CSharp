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