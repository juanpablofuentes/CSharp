$(document).ready(function () {     
$('form').on('submit', function (e) {
    var tables = $('#PostalCodesContainer').find('div.table-responsive');
    var indexInputs = 0;
    tables.each(function () {
        var trList = $(this).children('table').children('tbody').children('tr');
        trList.each(function () {
            var res = $(this).children().eq(3);
            var suffix = res.attr('name').match(/\d+/);
            if (suffix != indexInputs) {
                res.attr('name', 'SelectedPostalCodes[' + indexInputs + '].PostalCodeId')
                res.attr('id', 'SelectedPostalCodes_' + indexInputs + '__PostalCodeId')
            }
            res = $(this).children().eq(4);
            suffix = res.attr('name').match(/\d+/);
            if (suffix != indexInputs) {
                res.attr('name', 'SelectedPostalCodes[' + indexInputs + '].PostalCode')
                res.attr('id', 'SelectedPostalCodes_' + indexInputs + '__PostalCode')
            }
            res = $(this).children().eq(5);
            suffix = res.attr('name').match(/\d+/);
            if (suffix != indexInputs) {
                res.attr('name', 'SelectedPostalCodes[' + indexInputs + '].ZoneProjectId')
                res.attr('id', 'SelectedPostalCodes_' + indexInputs + '__ZoneProjectId')
            }
            res = $(this).children().eq(6);
            suffix = res.attr('name').match(/\d+/);
            if (suffix != indexInputs) {
                res.attr('name', 'SelectedPostalCodes[' + indexInputs + '].State')
                res.attr('id', 'SelectedPostalCodes_' + indexInputs + '__State')
            }
            indexInputs++;
        });
    });
    });
});