var app = app || {};
app.finalclients = app.finalclients || {};

app.finalclients = (function (event) {
    var initialName = '';
    var initalIsActive = '';
    var urlDeleteFinalClients = '';

    var init = function (options) {
        urlDeleteFinalClients = options;
        initializeEvents();
    };

    var ClickFilterButton = function (e) {
        if (initialName !== $('#FinalClientsFilterName').val() || initalIsActive !== $('#FinalClientsFilterIsActive').val()) {
            $('#pager-page').val(1);
        }
    };

    var toggleFilter = function (e) {
        $("#filterFinalClients").toggle();
    };

    var clearFilterFields = function (e) {
        $('#FinalClientsFilterName').val('');
        $('#FinalClientsFilterIsActive').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var clickDeleteButton = function (evt) {
        evt.preventDefault();
        showSpinner();

        var id = $(this).attr('id');
        var url = app.config.Urls.finalClientsCanDelete;
        var cannotBeDeleted = apiCall(url, 'GET', 'json', { id: id });

        cannotBeDeleted.done(function (data)
        {
            hideSpinner();
                var errorMessageKey = data['errorMessageKey'];
                if (errorMessageKey !== '')
                {
                    $("#ErrorModal").modal("toggle");
                    $("#ErrorModal").modal()
                    $('#textModalDialog').text(errorMessageKey);
                    $("#ErrorModalConfirmSave").on("click", function ()
                    {
                        $("#ErrorModal").modal("toggle");
                        $("#ErrorModalConfirmSave").off("click");
                    });                

            } else {
                deleteElement(id, "#confirmationModalFinalClients", "#confirmationModalFinalClientsConfirmSave");
            }
        });
    }

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal("toggle");
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: urlDeleteFinalClients,
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
        $('.button-filter').on('click', toggleFilter);
        $('#btnClear').on('click', clearFilterFields);
        $(".deleteButton").on('click', clickDeleteButton);
        $('#btnApplyFilter').on('click', ClickFilterButton);
    }

    return {
        Init: init
    };
})();