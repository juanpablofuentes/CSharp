var app = app || {};
app.colorPickerContrast = app.colorPickerContrast || {};
app.colorPickerContrast = (function () {

    var idContent, colorTxt, bgColor, _settings, callFrom;

    function setInitialValues() {
        idContent: _settings.idContent;
        bgColor = _settings.bgColor;
        colorTxt = _settings.colorTxt;

        if (idContent === undefined) {
            var contId = _settings.idContent;
            console.info(_settings.idContent);
        }
    }

    var contrast = function () {
        var $parent = $('#' + idContent);

        var colorInput = toRgb(bgColor);
        var cr = colorInput[0], cg = colorInput[1], cb = colorInput[2];
        var brightnessInput = Math.round(((parseInt(cr) * 299) + (parseInt(cg) * 587) + (parseInt(cb) * 114)) / 1000);
        if (brightnessInput > 125) {
            input.classList.remove('text-white');
        } else {
            input.classList.add('text-white');
        }
    };

    function toRgb(color) {
        var rgb = color.replace(/^rgba?\(|\s+|\)$/g, '').split(',');
        var result = [];
        for (var i in rgb) {
            result.push(rgb[i]);
        }
        return result;
    }

    function hexToRgba(hex) {
        if (/^#([A-Fa-f0-9]{3}){1,2}$/.test(hex)) {
            hex = hex.substring(1).split('');
            if (hex.length == 3) {
                hex = [hex[0], hex[0], hex[1], hex[1], hex[2], hex[2]];
            }
            hex = '0x' + hex.join('');
            return 'rgba(' + [(hex >> 16) & 255, (hex >> 8) & 255, hex & 255].join(',') + ',1)';
        }
    }

    function hexToRgb(hex) {
        hex = hex.replace('#', '');
        r = parseInt(hex.substring(0, 2), 16);
        g = parseInt(hex.substring(2, 4), 16);
        b = parseInt(hex.substring(4, 6), 16);

        result = [r, g, b];
        return result;
    }

    function isHex(hex) {
        var isOk = false;
        if (hex.length > 3) {
            isOk = /^#[0-9A-F]{6}$/i.test(hex);
        } else {
            isOk = /(^#[0-9A-F]{6}$)|(^#[0-9A-F]{3}$)/i.test(hex);
        }
        return isOk;
    }

    function isRgb(rgb) {
        var isOk = false;
        if (rgb !== null) {
            isOk = /^rgb[(](?:\s*0*(?:\d\d?(?:\.\d+)?(?:\s*%)?|\.\d+\s*%|100(?:\.0*)?\s*%|(?:1\d\d|2[0-4]\d|25[0-5])(?:\.\d+)?)\s*(?:,(?![)])|(?=[)]))){3}[)]$/i.test(rgb);
        }
        return isOk;
    }

    function isRgba(rgba) {
        var isOk = false;
        if (rgba !== null) {
            isOk = /^rgba[(](?:\s*0*(?:\d\d?(?:\.\d+)?(?:\s*%)?|\.\d+\s*%|100(?:\.0*)?\s*%|(?:1\d\d|2[0-4]\d|25[0-5])(?:\.\d+)?)\s*,){3}\s*0*(?:\.\d+|1(?:\.0*)?)\s*[)]$/i.test(rgba);
        }
        return isOk;
    }

    var init = function (settings) {
        _settings = settings;
        setInitialValues();
        checkContrast = contrast(idContent, bgColor);
    };

    return {
        Init: init
    };
})();