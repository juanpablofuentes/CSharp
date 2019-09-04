var app = app || {};
app.constants = app.constants || {};

app.constants = (function ()
{
    var post = 'POST';
    var get = 'GET'; 
    var deleteMethodType = 'DELETE'; 
    var clickEvent = 'click';
    var changeEvent = 'change';
    var slct = '#slct_';
    var itemIdProperty = 'Value';
    var itemTextProperty = 'Text';
    var itemIdSecondaryProperty = 'ValueSecondary';
    var itemTextSecondaryProperty = 'TextSecondary';
    var tabulatedChar = "\t";
    var searchMinimumCharacters = 3;
    var autoComplete = 1;
    var queryTypePeopleFilterByCompany = 2;
    var queryTypePeopleWithoutCompanyAndSubContractor = 3;
    var queryTypeWorkCenterFilterByCompany = 4;
    var queryKnowledge = 5;
    var queryToolsType = 6;
    var queryCategories = 7;
    var queryPeopleTechnicians = 8;
    var queryPeopleExpense = 9;
    var queryStates = 10;
    var queryExpense = 11;

    return {
        Post: post,
        Get: get,
        ClickEvent: clickEvent,
        ChangeEvent: changeEvent,
        Slct: slct,
        QueryKnowledge: queryKnowledge,
        QueryTypePeopleWithoutCompanyAndSubContractor: queryTypePeopleWithoutCompanyAndSubContractor,
        QueryTypePeopleFilterByCompany: queryTypePeopleFilterByCompany,
        QueryTypeWorkCenterFilterByCompany: queryTypeWorkCenterFilterByCompany,
        QueryToolsType: queryToolsType,
        QueryPeopleExpense: queryPeopleExpense,
        QueryStates: queryStates,
        QueryExpense: queryExpense,
        AutoComplete: autoComplete,
        Delete: deleteMethodType,
        QueryGetCategories: queryCategories,
        QueryPeopleTechnicians: queryPeopleTechnicians,
        ItemIdProperty: itemIdProperty,
        ItemTextProperty: itemTextProperty,
        ItemIdSecondaryProperty: itemIdSecondaryProperty,
        ItemTextSecondaryProperty: itemTextSecondaryProperty,
        SearchMinimumCharacters: searchMinimumCharacters,
        TabulatedChar: tabulatedChar
    };
})();