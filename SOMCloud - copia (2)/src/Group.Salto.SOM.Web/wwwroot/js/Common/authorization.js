var app = app || {};
app.common = app.common || {};
app.common.authorization = app.common.authorization || {};

app.common.authorization = (function () {

    function disable(id, hasAction) {
        if (hasAction === false) {
            $('#' + id).attr('disabled', true);
        } else {
            $('#' + id).attr('disabled', false);
        }
    }

    function visibilityDisplayInline(id, hasAction) {
        if (hasAction === false) {
            $('#' + id).css("display", "none");
        } else {
            $('#' + id).css("display", "inline");
        }
    }

    return {
        disable,
        visibilityDisplayInline
    };
})();