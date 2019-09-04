var app = app || {};
app.project = app.project || {};

app.project = (function () {
    var initialName = '';
    var initalSerie = '';
    var urlDeleteProject = '';

    var init = function (options) {
        urlDeleteProject = options;
        initializeEvents();
    };

    var toggleFilter = function () {
        $("#filterProject").toggle();
    };

    var clearFilterFields = function () {
        $('#projectFilterName').val('');
        $('#projectFilterSerie').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickFilterButton = function () {
        if (initialName !== $('#projectFilterName').val() || initalSerie !== $('#projectFilterSerie').val()) {
            $('#pager-page').val(1);
        }
    };

    var clickConfirmationModalConfirmCancel = function () {
        $("#confirmationModal").modal("toggle");
        $("#confirmationModalConfirmSave").off("click");
    };

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal("toggle");
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: urlDeleteProject,
                    type: 'DELETE',
                    dataType: 'json',
                    cache: false,
                    data: { id: id },
                    success: function () {
                        $("#btnApplyFilter").trigger("click");
                    },
                });
                $(modalId).modal("toggle");
            });
    }

    var clickDeleteButton = function (e) {
        e.preventDefault();
        var id = $(this).attr('id');
        $.ajax({
            url: app.config.Urls.projectCanDelete,
            type: app.constants.Get,
            dataType: 'json',
            cache: false,
            data: { id: id },
            success: function (data) {
                var errorMessageKey = data['errorMessageKey'];
                if (errorMessageKey !== '') {
                    $("#confirmationModalWithProjects").modal("toggle");
                    $('#textModalDialog').text(errorMessageKey);
                    $("#confirmationModalWithProjectsConfirmSave").on("click", function () {
                        $("#confirmationModalWithProjects").modal("toggle");
                        $("#confirmationModalWithProjectsConfirmSave").off("click");
                    });
                } else {
                    deleteElement(id, "#confirmationModal", "#confirmationModalConfirmSave");
                }
            }
        });
    };

    function initializeEvents() {
        $('.button-filter').on('click', toggleFilter);
        $('#btnClear').on('click', clearFilterFields);
        $('#btnApplyFilter').on('click', clickFilterButton);
        $(".deleteButton").on('click', clickDeleteButton);
        $("#confirmationModalConfirmCancel").on("click", clickConfirmationModalConfirmCancel);

        initialName = $('#projectFilterName').val();
        initalSerie = $('#projectFilterSerie').val();
    }

    return {
        Init: init
    };
})();