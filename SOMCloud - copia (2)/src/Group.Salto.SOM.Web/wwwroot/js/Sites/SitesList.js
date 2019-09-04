var app = app || {};
app.sites = app.sites || {};

app.sites = (function (event) {
    var initialSerialNumber = '';
    var urlDeleteSites = '';

    var init = function (options) {
        urlDeleteSites = options;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterSites").toggle();
    };

    var clearFilterFields = function (e) {
        $('#sitesFilterName').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialSerialNumber !== $('#sitesFilterName').val()) {
            $('#pager-page').val(1);
        }
    };

    var clickDeleteButton = function (evt) {
        evt.preventDefault();
        showSpinner();

        var id = $(this).attr('id');
        var url = app.config.Urls.sitesCanDelete;
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
                deleteElement(id, "#confirmationModalSites", "#confirmationModalSitesConfirmSave");
            }
        });
    }

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal("toggle");
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: urlDeleteSites,
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

        initialSerialNumber = $('#sitesFilterName').val();
    }
    return {
        Init: init
    };
})();