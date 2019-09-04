var app = app || {};
app.slatablelogic = app.slatablelogic || {};

app.slatablelogic = (function () {

    var init = function () {
        if ($('#checkboxTimeResponseActive').is(':checked')) {
            
            $('#checkboxMinutesResponseOtDefined').prop('disabled', false);
            $('#inputMinutesResponse').prop('disabled', false);
            $('#checkboxMinutesNaturalResponse').prop('disabled', false);
            $('#selectListReferenceMinutesResponse').prop('disabled', false);
            }
        else if ($('#checkboxTimeResponseActive').not(':checked')) {
            
            $('#checkboxMinutesResponseOtDefined').prop('disabled', true);
            $('#inputMinutesResponse').val('');
            $('#inputMinutesResponse').prop('disabled', true);
            $('#checkboxMinutesNaturalResponse').prop('disabled', true);
            $('#selectListReferenceMinutesResponse').prop('disabled', true);

        }
        if ($('#checkboxMinutesResponseOtDefined').is(':checked')) {
            $('#inputMinutesResponse').prop('disabled', true);
            $('#inputMinutesResponse').val('');
        }
        else if ($('#checkboxMinutesResponseOtDefined').not(':checked')) {
            $('#inputMinutesResponse').prop('disabled', false);
        }
        if ($('#checkboxTimeResolutionActive').is(':checked')) {
            $('#checkboxMinutesResolutionOtDefined').prop('disabled', false);
            $('#inputMinutesResolutions').prop('disabled', false);
            $('#checkboxMinutesResolutionNaturals').prop('disabled', false);
            $('#selectListReferenceMinutesResolution').prop('disabled', false);
        }
        else if ($('#checkboxTimeResolutionActive').not(':checked')) {
            $('#checkboxMinutesResolutionOtDefined').prop('disabled', true);
            $('#inputMinutesResolutions').prop('disabled', true);
            $('#checkboxMinutesResolutionNaturals').prop('disabled', true);
            $('#selectListReferenceMinutesResolution').prop('disabled', true);
        }
        if ($('#checkboxMinutesResolutionOtDefined').is(':checked')) {
            $('#inputMinutesResolutions').prop('disabled', true);
        }
        else if ($('#checkboxMinutesResolutionOtDefined').not(':checked')) {
            $('#inputMinutesResolutions').prop('disabled', false);
        }
        if ($('#checkboxTimePenaltyWithoutResponseActive').is(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResponseOtDefined').prop('disabled', false);
            $('#inputMinutesUnansweredPenalty').prop('disabled', false);
            $('#checkboxMinutesPenaltyWithoutResponseNaturals').prop('disabled', false);
            $('#selectListReferenceMinutesPenaltyUnanswered').prop('disabled', false);
        }
        else if ($('#checkboxTimePenaltyWithoutResponseActive').not(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResponseOtDefined').prop('disabled', true);
            $('#inputMinutesUnansweredPenalty').prop('disabled', true);
            $('#checkboxMinutesPenaltyWithoutResponseNaturals').prop('disabled', true);
            $('#selectListReferenceMinutesPenaltyUnanswered').prop('disabled', true);
        }
        if ($('#checkboxMinutesPenaltyWithoutResponseOtDefined').is(':checked')) {
            $('#inputMinutesUnansweredPenalty').prop('disabled', true);
        }
        else if ($('#checkboxMinutesPenaltyWithoutResponseOtDefined').not(':checked')) {
            $('#inputMinutesUnansweredPenalty').prop('disabled', false);
        }
        if ($('#checkboxTimePenaltyWhithoutResolutionActive').is(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResolutionOtDefined').prop('disabled', false);
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', false);
            $('#checkboxMinutesPenaltyWithoutNaturalResolution').prop('disabled', false);
            $('#selectListReferenceMinutesPenaltyWithoutResolution').prop('disabled', false);
        }
        else if ($('#checkboxTimePenaltyWhithoutResolutionActive').not(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResolutionOtDefined').prop('disabled', true);
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', true);
            $('#checkboxMinutesPenaltyWithoutNaturalResolution').prop('disabled', true);
            $('#selectListReferenceMinutesPenaltyWithoutResolution').prop('disabled', true);
        }
        if ($('#checkboxMinutesPenaltyWithoutResolutionOtDefined').is(':checked')) {
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', true);
        }
        else if ($('#checkboxMinutesPenaltyWithoutResolutionOtDefined').not(':checked')) {
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', false);
        }

        
    };
    $('#checkboxTimeResponseActive').change(function () {
        if ($('#checkboxTimeResponseActive').is(':checked')) {
            $('#checkboxMinutesResponseOtDefined').prop('disabled', false);
            $('#inputMinutesResponse').prop('disabled', false);
            $('#checkboxMinutesNaturalResponse').prop('disabled', false);
            $('#selectListReferenceMinutesResponse').prop('disabled', false);
            if ($('#checkboxMinutesResponseOtDefined').is(':checked')) {
                $('#inputMinutesResponse').prop('disabled', true);
            }
            else if ($('#checkboxMinutesResponseOtDefined').not(':checked')) {
                $('#inputMinutesResponse').prop('disabled', false);
            }
        }
        else if ($('#checkboxTimeResponseActive').not(':checked')) {
            $('#checkboxMinutesResponseOtDefined').prop('disabled', true);
            $('#inputMinutesResponse').prop('disabled', true);
            $('#inputMinutesResponse').val('');
            $('#checkboxMinutesNaturalResponse').prop('disabled', true);
            $('#selectListReferenceMinutesResponse').prop('disabled', true);
        }
    });
    $('#checkboxMinutesResponseOtDefined').change(function () {
        if ($('#checkboxMinutesResponseOtDefined').is(':checked')) {
            $('#inputMinutesResponse').prop('disabled', true);
            $('#inputMinutesResponse').val('');
        }
        else if ($('#checkboxMinutesResponseOtDefined').not(':checked')) {
            $('#inputMinutesResponse').prop('disabled', false);
            
        }
    });
    $('#checkboxTimeResolutionActive').change(function () {
        if ($('#checkboxTimeResolutionActive').is(':checked')) {
            $('#checkboxMinutesResolutionOtDefined').prop('disabled', false);
            $('#inputMinutesResolutions').prop('disabled', false);
            $('#checkboxMinutesResolutionNaturals').prop('disabled', false);
            $('#selectListReferenceMinutesResolution').prop('disabled', false);
            if ($('#checkboxMinutesResolutionOtDefined').is(':checked')) {
                $('#inputMinutesResolutions').prop('disabled', true);
            }
            else if ($('#checkboxMinutesResolutionOtDefined').not(':checked')) {
                $('#inputMinutesResolutions').prop('disabled', false);
            }
        }
        else if ($('#checkboxTimeResolutionActive').not(':checked')) {
            $('#checkboxMinutesResolutionOtDefined').prop('disabled', true);
            $('#inputMinutesResolutions').prop('disabled', true);
            $('#inputMinutesResolutions').val('');
            $('#checkboxMinutesResolutionNaturals').prop('disabled', true);
            $('#selectListReferenceMinutesResolution').prop('disabled', true);
        }
    });
    $('#checkboxMinutesResolutionOtDefined').change(function () {
        if ($('#checkboxMinutesResolutionOtDefined').is(':checked')) {
            $('#inputMinutesResolutions').prop('disabled', true);
            $('#inputMinutesResolutions').val('');
        }
        else if ($('#checkboxMinutesResolutionOtDefined').not(':checked')) {
            $('#inputMinutesResolutions').prop('disabled', false);

        }
    });
    $('#checkboxTimePenaltyWithoutResponseActive').change(function () {
        if ($('#checkboxTimePenaltyWithoutResponseActive').is(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResponseOtDefined').prop('disabled', false);
            $('#inputMinutesUnansweredPenalty').prop('disabled', false);
            $('#checkboxMinutesPenaltyWithoutResponseNaturals').prop('disabled', false);
            $('#selectListReferenceMinutesPenaltyUnanswered').prop('disabled', false);
            if ($('#checkboxMinutesPenaltyWithoutResponseOtDefined').is(':checked')) {
                $('#inputMinutesUnansweredPenalty').prop('disabled', true);
            }
            else if ($('#checkboxMinutesPenaltyWithoutResponseOtDefined').not(':checked')) {
                $('#inputMinutesUnansweredPenalty').prop('disabled', false);
            }
        }
        else if ($('#checkboxTimePenaltyWithoutResponseActive').not(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResponseOtDefined').prop('disabled', true);
            $('#inputMinutesUnansweredPenalty').prop('disabled', true);
            $('#inputMinutesUnansweredPenalty').val('');
            $('#checkboxMinutesPenaltyWithoutResponseNaturals').prop('disabled', true);
            $('#selectListReferenceMinutesPenaltyUnanswered').prop('disabled', true);
        }
    });
    $('#checkboxMinutesPenaltyWithoutResponseOtDefined').change(function () {
        if ($('#checkboxMinutesPenaltyWithoutResponseOtDefined').is(':checked')) {
            $('#inputMinutesUnansweredPenalty').prop('disabled', true);
            $('#inputMinutesUnansweredPenalty').val('');
        }
        else if ($('#checkboxMinutesPenaltyWithoutResponseOtDefined').not(':checked')) {
            $('#inputMinutesUnansweredPenalty').prop('disabled', false);
        }
    });
    $('#checkboxTimePenaltyWhithoutResolutionActive').change(function () {
        if ($('#checkboxTimePenaltyWhithoutResolutionActive').is(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResolutionOtDefined').prop('disabled', false);
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', false);
            $('#checkboxMinutesPenaltyWithoutNaturalResolution').prop('disabled', false);
            $('#selectListReferenceMinutesPenaltyWithoutResolution').prop('disabled', false);
            if ($('#checkboxMinutesPenaltyWithoutResolutionOtDefined').is(':checked')) {
                $('#inputMinutesPenaltyWithoutResolution').prop('disabled', true);
            }
            else if ($('#checkboxMinutesPenaltyWithoutResolutionOtDefined').not(':checked')) {
                $('#inputMinutesPenaltyWithoutResolution').prop('disabled', false);
            }
        }
        else if ($('#checkboxTimePenaltyWhithoutResolutionActive').not(':checked')) {
            $('#checkboxMinutesPenaltyWithoutResolutionOtDefined').prop('disabled', true);
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', true);
            $('#inputMinutesPenaltyWithoutResolution').val('');
            $('#checkboxMinutesPenaltyWithoutNaturalResolution').prop('disabled', true);
            $('#selectListReferenceMinutesPenaltyWithoutResolution').prop('disabled', true);
        }
    });
    $('#checkboxMinutesPenaltyWithoutResolutionOtDefined').change(function () {
        if ($('#checkboxMinutesPenaltyWithoutResolutionOtDefined').is(':checked')) {
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', true);
            $('#inputMinutesPenaltyWithoutResolution').val('');
        }
        else if ($('#checkboxMinutesPenaltyWithoutResolutionOtDefined').not(':checked')) {
            $('#inputMinutesPenaltyWithoutResolution').prop('disabled', false);
        }
    });
    return {
        Init: init
    }
    }) ();