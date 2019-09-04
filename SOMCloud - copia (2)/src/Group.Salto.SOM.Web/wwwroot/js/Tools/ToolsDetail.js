var app = app || {};
app.tools = app.tools || {};
app.tools.detail = app.tools.detail || {};

app.tools.detail = (function () {

    var init = function (options) {
        initializeToolsTypeCombo(options.toolstypeCombo);        
    };

    function initializeToolsTypeCombo(toolstypeOptions) {
        var toolstypeCombo = new autoCompleteListSelector();
        toolstypeCombo.Init("#toolstypeContainer",
            {
                selectedItems: toolstypeOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getToolsType,
                collectionProperty: 'Types',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 0,
                column1Text: toolstypeOptions.column1Text,
                column2Text: toolstypeOptions.column2Text,
                getDataMethod: toolstypeOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return toolstypeCombo;
    } 

    return {
        Init: init
    };
})();