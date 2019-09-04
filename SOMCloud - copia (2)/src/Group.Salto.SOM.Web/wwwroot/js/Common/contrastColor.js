var app = app || {};
app.contrastColor = app.contrastColor || {};
app.contrastColor = (function () {

    var parentElem, elemChild, childColor, iconDetail, _settings, callFrom, txtColor;

    function setInitialValues() {
        callFrom: _settings.callFrom;
        parentElem = _settings.parentElem;
        elemChild = _settings.elemChild;
        childColor = _settings.childColor;
        iconDetail = _settings.iconDetail || '';
        txtColor = _settings.txtColor || '';
    }

    var contrast = function () {
        var $parent = $(parentElem);
        var siblings = $parent.children().length;
        var $elem = $(elemChild);

        if (callFrom === 'scheduler' || _settings.callFrom === 'scheduler') {
            for (var i = 0; i < siblings; i++) {
                var color = $elem[i].style.background;
                var rgb = toRgb(color);
                var r = rgb[0], g = rgb[1], b = rgb[2];
                var brightness = Math.round((parseInt(r) * 299 + parseInt(g) * 587 + parseInt(b) * 114) / 1000);

                var iconElem = $(iconDetail) || undefined;

                if (brightness > 125) {
                    $elem[i].classList.remove('text-white');
                    if (iconElem[i]) {
                        iconElem[i].classList.remove('icon_details_white');
                        iconElem[i].classList.add('icon_details_default');
                    }
                } else {
                    $elem[i].classList.add('text-white');
                    if (iconElem[i]) {
                        iconElem[i].classList.remove('icon_details_default');
                        iconElem[i].classList.add('icon_details_white');
                    }
                }
            }
        }

        if (callFrom === 'lightbox' || _settings.callFrom === 'lightbox') {
            var input = $parent.find('input')[0];
            var colorInput = toRgb(input.style.backgroundColor);
            var cr = colorInput[0], cg = colorInput[1], cb = colorInput[2];
            var brightnessInput = Math.round((parseInt(cr) * 299 + parseInt(cg) * 587 + parseInt(cb) * 114) / 1000);
            if (brightnessInput > 125) {
                input.classList.remove('text-white');
            } else {
                input.classList.add('text-white');
            }
        }

        if (callFrom === 'default' || _settings.childColor !== undefined && _settings.childColor !== '') {
            let rgb = [];

            if (childColor !== null && isHex(childColor)) {
                rgb = hexToRgb(childColor);
            } else {
                rgb = toRgb(childColor);
            }

            let r = rgb[0], g = rgb[1], b = rgb[2];
            let brightness = Math.round((parseInt(r) * 299 + parseInt(g) * 587 + parseInt(b) * 114) / 1000);

            if (!$elem.hasClass(_settings.defaulClassColor)) {
                $elem.css('background-color', childColor);
                if (brightness > 125 && _settings.elemChild !== '.dhx_tooltip_line') {
                    $elem.addClass('text-center');
                } else {
                    $elem.addClass('text-white text-shadow text-center');
                }
                $elem.find('label').toggleClass('text-shadow', brightness > 95);
            }
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
            if (hex.length === 3) {
                hex = [hex[0], hex[0], hex[1], hex[1], hex[2], hex[2]];
            }
            hex = '0x' + hex.join('');
            return 'rgba(' + [hex >> 16 & 255, hex >> 8 & 255, hex & 255].join(',') + ',1)';
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
        checkContrast = contrast(parentElem, childColor);
    };

    return {
        Init: init
    };
})();