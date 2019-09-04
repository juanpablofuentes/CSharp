var app = app || {};
app.ContactsDetails = app.ContactsDetails || {};

app.ContactsDetails = (function () {

    var changeContactName = function () {
        $("#FullName").val($("#Name").val() + " " + $("#FirstSurname").val() + " " + $("#SecondSurname").val());
    }
    return {
        ChangeContactName: changeContactName
    };
})();