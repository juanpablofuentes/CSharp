var app = app || {};
app.extrafields = app.extrafields || {};
app.extrafields.detail = app.extrafields.detail || {};

app.extrafields.detail = (function () {
    var init = function (options) {
        initializeTextTranslations(options.textTranslationsOptions);
        initializeDescriptionTranslations(options.descriptionsTranslationsOptions);
        $('#extraFieldsTypes').on('load', loadExtraFieldsType);
        $('#extraFieldsTypes').on('change', changeExtraFieldsType);
        $('#extraFieldsTypes').trigger('load');
    };

    var changeCheckbox = function (that) {
        $(that).prop("checked") ? $(that).val("true") : $(that).val("false");
    };

    function clearFields() {
        $('#isMandatoryChk').prop('checked', false);
        $('#MultipleChoice').prop('checked', false);
        $('#DelSystem').prop('checked', false);
        $('#AllowedStringValues').val('');
        $('#ErpSystem').val('');
    }

    function visibilityFields(data) {
        if (data['isMandatoryVisibility'] === true) {
            $('#isMandatoryDiv').removeClass('d-none');
        } else {
            $('#isMandatoryDiv').addClass('d-none');
        }

        if (data['multipleChoiceVisibility'] === true) {
            $('#multipleChoiceDiv').removeClass('d-none');
        } else {
            $('#multipleChoiceDiv').addClass('d-none');
        }

        if (data['delSystemVisibility'] === true) {
            $('#delSystemDiv').removeClass('d-none');
        } else {
            $('#delSystemDiv').addClass('d-none');
        }

        if (data['allowedValuesVisibility'] === true) {
            $('#allowedStringValuesDiv').removeClass('d-none');
        } else {
            $('#allowedStringValuesDiv').addClass('d-none');
        }

        if (data['erpSystemVisibility'] === true) {
            $('#erpSystemDiv').removeClass('d-none');
        } else {
            $('#erpSystemDiv').addClass('d-none');
        }
    }
    var changeExtraFieldsType = function () {
        var id = this.value;
        $.ajax({
            url: '/api/ExtraFieldType',
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { id: id },
            success: function (data) {
                visibilityFields(data);
                clearFields();
            }
        });
    };

    var loadExtraFieldsType = function () {
        var id = this.value;
        $.ajax({
            url: '/api/ExtraFieldType',
            type: 'GET',
            dataType: 'json',
            cache: false,
            data: { id: id },
            success: function (data) {
                visibilityFields(data);
            }
        });
    };

    function initializeTextTranslations(textTranslationsOptions) {
        var options = {
            selectedItems: textTranslationsOptions.selectedItems,
            column1Text: textTranslationsOptions.column1Text,
            column2Text: textTranslationsOptions.column2Text,
            column3Text: textTranslationsOptions.column3Text,
            inputPlaceHolder: textTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.extrafields.constants.CollectionTranslationTextProperty,
            itemIdProperty: app.extrafields.constants.ItemIdProperty,
            itemTextProperty: app.extrafields.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.extrafields.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.extrafields.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.extrafields.constants.columnFirst,
            colSecond: app.extrafields.constants.columnSecond,
            colLast: app.extrafields.constants.columnLast
        };
        new comboInputListSelector().Init(app.extrafields.constants.TextTranslationsContainerId, options);
    }

    function initializeDescriptionTranslations(descriptionsTranslationsOptions) {
        var options = {
            selectedItems: descriptionsTranslationsOptions.selectedItems,
            column1Text: descriptionsTranslationsOptions.column1Text,
            column2Text: descriptionsTranslationsOptions.column2Text,
            column3Text: descriptionsTranslationsOptions.column3Text,
            inputPlaceHolder: descriptionsTranslationsOptions.inputPlaceHolder,
            collectionProperty: app.extrafields.constants.CollectionTranslationDescriptionProperty,
            itemIdProperty: app.extrafields.constants.ItemIdProperty,
            itemTextProperty: app.extrafields.constants.ItemTextProperty,
            itemIdSecondaryProperty: app.extrafields.constants.ItemIdSecondaryProperty,
            itemTextSecondaryProperty: app.extrafields.constants.ItemTextSecondaryProperty,
            urlPrincipalCombo: app.config.Urls.getLanguages,
            colFirst: app.extrafields.constants.columnFirst,
            colSecond: app.extrafields.constants.columnSecond,
            colLast: app.extrafields.constants.columnLast
        };
        new comboInputListSelector().Init(app.extrafields.constants.DescriptionsTranslationsContainerId, options);
    }

    return {
        Init: init,
        ChangeCheckbox: changeCheckbox,
        changeExtraFieldsType
    };
})();