var app = app || {};
app.taskmodal = app.taskmodal || {};

app.taskmodal = (function () {

    var textTranslations;
    var descriptionTranslations;
    var GlobalPermissionsKeyValues;
    var taskListId;

    var init = function (permissionsList) {
        GlobalPermissionsKeyValues = permissionsList;
        initializeEvents();        
    };

    var loadNewTaskModal = function (evt) {
        getAllPermissions();
        taskListId = 0;
        $.ajax({
            url: app.config.Urls.getEmptyTask,
            type: app.constants.Get,
            dataType: 'json',
            cache: false,
            data: { id: 0 },
            success: function (data) {
                $('#modalTask').modal('show');
                $('#taskName').val(data.name);
                $('#taskDescription').val(data.description);

                if (data.textTranslations) {
                    textTranslations = data.textTranslations;
                }

                if (data.descriptionTranslations) {
                    descriptionTranslations = data.descriptionTranslations;
                }

                fillModalLanguages();
                var emptySelectedPermissions = [];
                setPermissionsCombo(emptySelectedPermissions);
            }
        });
    };

    function getAllPermissions() {

        $.ajax({
            url: app.config.Urls.getAllPermissions,
            type: app.constants.Get,
            dataType: 'json',
            cache: false,
            success: function (data) {
                GlobalPermissionsKeyValues = data;
            }
        });

    }

    var loadTaskModal = function (evt) {
        evt.preventDefault();
        taskListId = CurrentTaskId;
        getAllPermissions();
        $.ajax({
            url: app.config.Urls.getTask,
            type: app.constants.Get,
            dataType: 'json',
            cache: false,
            data: { id: taskListId },
            success: function (data) {
                $('#modalTask').modal('show');
                $('#taskName').val(data.name);
                $('#taskDescription').val(data.description);

                if (data.textTranslations) {
                    textTranslations = data.textTranslations;
                }

                if (data.descriptionTranslations) {
                    descriptionTranslations = data.descriptionTranslations;
                }

                fillModalLanguages();
                setPermissionsCombo(data.permissionsTasksSelected);
            }
        });
    };

    function setPermissionsCombo(selectedPermissions) {
        $("#Container_MultiSelectModalTask").html('');
        if (GlobalPermissionsKeyValues.length > 0) {
            for (var i = 0; i < GlobalPermissionsKeyValues.length; i++) {
                var item = GlobalPermissionsKeyValues[i];
                var checked = "";
                if (selectedPermissions.length > 0) { 
                    if ($.inArray(item.id, selectedPermissions) > -1) {
                        checked = 'checked="checked"';
                    }
                }

                var html = '<div class="custom-control custom-checkbox">'
                    + '<input id="' + item.id + '"' + checked + '" type="checkbox" class="custom-control-input" />'
                    + '<label for= ' + item.id + ' class="custom-control-label">' + item.name + '</label>'
                    + '<input id="' + item.id + '-hidden" value="' + item.name + '" type="hidden" />'
                    + '</div>';
                $("#Container_MultiSelectModalTask").append(html);
            }
        }
    }

    function fillModalLanguages() {

        var options = {
            textTranslationsOptions: {
                selectedItems: textTranslations,

                column1Text: $('#hiddenTableHeaderLanguages').val(),
                column2Text: $('#hiddenTableHeaderDescription').val(),
                column3Text: '',
                inputPlaceHolder: $('#hiddenTableHeaderDescription').val()
            },
            descriptionsTranslationsOptions: {
                selectedItems: descriptionTranslations,

                column1Text: $('#hiddenTableHeaderLanguages').val(),
                column2Text: $('#hiddenTableHeaderDescription').val(),
                column3Text: '',
                inputPlaceHolder: $('#hiddenTableHeaderDescription').val()
            }
        };
        app.TaskTranslations.Init(options); 
        var permissionsName = 'MultiSelectModalTask';
        app.multiselect.Init(permissionsName);
    }

    var updateTask = function () {
        var editedTask = {};

        //var taskListId = $('#hiddenTaskItemId').val();
        var name = $('#taskName').val();
        var description = $('#taskDescription').val();
        var translations = [];
        var permissions = [];

        $('#Container_MultiSelectModalTask').find('input').each(function () {
            if ($(this).is(":checked")) {
                var permissionId = $(this).attr('id');
                permissions.push(permissionId);
            }
        });

        var rowIndex = 0;
        $('#nameTranslationsContainer .table > tbody >tr').each(function () {
            var translation = new Object();
            var variableLangNameId = '#TextTranslations_' + rowIndex + '__Value';
            var variableNameText = '#TextTranslations_' + rowIndex + '__TextSecondary';
            var langId = $(variableLangNameId).val();
            translation.LanguageId = langId;
            translation.NameText = $(variableNameText).val();
            translations.push(translation);
            rowIndex += 1;

        });

        rowIndex = 0;
        $('#descriptionTranslationsContainer .table > tbody >tr').each(function () {
            var variableLangNameId = '#DescriptionTranslations_' + rowIndex + '__Value';
            var variableDescriptionText = '#DescriptionTranslations_' + rowIndex + '__TextSecondary';
            var langId = $(variableLangNameId).val();
            var existingLangValue = translations.find(x => x.LanguageId.toString() === langId.toString());
            if (existingLangValue !== undefined) {
                existingLangValue.DescriptionText = $(variableDescriptionText).val();
            }
            else {
                var translation = new Object();
                translation.LanguageId = langId;
                translation.DescriptionText = $(variableDescriptionText).val();
                translations.push(translation);
            }

            rowIndex += 1;
        });

        editedTask = {
            "Id": taskListId,
            "FlowId": $('#FlowId').val(),
            "Name": name,
            "Description": description,
            "PermissionsTasksSelected": permissions,
            "TasksTranslationsList": translations
        };

        var strTask = JSON.stringify(editedTask);

        if (taskListId === 0) {
            $.ajax({
                url: app.config.Urls.createTask,
                type: app.constants.Post,
                dataType: 'json',
                cache: false,
                data: editedTask,
                success: function (data) {
                    var done_str = 'done'.data.name;
                }
            });
        }
        else {
            $.ajax({
                url: app.config.Urls.updateTask,
                type: app.constants.Post,
                dataType: 'json',
                cache: false,
                data: editedTask,
                success: function (data) {
                    var done_str = 'done'.data.name;
                }
            });
        }
    };

    function initializeEvents() {
        $('.task-Edit').on('click', loadTaskModal);
        $('#btnSaveTaskEdit').on('click', updateTask);
    }

    return {
        Init: init,
        UpdateTask: updateTask,
        CreateNewTask: loadNewTaskModal
    };
})();