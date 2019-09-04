var app = app || {};
app.postconditions = app.postconditions || {};

app.postconditions = (function (event) {
    var getPostconditionByTaskUrl = '', textGreaterThan, textLessThan, textSystemDate, textMinutes, textModify, textDeny, textDuplicate, canDeletePostconditionCollectionUrl = '';
    const ActuationDateTypeConstant = app.postconditionTypes.constants.ActuationDate;
    const ActuationEndDateTypeConstant = app.postconditionTypes.constants.ActuationEndDate;
    const BillableTypeConstant = app.postconditionTypes.constants.Billable;
    const ClientClosureDateTypeConstant = app.postconditionTypes.constants.ClientClosureDate;
    const AssignmentDateTypeConstant = app.postconditionTypes.constants.AssignmentDate;
    const SaltoClosureDateTypeConstant = app.postconditionTypes.constants.SaltoClosureDate;
    const PickupDateTypeConstant = app.postconditionTypes.constants.PickupDate;
    const ResolutionDateTypeConstant = app.postconditionTypes.constants.ResolutionDate;
    const ActionDateTypeConstant = app.postconditionTypes.constants.ActionDate;
    const ManipulatorTypeConstant = app.postconditionTypes.constants.Manipulator;
    const WoObservationsTypeConstant = app.postconditionTypes.constants.WoObservations;
    const WoReopeningPolicyTypeConstant = app.postconditionTypes.constants.WoReopeningPolicy;

    const DenyConstant = app.postcondition.constants.Deny;
    const ModifyConstant = app.postcondition.constants.Modify;
    const DuplicateConstant = app.postcondition.constants.Duplicate;
    const UnassignConstant = app.postcondition.constants.UnassignConstant;

    var init = function (options) {
        getPostconditionByTaskUrl = options.getPostconditionByTaskUrl;
        deletePostconditionCollectionUrl = options.deletePostconditionCollectionUrl;
        canDeletePostconditionCollectionUrl = options.canDeletePostconditionCollectionUrl;
        deletePostconditionUrl = options.deletePostconditionUrl;
        deleteAllPostconditionUrl = options.deleteAllPostconditionUrl;
        textGreaterThan = options.textGreaterThan;
        textYes = options.textYes;
        textNo = options.textNo;
        textLessThan = options.textLessThan;
        textSystemDate = options.textSystemDate;
        textMinutes = options.textMinutes;
        textModify = options.textModify;
        textDeny = options.textDeny;
        textDuplicate = options.textDuplicate;
        textUnassign = options.textUnassign;
        textSameToTheTechnician = options.textSameToTheTechnician;
        textConfirmDelete = options.textConfirmDelete;
        textNoDelete = options.textNoDelete;
        textNone = options.textNone;
    };

    function showPostconditionsTab() {
        showSpinner();
        getPostconditionsCollectionsByTask(CurrentTaskId);
    }

    function getPostconditionsCollectionsByTask(id) {
        var url = getPostconditionByTaskUrl;
        var getPostconditionsCollections = apiCall(url, 'GET', 'json', { id: id });

        var mainCollectionContainer = $("#container-postconditionCollection-" + CurrentTaskId);
        var collectionTemplate = $('#postconditionCollectionTemplate-' + CurrentTaskId);

        if (collectionTemplate.hasClass('d-none')) {
            collectionTemplate.removeClass('d-none');
        }

        getPostconditionsCollections.done(function (res) {
            var postconditionsCollectionsData = res.data;
            for (var i = 0; i < postconditionsCollectionsData.length; i++) {
                var postconditionCol = postconditionsCollectionsData[i];
                var newCollection = collectionTemplate.clone();

                newCollection.attr('id', 't-' + CurrentTaskId + '-postcollection-' + postconditionCol.id);

                var containerPreconditions = newCollection.find('#container-preconditions-' + CurrentTaskId);
                var containerPostconditions = newCollection.find('#container-postconditions-' + CurrentTaskId);

                var preconditions = postconditionCol.preconditionsList;
                var postconditions = postconditionCol.postconditionsList;

                app.tasks.LoadPreconditions(preconditions, containerPreconditions, '-pp-', postconditionCol.id);

                loadPostconditions(postconditions, postconditionCol.id, containerPostconditions);

                var options = {
                    area: 'global',
                    actionUrl: '#',
                    id: 'postconditionColection-' + postconditionCol.id,
                    target: '#modalPostcondition',
                    literalId: 0,
                    postconditionId: 0,
                    postconditionCollectionId: postconditionCol.id,
                    taskId: CurrentTaskId,
                    enterValue: null,
                    switchValue: null,
                    printDeleteButton: true,
                    printAddButton: false,
                }

                var buttonsEdits = addButtonsForEdit(options);
                newCollection.append(buttonsEdits);
                mainCollectionContainer.prepend(newCollection);

                var id = 'postconditionColection-' + postconditionCol.id + '-delete';
                newCollection.find('a[id*=' + id + ']').on('click', function () {
                    showSpinner();
                    var postconditioncollection = $(this).data('postconditioncollectionid');
                    var task = $(this).data('taskid');
                    $("#confirmationDeleteFlows").modal("toggle");
                    canDeletePostconditionCollection(postconditioncollection, task);
                    hideSpinner();
                });
            }

            var options = {
                area: 'global',
                actionUrl: '#',
                id: 'postconditionColection-new-' + CurrentTaskId,
                target: '#modalPostcondition',
                literalId: 0,
                postconditionId: 0,
                postconditionCollectionId: 0,
                taskId: CurrentTaskId,
                typeId: 0,
                printDeleteButton: false,
                printAddButton: true,
            }
            var buttonsEdits = addButtonsForEdit(options);
            mainCollectionContainer.append(buttonsEdits);

            var id = 'postconditionColection-new-' + CurrentTaskId + '-add';
            mainCollectionContainer.find('a[id*=' + id + ']').on('click', function () {
                showSpinner();
                var literal = $(this).data('literalid');
                var postconditioncollection = $(this).data('postconditioncollectionid');
                var postcondition = $(this).data('postconditionid');
                var task = $(this).data('taskid');
                var options =
                {
                    action: 'add',
                    literal: literal,
                    postcondition: postcondition,
                    task: task,
                    postconditioncollection: postconditioncollection,
                }
                app.postconditionModal.OpenPostconditionModal(options);
                hideSpinner();
            });

            collectionTemplate.addClass('d-none');

            function loadPostconditions(data, colectionId, containerPostconditions) {
                var PostconditionsArray = data;
                var mainContainer = containerPostconditions;
                mainContainer.attr("id", "t-" + CurrentTaskId + "-postconditioncolection-" + colectionId);

                var literalPreconditionId = '#t-' + CurrentTaskId + '-Pre-Lit';
                var literalValuesId = '#t-' + CurrentTaskId + '-Pre-Values';

                var literalsContainerId = '#container-literals';
                var literalsContainer = mainContainer.find(literalsContainerId);
                var deleteB = false;

                if (PostconditionsArray != null) {
                    for (var i = 0; i < PostconditionsArray.length; i++) {
                        var postconditionItem = PostconditionsArray[i];
                        literalsContainer.attr('id', 'task-' + CurrentTaskId + '-containerPostconditions-' + postconditionItem.id);

                        var containerFieldModel = mainContainer.find(literalPreconditionId).clone();
                        containerFieldModel.attr('id', 't-' + CurrentTaskId + '-postconditionItem-' + postconditionItem.id);
                        containerFieldModel.append('<strong>' + postconditionItem.postconditionTypeName + ': </strong>');
                        literalsContainer.append(containerFieldModel);

                        var containerTypeName = mainContainer.find(literalValuesId).clone();
                        containerTypeName.attr('id', 't-' + CurrentTaskId + '-postconditionItemValue-' + postconditionItem.id);

                        var textToAppend = textForTypeName(postconditionItem);
                        containerTypeName.append(textToAppend + '<br>');

                        var options = {
                            area: 'subSections',
                            actionUrl: '#',
                            id: 'postconLiteral-' + postconditionItem.id,
                            target: '#modalPostcondition',
                            literalId: 0,
                            postconditionId: postconditionItem.id,
                            postconditionCollectionId: colectionId,
                            taskId: CurrentTaskId,
                            postconditionTypeId: postconditionItem.postconditionTypeId,
                            typeId: postconditionItem.typeId,
                            enterValue: postconditionItem.enterValue,
                            switchValue: postconditionItem.booleanValue,
                            printDeleteButton: true,
                            printAddButton: true,
                        }

                        var buttonsEdits = addButtonsForEdit(options);
                        literalsContainer.append(containerTypeName);
                        literalsContainer.append(buttonsEdits);
                    }
                    deleteB = true;
                }
                var options = {
                    area: 'section',
                    actionUrl: '#',
                    id: 'postconditions-' + colectionId,
                    target: '#modalPostcondition',
                    literalId: 0,
                    postconditionId: 0,
                    postconditionCollectionId: colectionId,
                    taskId: CurrentTaskId,
                    enterValue: null,
                    switchValue: null,
                    printDeleteButton: deleteB,
                    printAddButton: true,
                }

                var buttonsEdits = addButtonsForEdit(options);
                mainContainer.append(buttonsEdits);

                mainContainer.find('a[id*="-add"]').on('click', function () {
                    showSpinner();
                    var literal = $(this).data('literalid');
                    var postconditioncollection = $(this).data('postconditioncollectionid');
                    var postcondition = $(this).data('postconditionid');
                    var task = $(this).data('taskid');
                    var options =
                    {
                        action: 'add',
                        literal: literal,
                        postcondition: postcondition,
                        task: task,
                        postconditioncollection: postconditioncollection,
                    }
                    app.postconditionModal.OpenPostconditionModal(options);
                    hideSpinner();
                });

                mainContainer.find('a[id*="-edit"]').on('click', function () {
                    showSpinner();
                    var literal = $(this).data('literalid');
                    var postconditioncollection = $(this).data('postconditioncollectionid');
                    var postconditionTypeId = $(this).data('postconditiontypeid');
                    var typeId = $(this).data('typeid');
                    var postconditionAdd = $(this).data('postconditionid');
                    var task = $(this).data('taskid');
                    var enterValue = $(this).data('entervalue');
                    var switchValue = $(this).data('switchvalue');
                    var options =
                    {
                        action: 'edit',
                        literal: literal,
                        postcondition: postconditionAdd,
                        task: task,
                        postconditionTypeId: postconditionTypeId,
                        typeId: typeId,
                        postconditioncollection: postconditioncollection,
                        enterValue: enterValue,
                        switchValue: switchValue,
                    }
                    app.postconditionModal.OpenPostconditionModal(options);
                    hideSpinner();
                });

                mainContainer.find('a[id*="-delete"]').on('click', function (e) {
                    e.preventDefault();
                    e.stopPropagation();
                    showSpinner();
                    var postconditioncollection = $(this).data('postconditioncollectionid');
                    var postconditionDelete = $(this).data('postconditionid');
                    var task = $(this).data('taskid');

                    $('#confirmationDeleteFlows').on('show.bs.modal', function (event) {
                        $("#confirmationDeleteFlowsConfirmSave").on("click", function (f) {
                            if (postconditionDelete == 0 && postconditioncollection != null) {
                                deleteAllPostcondition(postconditioncollection, task);
                            }
                            else if (postconditionDelete != null) {
                                deletePostcondition(postconditionDelete, task);
                            }

                            $("#confirmationDeleteFlows").modal("toggle");
                            $("#confirmationDeleteFlowsconfirmationDeleteFlows").off("click");
                            postconditionDelete = null;
                            task = null;
                            postconditioncollection = null;
                            $(f.currentTarget).off();
                        });
                        hideSpinner();
                    });

                    $("#confirmationDeleteFlows").modal("toggle");
                    $("#textModalDialog").html(textConfirmDelete);
                    $("#confirmationDeleteFlowsConfirmSave").show();

                });
            }

        }).always(function (err) {
            hideSpinner();
        });
    }

    function cleanAndReloadPrePostconditionTask() {
        showSpinner();
        var postconditionCollectionContainer = $('#container-postconditionCollection-' + CurrentTaskId).children();
        postconditionCollectionContainer.each(function () {
            if (!$(this).hasClass('row')) {
                $(this).remove();
            } else if ($(this).hasClass('conditions-row-add')) {
                $(this).remove();
            }
        });
        getPostconditionsCollectionsByTask(CurrentTaskId);
    }

    function textForTypeName(literal) {
        var textToAppend = '';
        if (literal.postconditionTypeName == BillableTypeConstant) {
            if (literal.booleanValue) {
                textToAppend = textYes;
            } else {
                textToAppend = textNo;
            }
        }
        else if (literal.postconditionTypeName == ActuationDateTypeConstant ||
            literal.postconditionTypeName == ActionDateTypeConstant ||
            literal.postconditionTypeName == AssignmentDateTypeConstant ||
            literal.postconditionTypeName == ActuationEndDateTypeConstant ||
            literal.postconditionTypeName == SaltoClosureDateTypeConstant ||
            literal.postconditionTypeName == PickupDateTypeConstant ||
            literal.postconditionTypeName == ClientClosureDateTypeConstant ||
            literal.postconditionTypeName == ResolutionDateTypeConstant) {
            if (literal.enterValue == null) {
              textToAppend = textNone;
            } else {
                textToAppend = textGreaterThan + ' ' + textSystemDate + ' ' + literal.enterValue + textMinutes;
            }
        } else if (literal.postconditionTypeName == ManipulatorTypeConstant) {
            if (literal.typeId == UnassignConstant) {
                textToAppend = textUnassign;
            } else {
                textToAppend = textSameToTheTechnician;
            }
        } else if (literal.postconditionTypeName == WoObservationsTypeConstant) {
            textToAppend = literal.stringValue;
        } else if (literal.postconditionTypeName == WoReopeningPolicyTypeConstant) {
            if (literal.enterValue == DenyConstant) {
               textToAppend = textDeny;
            } else if (literal.enterValue == ModifyConstant) {
               textToAppend = textModify;
            } else if (literal.enterValue == DuplicateConstant) {
                textToAppend = textDuplicate;
            }
        } else {
            textToAppend = literal.typeName;
        }
        return textToAppend;
    }

    function addButtonsForEdit(options) {
        var options = {
            area: options.area,
            actionUrl: options.actionUrl,
            id: options.id,
            target: options.target,
            literalId: options.literalId,
            postconditionId: options.postconditionId,
            postconditionCollectionId: options.postconditionCollectionId,
            taskId: options.taskId,
            postconditionTypeId: options.postconditionTypeId,
            typeId: options.typeId,
            enterValue: options.enterValue,
            switchValue: options.switchValue,
            printDeleteButton: options.printDeleteButton,
            printAddButton: options.printAddButton,
        }

        var htmlContent = '';
        var iconLg = options.area === "global" ? "fa-lg" : "";

        var editDeleteButtons = "<a id='" + options.id + "-edit' href='" + options.actionUrl + "' class=\"mr-2\" data-toggle=\"modal\" data-target=\"" + options.target + "\" data-literalId=\"" + options.literalId + "\" data-postconditionId=\"" + options.postconditionId + "\" data-taskId=\"" + options.taskId + "\" data-postconditionTypeId=\"" + options.postconditionTypeId + "\" data-typeId=\"" + options.typeId + "\" data-switchvalue=\"" + options.switchValue + "\" data-entervalue=\"" + options.enterValue + "\" data-postconditioncollectionid=\"" + options.postconditionCollectionId + "\"><i class=\"fa fa-pencil\"></i></a>"
            + "<a  id='" + options.id + "-delete' href='" + options.actionUrl + "' data-literalId=\"" + options.literalId + "\" data-postconditionId=\"" + options.postconditionId + "\" data-taskId=\"" + options.taskId + "\" data-postconditioncollectionid=\"" + options.postconditionCollectionId + "\"><i class=\"fa fa-trash-o\"></i></a>";

        var addButton = "<a id='" + options.id + "-add' href='" + options.actionUrl + "' class=\"mr-2\" data-toggle=\"modal\" data-target=\"" + options.target + "\" data-literalId=\"" + options.literalId + "\" data-postconditionId=\"" + options.postconditionId + "\" data-taskId=\"" + options.taskId + "\" data-postconditioncollectionid=\"" + options.postconditionCollectionId + "\"><i class=\"fa fa-plus " + iconLg + "\"></i></a>";
        var deleteButton = "<a  id='" + options.id + "-delete' href='" + options.actionUrl + "' data-literalId=\"" + options.literalId + "\" data-postconditionId=\"" + options.postconditionId + "\" data-taskId=\"" + options.taskId + "\" data-postconditioncollectionid=\"" + options.postconditionCollectionId + "\"><i class=\"fa fa-trash-o " + iconLg + "\"></i></a>";

        var addDeleteButtons = "";

        if (options.printAddButton) {
            addDeleteButtons = addButton;
        }

        if (options.printDeleteButton) {
            addDeleteButtons = addDeleteButtons + deleteButton;
        }

        var globalZone = "<div class=\"row conditions-row-add\">"
            + "<div class=\"col-12 p-0\">"
            + "<div class=\"collaps-icons text-right\">"
            + addDeleteButtons
            + "</div>"
            + "</div>"
            + "</div> ";

        var sectionZone = "<div class=\"row w-100 mt-2\"><div class=\"col-12\">"
            + "<div class=\"collaps-icons-zone float-right d-flex align-items-center\" >"
            + addDeleteButtons
            + "</div></div></div>";

        var subSectionZone = "<div class=\"collaps-icons float-right d-flex align-items-center\" >"
            + editDeleteButtons
            + "</div>";

        switch (options.area) {
            case "global":
                htmlContent = globalZone;
                break;

            case "section":
                htmlContent = sectionZone;
                break;

            default:
                htmlContent = subSectionZone;
        }

        return htmlContent;
    }

    function canDeletePostconditionCollection(postconditioncollectionId, task) {
        var url = canDeletePostconditionCollectionUrl;
        var canDeletePostconditionCollectionApi = apiCall(url, 'POST', 'json', { id: postconditioncollectionId });
        canDeletePostconditionCollectionApi.done(function (res) {
            if (res) {
                $("#textModalDialog").html(textConfirmDelete);
                $("#confirmationDeleteFlowsConfirmSave").show();
                $("#confirmationDeleteFlowsConfirmSave").on("click", function () {
                    deletePostconditionCollection(postconditioncollectionId, task);
                    $("#confirmationDeleteFlows").modal("toggle");
                    $("#confirmationDeleteFlowsconfirmationDeleteFlows").off("click");
                });
            } else {
                $("#textModalDialog").html(textNoDelete);
                $("#confirmationDeleteFlowsConfirmSave").hide();
            }
        });
    }

    function deletePostconditionCollection(postconditioncollectionId, taskId) {
        var url = deletePostconditionCollectionUrl;
        var deletePostconditionCollectionApi = apiCall(url, 'DELETE', 'json', { id: postconditioncollectionId });
        deletePostconditionCollectionApi.done(function (res) {
            cleanAndReloadPrePostconditionTask(taskId);
        });
    }

    function deletePostcondition(postconditionId, taskId) {
        var url = deletePostconditionUrl;
        var deletePostconditionApi = apiCall(url, 'DELETE', 'json', { id: postconditionId });
        deletePostconditionApi.done(function (res) {
            cleanAndReloadPrePostconditionTask(taskId);
        });
    }

    function deleteAllPostcondition(postconditionCollectionId, taskId) {
        var url = deleteAllPostconditionUrl;
        var deletePostconditionApi = apiCall(url, 'DELETE', 'json', { id: postconditionCollectionId });
        deletePostconditionApi.done(function (res) {
            cleanAndReloadPrePostconditionTask(taskId);
        });
    }

    return {
        Init: init,
        ShowPostconditionsTab: showPostconditionsTab,
        CleanAndReloadPrePostconditionTask: cleanAndReloadPrePostconditionTask,
        GetPostconditionsCollectionsByTask: getPostconditionsCollectionsByTask,
    };
})();