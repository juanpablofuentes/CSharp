var app = app || {};
app.toolstype = app.toolstype || {};
app.toolstype.detail = app.toolstype.detail || {};

app.toolstype.detail = (function () {

    var init = function (options) {
        initializeKnowledgeCombo(options.knowledgeCombo);        
    };

    function initializeKnowledgeCombo(knowledgeOptions) {
        var knowledgeCombo = new autoCompleteListSelector();
        knowledgeCombo.Init("#knowledgesContainer",
            {
                selectedItems: knowledgeOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getKnowledge,
                collectionProperty: 'KnowledgeSelected',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 0,
                column1Text: knowledgeOptions.column1Text,
                column2Text: knowledgeOptions.column2Text,
                getDataMethod: knowledgeOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return knowledgeCombo;
    } 

    return {
        Init: init
    };
})();