var app = app || {};
app.subContract = app.subContract || {};
app.subContract.detail = app.subContract.detail || {};

app.subContract.detail = (function () {

    var init = function (options) {
        initializeKnowledgeCombo(options.knowledgeCombo);
        initializeResponsiblesCombo(options.responsiblesCombo);
        initializePeopleCombo(options.peopleCombo);
    };

    function initializeKnowledgeCombo(knowledgeOptions) {
        new multiComboListSelector().Init("#knowledgesContainer",
            {
                selectedItems: knowledgeOptions.selectedItems,
                urlPrincipalCombo: knowledgeOptions.urlPrincipalCombo,
                urlSecondaryCombo: knowledgeOptions.urlSecondaryCombo,
                collectionProperty: "KnowledgeSelected",
                itemIdProperty: "Value",
                itemTextProperty: "Text",
                itemIdSecondaryProperty: "ValueSecondary",
                itemTextSecondaryProperty: "TextSecondary",
                column1Text: knowledgeOptions.column1Text,
                column2Text: knowledgeOptions.column2Text,
                column3Text: knowledgeOptions.column3Text,
            });
    }

    function initializeResponsiblesCombo(responsiblesOptions) {
        var responsibles = new autoCompleteListSelector();
        responsibles.Init("#responsiblesContainer",
            {
                selectedItems: responsiblesOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getPeople,
                collectionProperty: 'ResponsiblesSelected',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 3,
                column1Text: responsiblesOptions.column1Text,
                column2Text: responsiblesOptions.column2Text,
                getDataMethod: responsiblesOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return responsibles;
    }

    function initializePeopleCombo(peopleOptions) {
        var peopleCombo = new autoCompleteListSelector();
        peopleCombo.Init("#peopleContainer",
            {
                selectedItems: peopleOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getPeople,
                collectionProperty: 'PeopleSelected',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                minimumCharacters: 3,
                column1Text: peopleOptions.column1Text,
                column2Text: peopleOptions.column2Text,
                getDataMethod: peopleOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return peopleCombo;
    }

    return {
        Init: init
    };
})();