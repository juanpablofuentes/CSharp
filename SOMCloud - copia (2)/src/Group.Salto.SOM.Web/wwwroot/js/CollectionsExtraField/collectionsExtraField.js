var app = app || {};
app.collectionsExtraField = app.collectionsExtraField || {};

app.collectionsExtraField = (function (event) {
    var initialName = '';
    var urlDeleteCollectionExtraFields = '';

    var init = function (options) {
        urlDeleteCollectionExtraFields = options;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterCollectionsExtraField").toggle();
    };

    var clearFilterFields = function (e) {
        $('#collectionsExtraFieldFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#collectionsExtraFieldFilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    var clickDeleteButton = function (evt) {
        evt.preventDefault();
        showSpinner();

        var id = $(this).attr('id');
        var url = app.config.Urls.collectionsExtraFieldCanDelete;
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
                deleteElement(id, "#confirmationModalCollectionExtraFields", "#confirmationModalCollectionExtraFieldsConfirmSave");
            }
        });

        function deleteElement(id, modalId, saveButtonId) {
            $(modalId).modal("toggle");
            $(saveButtonId).on("click",
                function () {
                    $.ajax({
                        url: urlDeleteCollectionExtraFields,
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
    };

    function initializeEvents() {
        $('.button-filter').on('click', (toggleFilter));
        $('#btnClear').on('click', (clearFilterFields));
        $('#btnApplyFilter').on('click', (ClickFilterButton));
        $(".deleteButton").on('click', clickDeleteButton);
        initialName = $('#collectionsExtraFieldFilterName').val();
    }

    return {
        Init: init
    };
})();