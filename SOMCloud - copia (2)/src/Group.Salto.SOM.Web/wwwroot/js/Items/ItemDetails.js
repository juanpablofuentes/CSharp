var app = app || {};
app.items = app.items || {};
app.items.detail = app.items.detail || {};

app.items.detail = (function () {

    var selectedFamily;

    var init = function (options) {
        initializeFamiliesAutocomplete(options.Families);
        initializeSubFamiliesAutocomplete(options.SubFamilies);
        initializeEvents(options.Mode);
    };

    function initializeFamiliesAutocomplete(options) {
        var familiesCombo = new autocomplete();
        familiesCombo.Init("#familiesContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getFamilies,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: options.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        selectedFamily = familiesCombo;
        return familiesCombo;
    }

    function getSubFamilyFilter(text) {
        var selectedOption = selectedFamily.GetSelectedOption();
        var data = {
            Text: text,
            Selected: selectedOption !== undefined ? selectedOption.key : []
        };
        return data;
    }

    function initializeSubFamiliesAutocomplete(options) {
        var subfamiliesCombo = new autocomplete();
        subfamiliesCombo.Init("#subfamiliesContainer",
            {
                hasDefaultItem: options.hasDefaultItem,
                initValue: { key: options.key, value: options.value },
                urlData: app.config.Urls.getSubFamiliesFiltered,
                selectedItemProperty: options.itemIdProperty,
                selectedTextProperty: options.itemTextProperty,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: getSubFamilyFilter,
                ajaxMethodType: app.constants.Post
            });
        return subfamiliesCombo;
    }

    function initializeEvents(mode) {
        uploadFileSelect('#GeneralViewModel_Picture', mode, '#modalIMG');
    }

    return {
        Init: init
    };
})();
