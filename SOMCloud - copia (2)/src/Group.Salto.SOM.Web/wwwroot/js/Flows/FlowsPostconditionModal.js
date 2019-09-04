var app = app || {};
app.postconditionModal = app.postconditionModal || {};

app.postconditionModal = (function (event) {

    const ActuationDateTypeConstant = app.postconditionTypes.constants.ActuationDate;
    const ActuationEndDateTypeConstant = app.postconditionTypes.constants.ActuationEndDate;
    const BillableTypeConstant = app.postconditionTypes.constants.Billable;
    const ClientClosureDateTypeConstant = app.postconditionTypes.constants.ClientClosureDate;
    const AssignmentDateTypeConstant = app.postconditionTypes.constants.AssignmentDate;
    const InternalClosureDateTypeConstant = app.postconditionTypes.constants.InternalClosureDate;
    const PickupDateTypeConstant = app.postconditionTypes.constants.PickupDate;
    const ResolutionDateTypeConstant = app.postconditionTypes.constants.ResolutionDate;
    const ActionDateTypeConstant = app.postconditionTypes.constants.ActionDate;
    const ManipulatorTypeConstant = app.postconditionTypes.constants.Manipulator;
    const WoObservationsTypeConstant = app.postconditionTypes.constants.WoObservations;
    const WoReopeningPolicyTypeConstant = app.postconditionTypes.constants.WoReopeningPolicy;
    const TypeOtN1TypeConstant = app.postconditionTypes.constants.TypeOtN1;

    var getPostconditionValuesUrl = '', postNewPostconditionUrl = '', postEditPostconditionUrl = '', getAllPostconditionTypesUrl = '', postNewPostconditionCollectionUrl = '', getTypeOtnValuesUrl = '';
    var globalTaskId, globalPostconditionId, globalAction, globalPostconditionCollectionId, globalTypeId, globalPostconditionTypeId, globalEnterValue, globalSwitchValue;

    var init = function (options) {
        getPostconditionTypesByPostconditionUrl = options.getPostconditionTypesByPostconditionUrl;
        getPostconditionValuesUrl = options.getPostconditionValuesUrl;
        postNewPostconditionUrl = options.postNewPostconditionUrl;
        postNewPostconditionCollectionUrl = options.postNewPostconditionCollectionUrl;
        postEditPostconditionUrl = options.postEditPostconditionUrl;
        getAllPostconditionTypesUrl = options.getAllPostconditionTypesUrl;
        getTypeOtnValuesUrl = options.getTypeOtnValuesUrl;
        typeOtnListeners();
    };

    var typeOtnListeners = function()
    {
        $('#typeotn2-combo-values').change(function () {
            $('#typeotn3-combo-values').empty();
            $('#typeotn4-combo-values').empty();
            $('#typeotn5-combo-values').empty();
            var selected = $('#typeotn2-combo-values').val();
            if (selected != null) {
                var getTypeOtnValues = apiCall(getTypeOtnValuesUrl, 'GET', 'json', { id: selected });
                getTypeOtnValues.done(function (values) {
                    app.common.ui.LoadSelectorKeyValue('typeotn3-combo-values', values, 'id', 'name', true, null);
                });
            }
        });

        $('#typeotn3-combo-values').change(function () {
            $('#typeotn4-combo-values').empty();
            $('#typeotn5-combo-values').empty();

            var selected = $('#typeotn3-combo-values').val();
            if (selected != null) {
                var getTypeOtnValues = apiCall(getTypeOtnValuesUrl, 'GET', 'json', { id: selected });
                getTypeOtnValues.done(function (values) {
                    app.common.ui.LoadSelectorKeyValue('typeotn4-combo-values', values, 'id', 'name', true, null);
                });
            }
        });

        $('#typeotn4-combo-values').change(function () {
            $('#typeotn5-combo-values').empty();

            var selected = $('#typeotn4-combo-values').val();
            if (selected != null) {
                var getTypeOtnValues = apiCall(getTypeOtnValuesUrl, 'GET', 'json', { id: selected });
                getTypeOtnValues.done(function (values) {
                    app.common.ui.LoadSelectorKeyValue('typeotn5-combo-values', values, 'id', 'name', true, null);
                });
            }
        });
    }
    
    var openPostconditionModal = function (options) {
        globalAction = options.action;
        globalLiteralId = options.literal;
        globalPostconditionId = options.postcondition;
        globalTaskId = options.task;
        globalPostconditionTypeId = options.postconditionTypeId;
        globalTypeId = options.typeId;
        globalPostconditionCollectionId = options.postconditioncollection;
        globalEnterValue = options.enterValue;
        globalSwitchValue = options.switchValue;

        $('#postcondition-combo-types').empty();
        $('#postcondition-combo-values').empty();

        if (globalAction == 'edit') {
            loadPostconditionComboTypes(getAllPostconditionTypesUrl);
        } else {
            loadPostconditionComboTypes(getPostconditionTypesByPostconditionUrl);
        }
    };

    var postPostconditionModal = function () {
        var PostconditionDto = {
            Id: "",
            PostconditionCollectionsId: globalPostconditionCollectionId,
            NameFieldModel: $('#postcondition-combo-types :selected').text(),
            TypeId: '',
            EnterValue: '',
            StringValue: '',
            BooleanValue: '',
        }
        var comboSelected = $('#postcondition-combo-types :selected').text();

        if (comboSelected == ActuationDateTypeConstant ||
            comboSelected == ActionDateTypeConstant ||
            comboSelected == AssignmentDateTypeConstant ||
            comboSelected == ActuationEndDateTypeConstant ||
            comboSelected == InternalClosureDateTypeConstant ||
            comboSelected == PickupDateTypeConstant ||
            comboSelected == ClientClosureDateTypeConstant ||
            comboSelected == ResolutionDateTypeConstant) {
            var enterV = $('#data-value-postcondition').val();
            if (enterV == null) {
                enterV = 0;
            }
            PostconditionDto.EnterValue = enterV;
        } else if (comboSelected == BillableTypeConstant) {
            PostconditionDto.BooleanValue = $('#switch-value-postconditions').prop('checked');
        } else if (comboSelected == ManipulatorTypeConstant) {
            var manipulatorValue = $('#select-manipulator').val();
            if (manipulatorValue == "null") {
                PostconditionDto.TypeId = null;
            } else {
                PostconditionDto.TypeId = manipulatorValue;
            }
        } else if (comboSelected == WoObservationsTypeConstant) {
            PostconditionDto.StringValue = $('#text-value-postcondition').val();
        } else if (comboSelected == WoReopeningPolicyTypeConstant) {
            PostconditionDto.EnterValue = $('#select-woreopening').val();
        } else {
            PostconditionDto.TypeId = $('#postcondition-combo-values :selected').val();
        }

        if (globalAction == 'edit') {
            PostconditionDto.Id = globalPostconditionId;
            postPostconditionFunc(postEditPostconditionUrl, PostconditionDto);

        } else if (globalPostconditionCollectionId == 0) {
            var postPostconditionCollection = apiCall(postNewPostconditionCollectionUrl, 'POST', 'json', { taskId: globalTaskId });

            postPostconditionCollection.done(function (res) {
                PostconditionDto.PostconditionCollectionsId = res.id;
                postPostconditionFunc(postNewPostconditionUrl, PostconditionDto);
            });
        } else {
            PostconditionDto.Id = 0;
            postPostconditionFunc(postNewPostconditionUrl, PostconditionDto);
        }
    };

    var postPostconditionFunc = function (urlPost, PostconditionDto) {
        var postPostcondition = apiCall(urlPost, 'POST', 'json', { postcondition: PostconditionDto });

        postPostcondition.done(function (res) {
            cleanAndReloadPostconditions();
        });
    }

    var loadPostconditionComboTypes = function (url) {
        var getAllPreconditionTypesByPrecondition = apiCall(url, 'GET', 'json', { id: globalPostconditionCollectionId });

        getAllPreconditionTypesByPrecondition.done(function (res) {
            setPostconditionComboTypes(res);
        });
    };

    var setPostconditionComboTypes = function (types) {
        app.common.ui.LoadSelectorKeyValue('postcondition-combo-types', types, 'id', 'name', false);
        $('#postcondition-combo-types').change(function () {
            var comboSelected = $('#postcondition-combo-types :selected').text();
            if (checkModalVisibility(comboSelected)) {
                var getPostconditionValues = apiCall(getPostconditionValuesUrl, 'GET', 'json', { postconditionTypeName: comboSelected });
                getPostconditionValues.done(function (res) {
                    setPostconditionValues(res);
                });
            } else {
                if (comboSelected == BillableTypeConstant) {
                    $('#switch-value-postconditions').val(globalSwitchValue);
                } else {
                    $('#data-value-postcondition').val(globalEnterValue);
                }
            }
        });
        if (globalPostconditionTypeId != undefined) {
            $('#postcondition-combo-types').val(globalPostconditionTypeId).change();
            $('#postcondition-combo-types').prop("disabled", true);
        }
        else {
            $('#postcondition-combo-types option:first').attr('selected', 'selected').change();
            $('#postcondition-combo-types').prop("disabled", false);
        }
    }

    var setPostconditionValues = function (types) {
        app.common.ui.LoadSelectorKeyValue('postcondition-combo-values', types, 'id', 'name', false);
        if (globalTypeId != undefined) {
            $('#postcondition-combo-values').val(globalTypeId);
        }
        else {
            $('#postcondition-combo-values option:first').attr('selected', 'selected');
        }

        $('#postcondition-combo-values').change(function () {

            var comboSelected = $('#postcondition-combo-types :selected').text();
            if (comboSelected == TypeOtN1TypeConstant) {
                $('#typeotn2-combo-values').empty();
                $('#typeotn3-combo-values').empty();
                $('#typeotn4-combo-values').empty();
                $('#typeotn5-combo-values').empty();

                var selected = $('#postcondition-combo-values').val();
                var getTypeOtnValues = apiCall(getTypeOtnValuesUrl, 'GET', 'json', { id: selected });
                getTypeOtnValues.done(function (values) {
                    app.common.ui.LoadSelectorKeyValue('typeotn2-combo-values', values, 'id', 'name', true, null);
                });
            }
        });
    }

    var checkModalVisibility = function (comboSelected) {
        $('#data-literalValue-postcondition').addClass('d-none');
        $('#boolean-literalValue-postcondition').addClass('d-none');
        $('#manipulator-postcondition').addClass('d-none');
        $('#text-postcondition').addClass('d-none');
        $('#woreopening-postcondition').addClass('d-none');
        $('#typeotn-container').addClass('d-none');
        $('#postcondition-combo-values').addClass('d-none');
        $('#boolean-literalValue-postcondition').addClass('d-none');
        $('#manipulator-postcondition').addClass('d-none');
        $('#text-postcondition').addClass('d-none');
        $('#woreopening-postcondition').addClass('d-none');

        if (
            comboSelected == ActuationDateTypeConstant ||
            comboSelected == ActionDateTypeConstant ||
            comboSelected == AssignmentDateTypeConstant ||
            comboSelected == ActuationEndDateTypeConstant ||
            comboSelected == InternalClosureDateTypeConstant ||
            comboSelected == PickupDateTypeConstant ||
            comboSelected == BillableTypeConstant ||
            comboSelected == ClientClosureDateTypeConstant ||
            comboSelected == ResolutionDateTypeConstant ||
            comboSelected == ManipulatorTypeConstant ||
            comboSelected == WoObservationsTypeConstant ||
            comboSelected == WoReopeningPolicyTypeConstant
        ) {
            $('#data-literalValue-postcondition').removeClass('d-none');
           

            if (comboSelected == BillableTypeConstant) {
                $('#boolean-literalValue-postcondition').removeClass('d-none');
            }

            if (comboSelected == ManipulatorTypeConstant) {
                $('#manipulator-postcondition').removeClass('d-none');
            }

            if (comboSelected == WoObservationsTypeConstant) {
                $('#text-postcondition').removeClass('d-none');
            }

            if (comboSelected == WoReopeningPolicyTypeConstant)
            {
                $('#woreopening-postcondition').removeClass('d-none');
            }

            return false;

        } else if (comboSelected == TypeOtN1TypeConstant)
        {
            $('#postcondition-combo-values').removeClass('d-none');
            $('#typeotn-container').removeClass('d-none');

            return true;
        } else
        {
            $('#postcondition-combo-values').removeClass('d-none');
            return true;
        }
    };

    function cleanAndReloadPostconditions(taskId) {
        showSpinner();
        var postconditionCollectionContainer = $('#container-postconditionCollection-' + globalTaskId).children();
        postconditionCollectionContainer.each(function () {
            if (!$(this).hasClass('row')) {
                $(this).remove();
            }
            else if ($(this).hasClass('conditions-row-add')) {
                $(this).remove();
            }
        });
        app.postconditions.GetPostconditionsCollectionsByTask(globalTaskId);
    }

    return {
        Init: init,
        OpenPostconditionModal: openPostconditionModal,
        PostPostconditionModal: postPostconditionModal,
    };
})();