var app = app || {};
app.TechnicianDetails = app.TechnicianDetails || {};

app.TechnicianDetails = (function () {

    var changePeople = function () {
        $("#TechniciansName").val($("#PeopleId").find(":selected").text());
    };

    return {
        ChangePeople: changePeople
    };
})();