var app = app || {};
app.trigger = app.trigger || {};

app.trigger = (function (event) {
    var CurrentTaskId;
    var getTriggerTypeByIdUrl = '';
    var init = function (options) {
        getTriggerTypeByIdUrl = options.getTriggerTypeByIdUrl;
        initializeEvents();
    };

    function initializeEvents() {
        $('[id*="-edit-trigger"]').on('click', loadTriggerModal);
    }

    function triggerTypeLoad(evt) {
        if (evt)
        {
            evt.preventDefault();
        }
        CurrentTaskId = $(this).data('taskid');
        loadTrigger(CurrentTaskId);
    }

    var loadTrigger = function (currentTaskId) {
        var callApi = apiCall(getTriggerTypeByIdUrl, 'GET', 'json', { id: currentTaskId });

        callApi.done(function (data) {
            $('#h-typeId-' + CurrentTaskId + '-trigger').val(data.typeId);
            $('#h-value-' + CurrentTaskId + '-trigger').val(data.valueId);
            $('#typeName-' + CurrentTaskId + '-trigger').html(data.typeName);
            $('#value-' + CurrentTaskId + '-trigger').html(data.value);
        });
    }

    var loadTriggerModal = function (evt) {
        if (evt) {
            evt.preventDefault();
        }
        $('#modalTrigger').modal('show');
        app.triggerModal.OpenTriggerModal();
    }

    return {
        Init: init,
        TriggerTypeLoad: triggerTypeLoad,
        LoadTrigger: loadTrigger,
    };
})();