var app = app || {};
app.modalControl = app.modalControl || {};

app.modalControl = (function (event) {
    var getLiteralPreconditionUrl = '';
    var getLiteralValuesUrl = '';
    var getAllPreconditionTypesUrl = '';
    var postNewLiteralPreconditionUrl = '';
    var postEditLiteralPreconditionUrl = '';
    var getAllPreconditionTypesByPreconditionUrl = '';
    var createPreconditionUrl = '';

    const BillableTypeConstant = app.literalPreconditionTypes.constants.Billable;
    const ActuationDateTypeConstant = app.literalPreconditionTypes.constants.ActuationDate;
    const AssignmentDateTypeConstant = app.literalPreconditionTypes.constants.AssignmentDate;
    const ClientClosureDateTypeConstant = app.literalPreconditionTypes.constants.ClientClosureDate;
    const CollectionDateTypeConstant = app.literalPreconditionTypes.constants.CollectionDate;
    const CreateDateTypeConstant = app.literalPreconditionTypes.constants.CreateDate;
    const SaltoClosureDateTypeConstant = app.literalPreconditionTypes.constants.SaltoClosureDate;
    const WOClientClosureDateTypeConstant = app.literalPreconditionTypes.constants.WOClientClosureDate;

    let globalTaskId = 0;
    let globalPreconditionId;
    let globalPostconditionCollectionId;
    let globalAction = '';
    const AddAction = 'add';
    const EditAction = 'edit';


    var init = function (options) {
        getLiteralPreconditionUrl = options.getLiteralPreconditionUrl;
        getLiteralValuesUrl = options.getLiteralValuesUrl;
        getAllPreconditionTypesUrl = options.getAllPreconditionTypesUrl;
        postNewLiteralPreconditionUrl = options.postNewLiteralPreconditionUrl;
        postEditLiteralPreconditionUrl = options.postEditLiteralPreconditionUrl;
        getAllPreconditionTypesByPreconditionUrl = options.getAllPreconditionTypesByPreconditionUrl;
        createPreconditionUrl = options.createPreconditionUrl;
    };

    var loadComboTypes = function (types) {
        $('#precondition-combo-types').empty();

        var html = '';
        for (var i = 0; i < types.length; i++) {
            var type = types[i];
            html = html + '<option value="' + type.id + '">' + type.name + '</option>';
        }

        $('#precondition-combo-types').append(html);
        if (globalAction == AddAction) {
            $('#precondition-combo-types').prop("disabled", false);

        } else if (globalAction == EditAction) {
            $('#precondition-combo-types').prop("disabled", true);
        }

        $('#precondition-combo-types').val(type.id);

        $('#precondition-combo-types').change(function () {
            var comboSelected = $('#precondition-combo-types :selected').text();

            if (checkModalVisibility(comboSelected)) {
                var getLiteralValues = apiCall(getLiteralValuesUrl, 'GET', 'json', { id: 0, preconditionId: globalPreconditionId, type: comboSelected });
                getLiteralValues.done(function (res) {
                    setLiteralValues(res);
                });
            }
        });
    };

    var checkModalVisibility = function (comboSelected) {
        if (comboSelected == BillableTypeConstant) {
            if ($('#boolean-literalValue').hasClass('d-none')) {
                $('#boolean-literalValue').removeClass('d-none');
            }
            $('#data-literalValue').addClass('d-none');
            $('#MultiSelectModal').addClass('d-none');
        } else if (
            comboSelected == ActuationDateTypeConstant ||
            comboSelected == AssignmentDateTypeConstant ||
            comboSelected == ClientClosureDateTypeConstant ||
            comboSelected == CollectionDateTypeConstant ||
            comboSelected == CreateDateTypeConstant ||
            comboSelected == SaltoClosureDateTypeConstant ||
            comboSelected == WOClientClosureDateTypeConstant
        ) {
            if ($('#data-literalValue').hasClass('d-none')) {
                $('#data-literalValue').removeClass('d-none');
            }
            $('#boolean-literalValue').addClass('d-none');
            $('#MultiSelectModal').addClass('d-none');
        } else {
            if ($('#MultiSelectModal').hasClass('d-none')) {
                $('#MultiSelectModal').removeClass('d-none');
            }
            $('#data-literalValue').addClass('d-none');
            $('#boolean-literalValue').addClass('d-none');
            return true;
        }
        return false;
    };

    var setComboTypes = function (typeId) {
        $('#precondition-combo-types').val(typeId);
    };

    var setLiteralValues = function (literalValues) {
        var comboSelected = $('#precondition-combo-types :selected').text();

        checkModalVisibility(comboSelected);

        if (comboSelected == BillableTypeConstant) {
            $('#switch-value').prop('checked', literalValues[0].booleanValue);

        } if (
            comboSelected == ActuationDateTypeConstant ||
            comboSelected == AssignmentDateTypeConstant ||
            comboSelected == ClientClosureDateTypeConstant ||
            comboSelected == CollectionDateTypeConstant ||
            comboSelected == CreateDateTypeConstant ||
            comboSelected == SaltoClosureDateTypeConstant ||
            comboSelected == WOClientClosureDateTypeConstant
        ) {
            $('#data-value').val(literalValues[0].enterValue);

        } else {
            $("#Container_MultiSelectModal").html('');
            if (literalValues.length > 0) {
                for (var i = 0; i < literalValues.length; i++) {
                    var item = literalValues[i];
                    var inputId = 'literalValue-' + i;
                    var checked = item.isChecked ? 'checked="checked"' : '';

                    var html = '<div class="custom-control custom-checkbox">'
                        + '<input id="' + inputId + '"' + checked + '" type="checkbox" class="custom-control-input" />'
                        + '<label for= ' + inputId + ' class="custom-control-label">' + item.labelName + '</label>'
                        + '<input id="' + inputId + '-hidden" value="' + item.value + '" type="hidden" />'
                        + '</div>';
                    $("#Container_MultiSelectModal").append(html);
                }
            }
        }

    };

    var openLiteralModal = function (action, literalId, preconditionId, taskId, postconditionCollectionId) {
        globalTaskId = taskId;
        globalPreconditionId = preconditionId;
        globalAction = action;
        globalPostconditionCollectionId = postconditionCollectionId;

        var url = '';
        if (globalAction == AddAction) {
            url = getAllPreconditionTypesByPreconditionUrl;
        } else if (globalAction == EditAction) {
            url = getAllPreconditionTypesUrl;
        }

        var getAllPreconditionTypesByPrecondition = apiCall(url, 'GET', 'json', { id: preconditionId });

        getAllPreconditionTypesByPrecondition.done(function (res) {
            loadComboTypes(res);

            if (globalAction == AddAction) {
                var comboSelected = $('#precondition-combo-types :selected').text();

                var getLiteralValues = apiCall(getLiteralValuesUrl, 'GET', 'json', { id: literalId, preconditionId: globalPreconditionId, type: comboSelected });

                getLiteralValues.done(function (res) {
                    setLiteralValues(res);
                });

            } else if (globalAction == EditAction) {
                $('#literalPreconditionId').val(literalId);
                var getLiteralPrecondition = apiCall(getLiteralPreconditionUrl, 'GET', 'json', { id: literalId });

                getLiteralPrecondition.done(function (res) {
                    setComboTypes(res.preconditionsTypeId);

                    var getLiteralValues = apiCall(getLiteralValuesUrl, 'GET', 'json', { id: literalId, preconditionId: globalPreconditionId, type: res.preconditionsTypeName });

                    getLiteralValues.done(function (res) {
                        setLiteralValues(res);
                    });
                });
            }
        });
    };

    var postModalData = function () {
        var actionUrl = '';
        var hasValueToPost = false;

        var literalPreconditionId = $('#literalPreconditionId').val();
        var literalPreconditionDto = {
            PreconditionId: globalPreconditionId,
            ComparisonOperator: 'Igual',
            PreconditionsTypeId: $('#precondition-combo-types :selected').val(),
            NomCampModel: $('#precondition-combo-types :selected').text(),
            PreconditionsLiteralValues: []
        };

        if (globalAction == AddAction) {
            actionUrl = postNewLiteralPreconditionUrl;
        } else if (globalAction == EditAction) {
            actionUrl = postEditLiteralPreconditionUrl;
            literalPreconditionDto.Id = $('#literalPreconditionId').val();;
        }
        var comboSelected = $('#precondition-combo-types :selected').text();
        if (comboSelected == BillableTypeConstant) {
            var literalValuesDto = {
                LiteralPreconditionId: literalPreconditionId,
                TypeId: $('#precondition-combo-types').val(),
                BooleanValue: $('#switch-value').prop('checked'),
            };
            literalPreconditionDto.PreconditionsLiteralValues.push(literalValuesDto);
            hasValueToPost = true;
        } else if (
            comboSelected == ActuationDateTypeConstant ||
            comboSelected == AssignmentDateTypeConstant ||
            comboSelected == ClientClosureDateTypeConstant ||
            comboSelected == CollectionDateTypeConstant ||
            comboSelected == CreateDateTypeConstant ||
            comboSelected == SaltoClosureDateTypeConstant ||
            comboSelected == WOClientClosureDateTypeConstant
        ) {
            literalPreconditionDto.ComparisonOperator = $('#data-comparisonOperator').val();

            var enterValue = $('#data-value').val();
            if (enterValue == "") {
                enterValue = 0;
            } else {
                hasValueToPost = true;
            }

            let literalValuesDto = {
                LiteralPreconditionId: literalPreconditionId,
                TypeId: $('#precondition-combo-types').val(),
                EnterValue: enterValue
            };
            literalPreconditionDto.PreconditionsLiteralValues.push(literalValuesDto);
        } else {
            var valuesChecked = $('#Container_MultiSelectModal').find($('input:checked'));
            if (valuesChecked) {
               hasValueToPost = true;
               for (var x = 0; x < valuesChecked.length; x++) {
                    var inputId = valuesChecked[x].id;
                    var hiddenInputValue = $('#' + inputId + '-hidden').val();
                    let literalValuesDto = {
                        LiteralPreconditionId: literalPreconditionId,
                        TypeId: hiddenInputValue
                    };
                    literalPreconditionDto.PreconditionsLiteralValues.push(literalValuesDto);
                }
            }
        }
        if (hasValueToPost) {
            if (globalPreconditionId == 0) {
                var createPrecondition = apiCall(createPreconditionUrl, 'POST', 'json', { id: globalTaskId, postconditionCollectionId: globalPostconditionCollectionId });

                createPrecondition.done(function (res) {
                    var precondition = res;
                    literalPreconditionDto.PreconditionId = precondition.id;
                    $.post(actionUrl, literalPreconditionDto).done(function (data) {
                        app.tasks.CheckCleanAndReloadPage(globalTaskId);
                    });
                });
            } else {
                $.post(actionUrl, literalPreconditionDto).done(function (data) {
                    app.tasks.CheckCleanAndReloadPage(globalTaskId);
                });
            }
        }
    };

    return {
        Init: init,
        OpenLiteralModal: openLiteralModal,
        PostModalData: postModalData,
    };
})();