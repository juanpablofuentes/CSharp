var app = app || {};
app.common = app.common || {};
app.common.validations = app.common.validations || {};

app.common.validations = (function () {

    var hasDirtyData = false;
    var isFunction = function isFunctionMethod(functionToCheck) {
        return functionToCheck && {}.toString.call(functionToCheck) === '[object Function]';
    };

    $(".dirtyData").on('change', function () {
        hasDirtyData = true;
    });

    var getHasDirtyData = function gethasDirtyDataMethod() {
        return hasDirtyData;
    };

    var changeDirtyData = function changeDirtyDataMethod() {
        hasDirtyData = true;
    };

    var isEmpty = function isStringEmpty(str) {
        return (!str || 0 === str.length);
    };

    var stringContains = function stringContainsMethod(text, search) {
        var result = true;
        if (!app.common.validations.IsEmpty(search)) {
            result = text.toUpperCase().includes(search.toUpperCase());
        }
        return result;
    };

    function checkFloatNumber(item, msgformat) {
        var regExp = /^[+-]?\d*(\.|\,)?\d+([eE][+-]?\d+)?$/;
        return checkRegExpError(regExp, item, msgformat);
    }

    function checkRegExpError(regExp, item, msgformat) {
        if ((item.val() !== "" && regExp != undefined)) {
            if (!regExp.test(item.val())) {
                alert(msgformat);
                item.focus();
                return false;
            }
        }
        return true;
    }

    return {
        IsFunction: isFunction,
        GethasDirtyData: getHasDirtyData,
        ChangeDirtyData: changeDirtyData,
        IsEmpty: isEmpty,
        StringContains: stringContains,
        CheckFloatNumber: checkFloatNumber,
    };
})();