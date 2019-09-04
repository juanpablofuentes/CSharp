$(document).ready(function () {
$('#ProjectId').on("change", function () {
    var textText = $('#ProjectId option:selected').text();
    var textValue = $('#ProjectId option:selected').val();
    $('#ProjectName').val(textText);
    $('#FinalId').val(textValue);
    document.getElementById('ProjectName').value = textText;
});
$('[name="ProjectForm"]').ready(function () {
    $.refreshModal = function () {
        $('#ProjectId option').each(function () {
            $(this).show();
        });
        $('#PrincipalZoneForm').find('tbody tr').each(function (i, it) {
            if ($(this).attr('style') == 'display:none;') {
                return true;
            }
            $('#ProjectId option').each(function () {               
                if (it.children[4].defaultValue == $(this).val()) {
                    $(this).css("display", "none");
                }
            });
        });
    }
    $.refreshModal();
    });
});