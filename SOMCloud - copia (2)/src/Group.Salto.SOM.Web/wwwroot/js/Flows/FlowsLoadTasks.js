var app = app || {};
app.tasks = app.tasks || {};

app.tasks = (function (event) {
    var getTasksUrl = '', getAllPermissionsUrl = '', getAllPreconditionsByTaskUrl = '', getPostconditionByTaskUrl = '', GlobalPermissionsKeyValues, txtReadMore, txtReadLess, textYes, textNo, textGreaterThan, textLessThan, deleteLiteralPreconditionUrl = '', deletePreconditionUrl = '', deleteAllPreconditionUrl = '';
    const BillableTypeConstant = app.literalPreconditionTypes.constants.Billable;
    const ActuationDateTypeConstant = app.literalPreconditionTypes.constants.ActuationDate;
    const AssignmentDateTypeConstant = app.literalPreconditionTypes.constants.AssignmentDate;
    const ClientClosureDateTypeConstant = app.literalPreconditionTypes.constants.ClientClosureDate;
    const CollectionDateTypeConstant = app.literalPreconditionTypes.constants.CollectionDate;
    const CreateDateTypeConstant = app.literalPreconditionTypes.constants.CreateDate;
    const SaltoClosureDateTypeConstant = app.literalPreconditionTypes.constants.SaltoClosureDate;
    const WOClientClosureDateTypeConstant = app.literalPreconditionTypes.constants.WOClientClosureDate;
    const GreaterThanConstant = app.literalPreconditionTypes.constants.GreaterThan;
        
    var init = function (options) {
        txtReadMore = options.txtReadMore;
        txtReadLess = options.txtReadLess;
        getTasksUrl = options.getTasksUrl;
        getAllPermissionsUrl = options.getAllPermissionsUrl;
        getAllPreconditionsByTaskUrl = options.getAllPreconditionsByTaskUrl;
        getPostconditionByTaskUrl = options.getPostconditionByTaskUrl;
        textYes = options.textYes;
        textNo = options.textNo;
        textGreaterThan = options.textGreaterThan;
        textLessThan = options.textLessThan;
        textSystemDate = options.textSystemDate;
        textMinutes = options.textMinutes;
        deleteLiteralPreconditionUrl = options.deleteLiteralPreconditionUrl;
        deletePreconditionUrl = options.deletePreconditionUrl;
        deleteAllPreconditionUrl = options.deleteAllPreconditionUrl;
        initializeEvents();
    };

    function initializeEvents() {
        loadGlobalPermissions();
    }

    function loadGlobalPermissions() {
        var url = getAllPermissionsUrl;
        var getPermissions = apiCall(url, 'GET', 'json');

        getPermissions.done(function (data) {
            GlobalPermissionsKeyValues = data;
        });
    }

    function loadPermissions(id, task) {
        var permissionsNames = [];

        for (var i = 0; i < task.permissionsTasksSelected.length; i++) {
            var elem = task.permissionsTasksSelected[i];
            var permission = GlobalPermissionsKeyValues.find(x => x.id === elem);
            permissionsNames.push('<li>' + permission.name + '</li>');
        }
        $('#permissions-' + id).empty();
        $('#permissions-' + id).append(permissionsNames);
    }

    function showTranslations(id, name, elem) {
        if (elem.languageId === app.common.ui.constants.SpanishId) {
            if (elem.nameText == undefined || elem.nameText === '') {
                $('#name-' + id + '-spa').html('');
                $('#name-' + id + '-spa').html(name);
            }
            else {
                $('#name-' + id + '-spa').html('');
                $('#name-' + id + '-spa').html(elem.nameText);
            }
            $('#name-' + id + '-spa-descr').html('');
            $('#name-' + id + '-spa-descr').append(elem.descriptionText);
        }
        else if (elem.languageId === app.common.ui.constants.CatalanId) {
            $('#name-' + id + '-cat').html('');
            $('#name-' + id + '-cat').html(elem.nameText);
            $('#name-' + id + '-cat-descr').html('');
            $('#name-' + id + '-cat-descr').append(elem.descriptionText);
        }
        else if (elem.languageId === app.common.ui.constants.EnglishId) {
            $('#name-' + id + '-eng').html('');
            $('#name-' + id + '-eng').html(elem.nameText);
            $('#name-' + id + '-eng-descr').html('');
            $('#name-' + id + '-eng-descr').append(elem.descriptionText);
        }
    }

    function dropDownElemt(evt) {
        evt.preventDefault();
        var id = $(this).data('target');
        if ($('#container-preconditions-' + id).data('hasdata') === false) {
            CurrentTaskId = id;
            app.taskmodal.Init(CurrentTaskId, GlobalPermissionsKeyValues);
            var url = getTasksUrl;
            var callApi = apiCall(url, 'GET', 'json', { id: id });

            callApi.done(function (data) {
                var task = data;
                $('#hidden_task_triggerType').val(task.triggerTypesId);

                data.tasksTranslationsList.forEach(function (elem) {
                    showTranslations(CurrentTaskId, data.name, elem);
                });
                loadPermissions(CurrentTaskId, task);
                getPreconditionsByTask(CurrentTaskId);
            });
        }
        $('#' + id).collapse('toggle');
        readMore('.read-more');
    }

    function getPreconditionsByTask(id) {
        showSpinner();
        var url = getAllPreconditionsByTaskUrl;
        var getPreconditions = apiCall(url, 'GET', 'json', { id: id });
        getPreconditions.done(function (res) {
            var cont = $('#container-preconditions-' + CurrentTaskId);
            var zone = '-p-'
            loadPreconditions(res.data, cont, zone);
            hideSpinner();
        }).fail(function (err) {
            hideSpinner();
        });
    }

    function loadPreconditions(data, container, zone, postconditionCollectionId) {
        var CurrentPostconditionCollectionId = postconditionCollectionId;
        var PreconditionsArray = data;
        var preconditionsContainer = container;
        var literalsContainerId = '#container-literals';

        var literalsEditId = '#t-' + CurrentTaskId + '-Pre-Edit';

        var preconditionTemplate = container.find('#t-' + CurrentTaskId + '-Pre');

        if (preconditionTemplate.hasClass('d-none')) {
            preconditionTemplate.removeClass('d-none');
        }

        var literalPreconditionId = '#t-' + CurrentTaskId + '-Pre-Lit';
        var literalValuesId = '#t-' + CurrentTaskId + '-Pre-Values';

        if (PreconditionsArray != null) {
            for (var i = 0; i < PreconditionsArray.length; i++) {
                var preconditionItem = PreconditionsArray[i];
                CurrentPostconditionCollectionId = preconditionItem.postconditionCollectionId;

                var newPrecondition = preconditionTemplate.clone();
                newPrecondition.attr('id', 't-' + CurrentTaskId + zone + preconditionItem.id);

                var literalsContainer = newPrecondition.find(literalsContainerId);
                literalsContainer.attr('id', 't-' + CurrentTaskId + '-c-' + preconditionItem.id);

                for (var z = 0; z < preconditionItem.literalsPreconditions.length; z++) {
                    var literal = preconditionItem.literalsPreconditions[z];
                    var literalsEdit = literalsContainer.find(literalsEditId).clone();
                    literalsEdit.attr('id', 't-' + CurrentTaskId + '-c-' + preconditionItem.id + '-edit');

                    var containerLiteralPreconditions = literalsEdit.find(literalPreconditionId);
                    containerLiteralPreconditions.attr('id', 't-' + CurrentTaskId + zone + preconditionItem.id + '-l-' + literal.id);
                    containerLiteralPreconditions.append('<strong>' + literal.preconditionsTypeName + '</strong> <br>');

                    var containerLiteralValuesPreconditions = literalsEdit.find(literalValuesId);
                    containerLiteralValuesPreconditions.attr('id', 't-' + CurrentTaskId + zone + preconditionItem.id + '-v-' + literal.id);

                    for (var y = 0; y < literal.preconditionsLiteralValues.length; y++) {
                        var literalValue = literal.preconditionsLiteralValues[y];
                        var textToAppend = textForTypeName(literal, literalValue);
                        containerLiteralValuesPreconditions.append(textToAppend + '<br>');

                        if (literal.preconditionsLiteralValues.length > 6) {
                            containerLiteralValuesPreconditions.addClass('read-more');
                        }
                    }
                    var buttonsEdits = addButtonsForEdit('subSections', "#", "preconLiteral-" + literal.id, '#literalModal', literal.id, preconditionItem.id, CurrentTaskId, CurrentPostconditionCollectionId, true);

                    literalsContainer.append(literalsEdit);
                    literalsContainer.append(buttonsEdits);
                }

                newPrecondition.append(addButtonsForEdit('section', '#', "preconLiteral-" + preconditionItem.id, '#literalModal', 0, preconditionItem.id, CurrentTaskId, CurrentPostconditionCollectionId, true));

                literalsContainer.find(literalsEditId).remove();
                preconditionsContainer.prepend(newPrecondition);
            }
        }

        preconditionTemplate.addClass('d-none');
        container.data('hasdata', true);
        if (container.find('.conditions-row-add')) {
            var deleteB = false;
            if (PreconditionsArray != null) {
                if (PreconditionsArray.length > 0) {
                    deleteB = true;
                }
            }
            var btnEdit = addButtonsForEdit('global', '#', CurrentTaskId, '#literalModal', 0, 0, CurrentTaskId, CurrentPostconditionCollectionId, deleteB);
            $(btnEdit).appendTo(container);
        }

        readMore('.read-more');

        container.find('a[id*="-edit"]').on('click', function () {
            showSpinner();
            var literal = $(this).data('literalid');
            var precondition = $(this).data('preconditionid');
            var task = $(this).data('taskid');
            app.modalControl.OpenLiteralModal('edit', literal, precondition, task);
            hideSpinner();
        });

        container.find('a[id*="-delete"]').on('click', function () {
            showSpinner();
            var literal = $(this).data('literalid');
            var precondition = $(this).data('preconditionid');
            var task = $(this).data('taskid');

            $("#confirmationDeleteFlows").modal("toggle");
            $("#confirmationDeleteFlowsConfirmSave").on("click", function () {
                if (precondition == 0) {
                    deleteAllPreconditions(task);
                }      
                else if(literal == 0) {
                    deletePrecondition(precondition, task);
                } else {
                    deleteLiteralPrecondition(literal, task);
                }
                literal = null;
                precondition = null;
                task = null;
                $("#confirmationDeleteFlows").modal("toggle");
                $("#confirmationDeleteFlowsconfirmationDeleteFlows").off("click");
            });
            hideSpinner();
        });

        container.find('a[id*="-add"]').on('click', function () {
            showSpinner();
            var literal = $(this).data('literalid');
            var postconditioncollection = $(this).data('postconditioncollectionid');
            var precondition = $(this).data('preconditionid');
            var task = $(this).data('taskid');
            app.modalControl.OpenLiteralModal('add', literal, precondition, task, postconditioncollection);
            hideSpinner();
        });
    }

    function textForTypeName(literal, literalValue) {
        var textToAppend = '';
        if (literal.preconditionsTypeName == BillableTypeConstant)
        {
            if (literalValue.booleanValue) {
                textToAppend = textYes;
            } else {
                textToAppend = textNo;
            }
        }else
        if (literal.preconditionsTypeName == ActuationDateTypeConstant ||
            literal.preconditionsTypeName == AssignmentDateTypeConstant ||
            literal.preconditionsTypeName == ClientClosureDateTypeConstant ||
            literal.preconditionsTypeName == CollectionDateTypeConstant ||
            literal.preconditionsTypeName == CreateDateTypeConstant ||
            literal.preconditionsTypeName == SaltoClosureDateTypeConstant ||
            literal.preconditionsTypeName == WOClientClosureDateTypeConstant)
        {
            if (literal.comparisonOperator == GreaterThanConstant) {
                textToAppend = textGreaterThan;
            } else {
                textToAppend = textLessThan;
            }
            textToAppend = textToAppend +" "+ textSystemDate+" "+ literalValue.enterValue + textMinutes;
        } else
        {
            textToAppend = literalValue.typeName;
        }
        return textToAppend;
    }
    
    function addButtonsForEdit(area, actionUrl, id, target, literalId, preconditionId, taskId, postconditionCollectionId, printDeleteButton) {
        var htmlContent = '';
        var iconLg = area === "global" ? "fa-lg" : "";

        var editDeleteButtons = "<a id='" + id + "-edit' href='" + actionUrl + "' class=\"mr-2\" data-toggle=\"modal\" data-target=\"" + target + "\" data-literalId=\"" + literalId + "\" data-preconditionId=\"" + preconditionId + "\" data-taskId=\"" + taskId + "\"><i class=\"fa fa-pencil\"></i></a>"
            + "<a  id='" + id + "-delete' href='" + actionUrl + "' data-literalId=\"" + literalId + "\" data-preconditionId=\"" + preconditionId + "\" data-taskId=\"" + taskId + "\"><i class=\"fa fa-trash-o\"></i></a>";

        var addButton = "<a id='" + id + "-add' href='" + actionUrl + "' class=\"mr-2\" data-toggle=\"modal\" data-target=\"" + target + "\" data-literalId=\"" + literalId + "\" data-preconditionId=\"" + preconditionId + "\" data-taskId=\"" + taskId + "\" data-postconditioncollectionid=\"" + postconditionCollectionId + "\"><i class=\"fa fa-plus " + iconLg + "\"></i></a>";
        var deleteButton = "<a  id='" + id + "-delete' href='" + actionUrl + "' data-literalId=\"" + literalId + "\" data-preconditionId=\"" + preconditionId + "\" data-taskId=\"" + taskId + "\"><i class=\"fa fa-trash-o " + iconLg + "\"></i></a>";

        var addDeleteButtons;

        if (printDeleteButton) {
            addDeleteButtons = addButton + deleteButton;
        } else {
            addDeleteButtons = addButton;
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

        switch (area) {
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

    function readMore(selector) {
        $(selector).readmore({
            embedCSS: false,
            moreLink: '<div class="row"><div class="col text-center"><a href="#"><i class="fa fa-chevron-down"></i> ' + txtReadMore + '</a></div></div>',
            lessLink: '<div class="row"><div class="col text-center"><a href="#"><i class="fa fa-chevron-up"></i> ' + txtReadLess + '</a></div></div>',
            collapsedHeight: 100,
            speed: 500,
            afterToggle: function (trigger, element, expanded) {
                if (!expanded) {
                    $('html, body').animate({
                        scrollTop: $('#' + element[0].id).offset().top
                    }, 1000, "linear");
                }
            }
        });
    }

    function deleteLiteralPrecondition(literalId, globalTaskId) {
        var url = deleteLiteralPreconditionUrl;
        var deleteLiteralPreconditionsApi = apiCall(url, 'DELETE', 'json', { id: literalId });
        deleteLiteralPreconditionsApi.done(function (res) {
            checkCleanAndReloadPage(globalTaskId);
        });
    }

    function deletePrecondition(preconditionId, globalTaskId) {
        var url = deletePreconditionUrl;
        var deletePreconditionsApi = apiCall(url, 'DELETE', 'json', { id: preconditionId });
        deletePreconditionsApi.done(function (res) {
            checkCleanAndReloadPage(globalTaskId);
        });
    }

    function deleteAllPreconditions(taskId) {
        var url = deleteAllPreconditionUrl;
        var deleteAllPreconditionsApi = apiCall(url, 'DELETE', 'json', { id: taskId });
        deleteAllPreconditionsApi.done(function (res) {
            checkCleanAndReloadPage(taskId);
        });
    }

    function checkCleanAndReloadPage(taskId) {
        showSpinner();
        if ($('#nav-preconditions-tab').hasClass('active')) {
            cleanAndReloadPreconditionTask(taskId);
        }
        else if ($('#nav-postconditions-tab').hasClass('active')) {
            app.postconditions.CleanAndReloadPrePostconditionTask(taskId);
        }
        hideSpinner();
    }

    function cleanAndReloadPreconditionTask(taskId) {
        var preconditionContainer = $('#container-preconditions-' + taskId).children();
        preconditionContainer.each(function () {
            if (!$(this).hasClass('d-none')) {
                $(this).remove();
            }
        });
        getPreconditionsByTask(taskId);
    }

    return {
        Init: init,
        DropDownElemt: dropDownElemt,
        LoadGlobalPermissions: loadGlobalPermissions,
        GetPreconditionsByTask: getPreconditionsByTask,
        CleanAndReloadPreconditionTask: cleanAndReloadPreconditionTask,
        LoadPreconditions: loadPreconditions,
        CheckCleanAndReloadPage: checkCleanAndReloadPage,
        AddButtonsForEdit: addButtonsForEdit,
    };
})();