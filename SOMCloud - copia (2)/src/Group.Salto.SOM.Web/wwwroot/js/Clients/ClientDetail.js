var app = app || {};
app.clientDetail = app.clientDetail || {};

app.clientDetail = (function ()
{
    var bankchange = function (editMode) {
        $("#ClientBankDetail_BankCode").prop('readonly', !editMode);
        $("#ClientBankDetail_BranchNumber").prop('readonly', !editMode);
        $("#ClientBankDetail_ControlDigit").prop('readonly', !editMode);
        $("#ClientBankDetail_AccountNumber").prop('readonly', !editMode);
        $("#ClientBankDetail_SwiftCode").prop('readonly', !editMode);
        $("#ClientBankDetail_BankName").prop('readonly', !editMode);
        $("#ClientBankDetail_BankAddress").prop('readonly', !editMode);
        $("#ClientBankDetail_BankPostalCode").prop('readonly', !editMode);
        $("#ClientBankDetail_BankCity").prop('readonly', !editMode);
    };

    return {
        Bankchange: bankchange,
    };
})();