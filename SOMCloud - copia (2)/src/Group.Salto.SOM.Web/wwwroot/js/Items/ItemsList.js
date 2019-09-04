var app = app || {};
app.items = app.items || {};

app.items = (function (event) {
    var initialFilterName = '';
    var urlDeleteItems = '';

    var init = function (options) {
        urlDeleteItems = options;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterItems").toggle();
    };

    var clearFilterFields = function (e) {
        $('#FilterName').val('');//filters
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
        var url = app.config.Urls.itemsCanDelete;
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
                deleteElement(id, "#confirmationModalItems", "#confirmationModalItemsConfirmSave");
            }
        });
    }

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal("toggle");
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: urlDeleteItems,
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