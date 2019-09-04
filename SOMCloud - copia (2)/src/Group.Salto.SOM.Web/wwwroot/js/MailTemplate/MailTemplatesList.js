var app = app || {};
app.MailTemplate = app.MailTemplate || {};

app.MailTemplate = (function (event) {
    var initialFilterName = '';
    var urlDeleteMailTemplate = '';

    var init = function (options) {
        urlDeleteMailTemplate = options;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterMailTemplate").toggle();
    };

    var clearFilterFields = function (e) {
        $('#FilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialFilterName !== $('#FilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    var clickDeleteButton = function (evt) {
        evt.preventDefault();
        showSpinner();

        var id = $(this).attr('id');
        var url = app.config.Urls.MailTemplateCanDelete;
        var cannotBeDeleted = apiCall(url, 'GET', 'json', { id: id });

        cannotBeDeleted.done(function (data) {
            hideSpinner();
            var errorMessageKey = data['errorMessageKey'];
            if (errorMessageKey !== '') {
                $("#ErrorModal").modal("toggle");
                $("#ErrorModal").modal()
                $('#textModalDialog').text(errorMessageKey);
                $("#ErrorModalConfirmSave").on("click", function () {
                    $("#ErrorModal").modal("toggle");
                    $("#ErrorModalConfirmSave").off("click");
                });

            } else {
                deleteElement(id, "#confirmationModalMailTemplate", "#confirmationModalMailTemplateConfirmSave");
            }
        });
    }

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal("toggle");
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: urlDeleteMailTemplate,
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

    function initializeEvents() {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $(".deleteButton").on('click', clickDeleteButton);
        $('#btnApplyFilter').on('click', (ClickFilterButton));
    }

    return {
        Init: init
    };
})();