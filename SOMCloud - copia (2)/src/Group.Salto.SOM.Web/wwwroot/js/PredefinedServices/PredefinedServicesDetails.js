var app = app || {};
app.PredefinedServicesDetails = app.PredefinedServicesDetails || {};

app.PredefinedServicesDetails = (function () {

    var changeCollectionExtraFields = function () {
        $("#CollectionExtraFieldName").val($("#CollectionExtraFieldId").find(":selected").text());
    };

    return {
        ChangeCollectionExtraFields: changeCollectionExtraFields,
    };
})();