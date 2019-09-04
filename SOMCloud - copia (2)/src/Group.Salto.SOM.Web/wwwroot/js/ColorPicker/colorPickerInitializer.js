//ColorPicker Init
var app = app || {};
app.colorPicker = app.colorPicker || {};
app.colorPicker = (function () {
    var init = function (settings) {
        initializeColorPicker(settings);
    };

    function initializeColorPicker(settings) {
        dhtmlXColorPicker.prototype.i18n['custom'] = {
            btnSelect: settings.btnSelect,
            btnCancel: settings.btnCancel
        };

        var myColorPicker = new dhtmlXColorPicker({
            parent: settings.parent,
            closeable: true,
            skin: settings.skin,
            color: settings.color,
            hide: true
        });

        myColorPicker.linkTo(settings.input);

        myColorPicker.setPosition(settings.setPosition || "bottom");
        myColorPicker.attachEvent("onSelect", function (color, node) {
            app.contrastColor.Init(
                {
                    callFrom: 'default',
                    parentElem: '#colorPickerContainer',
                    elemChild: '#' + node.id,
                    childColor: color
                });
        });

        if (settings.toggle !== undefined) {
            toggleColorPicker(myColorPicker);
        }

        myColorPicker.loadUserLanguage('custom');

        var onShowEvent = myColorPicker.attachEvent("onShow", function (node) {
            $('.dhx_button_save').attr('type', 'button');
        });

        var onSelectEvent = myColorPicker.attachEvent("onSelect", function (color, node) {
            myColorPicker.hide();
        });
    }

    function toggleColorPicker(nameColorPkr) {
        var element = $('#' + nameColorPkr.base.offsetParent.id);
        var sectionHeight = 0;

        nameColorPkr.attachEvent("onShow", function () {
            sectionHeight = element.children("#colorPicker").children().height() + 30 + 'px';
            element.css("height", sectionHeight);
        });


        nameColorPkr.attachEvent("onHide", function () {

            var elem = element.children("#colorPicker");
            var h = elem.children().height();

            $('#colorPickerContainer').css("height", "21px");

            if (h) {
                elem.children().remove();
            }
        });

    }
    return {
        Init: init
    };
})();