var app = app || {};
app.pointrate = app.pointrate || {};

app.pointrate = (function (evt) {

    var initialName = '';
    var initalDescription = '';
    var urlDeletePointRate = '';

    var init = function (option) {
        urlDeletePointRate = option;
        initializeEvents();
    };

    var toggleFilter = function (e) {
        $("#filterPointRate").toggle();
    };

    var clearFilterFields = function (e) {
        $('#ActionsFilterName').val('');
        $('#ActionsFilterDescription').val('');
        $('#pager-page').val(1);
        $('#btnApplyFilter').click();
    };

    var ClickFilterButton = function (e) {
        if (initialName != $('#ActionsFilterName').val() || initalDescription != $('#ActionsFilterDescription').val()) {
            $('#pager-page').val(1);
        }
    };

    var clickConfirmationModalConfirmCancel = function () {
        $("#confirmationModal").modal("toggle");
        $("#confirmationModalConfirmSave").off("click");
    };
    
    function initializeEvents() {
        $('.button-filter').click(toggleFilter);
        $('#btnClear').click(clearFilterFields);
        $(".deleteButton").on('click', clickDeleteButton);
        $('#btnApplyFilter').click(ClickFilterButton);

        initialName = $('#ActionsFilterName').val();
        initalDescription = $('#ActionsFilterDescription').val();
        $("#confirmationModalConfirmCancel").on("click", clickConfirmationModalConfirmCancel);
    }

    function deleteElement(id, modalId, saveButtonId) {
        $(modalId).modal();
        $(saveButtonId).on("click",
            function () {
                $.ajax({
                    url: urlDeletePointRate,
                    type: 'Post',
                    dataType: 'json',
                    cache: false,
                    data: { id: id },
                    success: function () {
                        $("#btnApplyFilter").click();
                    },
                });
                $(modalId).modal();
            });
    }

    var clickDeleteButton = function (evt) {
        evt.preventDefault();
        var id = $(this).attr('id');
        $.ajax({
            url: app.config.Urls.pointRateCanDelete,
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

    return {
        Init: init
    }
})();