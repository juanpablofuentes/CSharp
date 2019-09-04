$('#PostalCodeForm').ready(function () {
    var ZonePro = $('#ActualZoneProjectId').val();
    $("input[name = 'ZoneProjectId']").val(ZonePro);
});
$('#PostalCodeConfirmSave').on('click', function () {
    var postalCodes = $('#PostalCodesContainer div.table-responsive table tboby');
    var newpostalcode = $('input#PostalCode').val();
    postalCodes.each(function () {
        if ($(this).PostalCode == newpostalcode) {
            $('label#MessageCode').show();
        }
    });
});