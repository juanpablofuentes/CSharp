var app = app || {};
app.company = app.company || {};
app.company.detail = app.company.detail || {};

app.company.detail = (function () {

    var init = function (options) {
        initializeWorkCenters(options.workCenters);
    };

    function initializeWorkCenters(options) {
        var workCentersCombo = new autoCompleteListSelector();
        workCentersCombo.Init("#workCentersCombo",
            {
                selectedItems: options.selectedItems,
                collectionProperty: 'WorkCentersSelected',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 0,
                column1Text: '#',
                column2Text: options.column2Text,
                ajaxMethodType: app.constants.Post,
                editMethod: options.editMethod,
                showComboSelector: false,
            });
    }

    return {
        Init: init
    };
})();