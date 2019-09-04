var app = app || {};
app.siteUser = app.siteUser || {};

app.siteUser = (function (event) {
    var initialSerialNumber = '';
    var urlDelete = '';

    var init = function (options) {
        urlDelete = options;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterSiteUser").toggle();
    };

    var clearFilterFields = function (e) {
        $('#siteUserFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialSerialNumber !== $('#siteUserFilterName').val()) {
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
                    url: urlDelete,
                    type: 'DELETE',
                    dataType: 'json',
                    cache: false,
                    data: { id: id },
                    success: function () {
                        $("#btnApplyFilter").trigger("click");
                    }
                });
                $(modalId).modal("toggle");
            });
    }

    var clickDeleteButton = function (e) {
        e.preventDefault();
        var id = $(this).attr('id');
        $.ajax({
            url: app.config.Urls.siteUserCanDelete,
            type: app.constants.Get,
            dataType: 'json',
            cache: false,
            data: { id: id },
            success: function (data) {
                hideSpinner();
                var errorMessageKey = data['errorMessageKey'];
                if (errorMessageKey !== '') {
                    $("#confirmationModalCannotDelete").modal("toggle");
                    $('#textModalDialog').text(errorMessageKey);
                    $("#confirmationModalCannotDelete").on("click", function () {
                        $("#confirmationModalCannotDelete").modal("toggle");
                        $("#confirmationModalCannotDelete").off("click");
                    });
                } else {
                    deleteElement(id, "#confirmationModal", "#confirmationModalConfirmSave");
                }
            }
        });
    };

    function initializeEvents() {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $('#btnApplyFilter').on('click', (ClickFilterButton));
        $(".deleteButton").on('click', clickDeleteButton);
        $("#confirmationModalConfirmCancel").on("click", clickConfirmationModalConfirmCancel);


        initialSerialNumber = $('#siteUserFilterName').val();
    }
    return {
        Init: init
    };
})();