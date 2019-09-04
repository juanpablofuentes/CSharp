var app = app || {};
app.triggerModal = app.triggerModal || {};

app.triggerModal = (function (event) {
    const AssignmentDateTypeConstant = app.triggerTypes.constants.AssignmentDate;
    const ActuationDateTypeConstant = app.triggerTypes.constants.ActuationDate;
    const CreateTypeConstant = app.triggerTypes.constants.Create;
    const NoActionTypeConstant = app.triggerTypes.constants.NoAction;
    const RestartSlaWatchTypeConstant = app.triggerTypes.constants.RestartSlaWatch;
    const StopSlaWatchTypeConstant = app.triggerTypes.constants.StopSlaWatch;
    const TechnicianAndActuationDateTypeConstant = app.triggerTypes.constants.TechnicianAndActuationDate;
    const WoReopeningTypeConstant = app.triggerTypes.constants.WoReopening;

    var getTriggerTypesUrl = '';

    var init = function (options) {
        getTriggerTypesUrl = options.getTriggerTypesUrl;
        getTriggerTypesValuesUrl = options.getTriggerTypesValuesUrl;
        postTriggerUrl = options.postTriggerUrl;
        $('form input').keydown(function (e) {
            if (e.keyCode == 13) {
                e.preventDefault();
                return false;
            }
        });
    };

    var openTriggerModal = function () {
        $('#triggers-combo-types').empty();
        $('#triggers-combo-values').empty();
        loadTriggerComboTypes();
    }

    var loadTriggerComboTypes = function () {
        var getAllPreconditionTypesByPrecondition = apiCall(getTriggerTypesUrl, 'GET', 'json');

        getAllPreconditionTypesByPrecondition.done(function (res) {
            setTriggerComboTypes(res);
        });
    };

    var setTriggerComboTypes = function (types) {
        app.common.ui.LoadSelectorKeyValue('triggers-combo-types', types, 'id', 'name', false);

        var typeId = $('#h-typeId-' + CurrentTaskId + '-trigger').val();
        $('#triggers-combo-types').val(typeId).change();

        $('#triggers-combo-types').change(function () {
            var comboSelected = $('#triggers-combo-types :selected').text();

            if (checkModalVisibility(comboSelected)) {
                var getTriggerValues = apiCall(getTriggerTypesValuesUrl, 'GET', 'json', { triggerTypeName: comboSelected });
                getTriggerValues.done(function (res) {
                    setTriggerComboValues(res);
                });
            }
        });
        $('#triggers-combo-types').change();
    }

    var setTriggerComboValues = function (types) {
        app.common.ui.LoadSelectorKeyValue('triggers-combo-values', types, 'id', 'name', false);
        var value = $('#h-value-' + CurrentTaskId + '-trigger').val();
        var typeId = $('#h-typeId-' + CurrentTaskId + '-trigger').val();

        if (typeId == $('#triggers-combo-types').val()) {
            $('#triggers-combo-values').val(value);
        }
    }

    var checkModalVisibility = function (comboSelected) {
        if (
            comboSelected == ActuationDateTypeConstant ||
            comboSelected == AssignmentDateTypeConstant ||
            comboSelected == CreateTypeConstant ||
            comboSelected == NoActionTypeConstant ||
            comboSelected == RestartSlaWatchTypeConstant ||
            comboSelected == TechnicianAndActuationDateTypeConstant ||
            comboSelected == StopSlaWatchTypeConstant ||
            comboSelected == WoReopeningTypeConstant
        ) {
            $('#triggers-combo-values').addClass('d-none');
            return false;
        } else {
            if ($('#triggers-combo-values').hasClass('d-none')) {
                $('#triggers-combo-values').removeClass('d-none');
            }
            return true;
        }
    };

    var postModalTriggerData = function () {

        var triggerDto = {
            TaskId: CurrentTaskId,
            TypeId: $('#triggers-combo-types :selected').val(),
            TypeName: $('#triggers-combo-types :selected').text(),
            Value: $('#triggers-combo-values :selected').text(),
            ValueId: $('#triggers-combo-values :selected').val(),
        };

        $.post(postTriggerUrl, triggerDto).done(function (data) {
            console.log(data.data.taskId);
            app.trigger.LoadTrigger(data.data.taskId);
        });
    };

    return {
        Init: init,
        PostModalTriggerData: postModalTriggerData,
        OpenTriggerModal: openTriggerModal,
        PostModalTriggerData: postModalTriggerData,
    };
})();