var app = app || {};
app.common = app.common || {};
app.common.ui = app.common.ui || {};

app.common.ui = (function () {
    var loadSelector = function loadSelector(selectorId, items) {
        var select = $('#' + selectorId);
        select.empty();
        if (items.length > 0) {
            for (var i = 0; i < items.length; i++) {
                var option = "<option value='" + items[i].key + "'>" + items[i].value + "</option>";
                select.append($(option));
            }
        }
        return select;
    };

    var loadSelectorKeyValue = function loadSelector(selectorId, items, key, value, withVoidLine, voidId) {
        var select = $('#' + selectorId);
        select.empty();
        if (items.length > 0) {
            if (withVoidLine) {
                if (voidId === undefined || voidId === null)
                    voidId = 0;
                var defaultOption = "<option value='" + voidId + "'>_</option>";
                select.append($(defaultOption));
            }
            for (var i = 0; i < items.length; i++) {
                var option = "<option value='" + items[i][key] + "'>" + items[i][value] + "</option>";
                select.append($(option));
            }
        }
        return select;
    };

    var loadSelectorWithVoidLine = function (selectorId, items, defaultOptionText) {
        var select = $('#' + selectorId);
        select.empty();
        if (items !== undefined && items !== null) {
            if (items.length > 1) {
                var defaultOption = "<option value='" + 0 + "'>" + defaultOptionText + "</option>";
                select.append($(defaultOption));
            }
            for (var i = 0; i < items.length; i++) {
                var option = "<option value='" + items[i].key + "'>" + items[i].value + "</option>";
                select.append($(option));
            }
        }
        return select;
    };

    var getCookie = function getCookie(cname) {
        var name = cname + "=";
        var decodedCookie = decodeURIComponent(document.cookie);
        var ca = decodedCookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) === 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    };

    var changeCheckbox = function (that) {
        $(that).prop("checked") ? $(that).val("true") : $(that).val("false");
    };

    var getDataById = function (data, url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            async: false,
            data: { id: data },
            success: success,
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.options.closeButton = 'False';
                toastr.options.newestOnTop = 'False';
                var optionsOverride = {};
                toastr['error']("Error", thrownError.message, optionsOverride);
            }
        });
    };

    var getDataByIdWaitResponse = function (data, url) {
        var res = null;
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            async: false,
            data: { id: data },
            success: function (result) {
                res = result;
            },
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.options.closeButton = 'False';
                toastr.options.newestOnTop = 'False';
                var optionsOverride = {};
                toastr['error']("Error", thrownError.message, optionsOverride);
            }
        });
        return res;
    };

    return {
        LoadSelector: loadSelector,
        LoadSelectorKeyValue: loadSelectorKeyValue,
        LoadSelectorWithVoidLine: loadSelectorWithVoidLine,
        ChangeCheckbox: changeCheckbox,
        GetDataById: getDataById,
        GetDataByIdWaitResponse: getDataByIdWaitResponse
    };
})();