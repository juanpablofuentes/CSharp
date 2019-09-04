var app = app || {};
app.extrafields = app.extrafields || {};
app.extrafields.detailModal = app.extrafields.detailModal || {};

app.extrafields.detailModal = (function () {

    var onCreate = function () {
        $('#ExtraFieldsSystemEdit').text('');
        $('#ExtraFieldsRegularEdit').text('');
        var extraFieldValue = parseInt($('#newExtraField').val());
        if (extraFieldValue === 1) {//Nuevo
            $('#divextrafieldssystemid').addClass('d-none');
            $('#divextrafieldsregularid').addClass('d-none');
            $('#divnewextrafieldsid').removeClass('d-none');

            $('#divdescription').removeClass('d-none');
            $('#divtypeid').removeClass('d-none');
            $('#divdelsystem').removeClass('d-none');
            $('#divposition').removeClass('d-none');

            $('#TypeId').val(0);
            $('#TypeId').removeClass('d-none');
            $('#TypeName').val($('#TypeId').find(':selected').text());
            $('#TypeNameEdit').text('');
            visibilityForOptionals();
        }
        else if (extraFieldValue === 2) {//Sistema
            $('#DelSystem').val(true);
            $('#DelSystem').prop('checked', true);
            $('#divextrafieldssystemid').removeClass('d-none');
            $('#divextrafieldsregularid').addClass('d-none');
            $('#divnewextrafieldsid').addClass('d-none');
            setAllDisabled();
        }
        else if (extraFieldValue === 3) {//regular
            $('#DelSystem').val(false);
            $('#DelSystem').prop('checked', false);
            $('#divextrafieldssystemid').addClass('d-none');
            $('#divextrafieldsregularid').removeClass('d-none');
            $('#divnewextrafieldsid').addClass('d-none');
            setAllDisabled();
        }
    };

    function setAllDisabled() {
        $('#divdescription').addClass('d-none');
        $('#divtypeid').addClass('d-none');
        $('#divdelsystem').addClass('d-none');
        $('#divposition').addClass('d-none');
        $('#divismandatory').addClass('d-none');
        $('#divmultiplechoice').addClass('d-none');
        $('#divallowedstringvalues').addClass('d-none');
        $('#diverpsysteminstancequeryid').addClass('d-none');
    }

    var onEdit = function () {
        editDisplay();
    };

    var onValidationError = function () {
        var extraFieldValue = parseInt($('#newExtraField').val());
        if (extraFieldValue === 1) {
            visibilityForOptionals();
            $('#divextrafieldssystemid').addClass('d-none');
            $('#divextrafieldsregularid').addClass('d-none');
        } else if (extraFieldValue === 2) {
            $('#DelSystem').val(true);
            $('#DelSystem').prop('checked', true);
            $('#divextrafieldssystemid').removeClass('d-none');
            $('#divextrafieldsregularid').addClass('d-none');
            $('#divnewextrafieldsid').addClass('d-none');
            setAllDisabled();
        } else if (extraFieldValue === 3) {
            $('#DelSystem').val(false);
            $('#DelSystem').prop('checked', false);
            $('#divextrafieldssystemid').addClass('d-none');
            $('#divextrafieldsregularid').removeClass('d-none');
            $('#divnewextrafieldsid').addClass('d-none');
            setAllDisabled();
        }
    };

    var onSaveModal = function (item) {

        var extraFieldValue = parseInt($('#newExtraField').val());
        if (extraFieldValue === 2)
            item.ExtraFieldsId = item.ExtraFieldsSystemId;

        if (extraFieldValue === 3)
            item.ExtraFieldsId = item.ExtraFieldsRegularId;
        if (item.Position === '') {
            let nextPosition = getMaxPosition(selectedExtraFields);
            item.Position = nextPosition;

        } else if (item.Position !== '') {
            var nextPosition = getMaxPosition(selectedExtraFields);
            for (var i = 0; i < selectedExtraFields.length; i++) {
                if (item.Position === selectedExtraFields[i].Position.toString()) {
                    if (item.ExtraFieldsName !== selectedExtraFields[i].ExtraFieldsName) {
                        selectedExtraFields[i].Position = nextPosition;
                        if (selectedExtraFields[i].State == null) {
                            selectedExtraFields[i].State = 'U';
                            break;
                        }
                    }
                }
            }
        }
    };

    getMaxPosition = function (arrayEF) {
        var maxPosition = 0;
        var limit = 2147483647;
        for (var i = 0; i < arrayEF.length; i++) {
            var currentPosition = parseInt(arrayEF[i].Position);
            if (currentPosition > maxPosition) {
                maxPosition = currentPosition;
            }
        }

        if (maxPosition !== limit) {
            maxPosition = maxPosition + 1;
        }

        return maxPosition;
    };

    var changeExtraFieldSystem = function () {
        $('#ExtraFieldsName').val($('#ExtraFieldsSystemId').find(':selected').text());
        $('#ExtraFieldsId').val($('#ExtraFieldsSystemId').val());
        $('#ExtraFieldsRegularId').val(0);
        app.common.ui.GetDataById($('#ExtraFieldsId').val(), app.config.Urls.getExtraFields, getExtraFieldsValues)
    };

    var changeExtraFieldRegular = function () {
        $('#ExtraFieldsName').val($('#ExtraFieldsRegularId').find(':selected').text());
        $('#ExtraFieldsId').val($('#ExtraFieldsRegularId').val());
        $('#ExtraFieldsSystemId').val(0);
        app.common.ui.GetDataById($('#ExtraFieldsId').val(), app.config.Urls.getExtraFields, getExtraFieldsValues)
    };

    var changeNewExtraField = function () {
        $('#ExtraFieldsName').val($('#NewExtraFieldsRegularName').val());
        $('#ExtraFieldsRegularId').val(0);
        $('#ExtraFieldsSystemId').val(0);
    };

    function getExtraFieldsValues(result) {
        $('#ExtraFieldsDescription').val(result.description);
        $('#TypeId').val(result.type);
        $('#TypeName').val($('#TypeId').find(':selected').text());
        $('#DelSystem').val(result.delSystem);
        $('#DelSystem').prop('checked', result.delSystem);
        $('#MultipleChoice').val(result.multipleChoice);
        $('#MultipleChoice').prop('checked', result.multipleChoice);
        $('#AllowedStringValues').val(result.allowedStringValues);
        $('#IsMandatory').val(result.isMandatory);
        $('#IsMandatory').prop('checked', result.isMandatory);
    }

    var changeType = function () {
        $('#TypeName').val($('#TypeId').find(':selected').text());
        visibilityForOptionals();
    };

    var changeErpSystemInstanceQuery = function () {
        $('#ErpSystemInstanceQueryName').val($('#ErpSystemInstanceQueryId').find(':selected').text());
    };

    function visibilityForOptionals() {
        var item = selectedExtraFieldsType.find(function (element) {
            return '' + element['Id'] + '' === $('#TypeId').val();
        });

        if (item !== undefined) {
            if (item.IsMandatoryVisibility) $('#divismandatory').removeClass('d-none');
            else $('#divismandatory').addClass('d-none');

            if (item.ErpSystemVisibility) $('#diverpsysteminstancequeryid').removeClass('d-none');
            else $('#diverpsysteminstancequeryid').addClass('d-none');

            if (item.AllowedValuesVisibility) $('#divallowedstringvalues').removeClass('d-none');
            else $('#divallowedstringvalues').addClass('d-none');

            if (item.MultipleChoiceVisibility) $('#divmultiplechoice').removeClass('d-none');
            else $('#divmultiplechoice').addClass('d-none');
        }
    }

    var editDisplay = function () {
        $('#divnewextrafieldsid').addClass('d-none');
        $('#TypeId').addClass('d-none');
        $('#divextrafieldssystemid').addClass('d-none');
        $('#divextrafieldsregularid').addClass('d-none');
        $('#divdescription').addClass('d-none');
        $('#divtypeid').addClass('d-none');
        $('#divdelsystem').addClass('d-none');
        $('#divposition').removeClass('d-none');
        $('#divismandatory').addClass('d-none');
        $('#diverpsysteminstancequeryid').addClass('d-none');
        $('#divallowedstringvalues').addClass('d-none');
        $('#divmultiplechoice').addClass('d-none');
    };

    return {
        ChangeExtraFieldSystem: changeExtraFieldSystem,
        ChangeExtraFieldRegular: changeExtraFieldRegular,
        ChangeNewExtraField: changeNewExtraField,
        ChangeType: changeType,
        ChangeErpSystemInstanceQuery: changeErpSystemInstanceQuery,
        OnCreate: onCreate,
        OnEdit: onEdit,
        OnSaveModal: onSaveModal,
        onValidationError: onValidationError
    };
})();