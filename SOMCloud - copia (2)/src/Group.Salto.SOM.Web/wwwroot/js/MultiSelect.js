var app = app || {};
app.multiselect = app.multiselect || {};

app.multiselect = (function () {

    var init = function (name) {
        saveLocalStore(name);
    };

    var saveLocalStore = function (name) {
        var storageKey = localStorage.getItem(name);
        var storageToObj = JSON.parse(storageKey);

        if (storageToObj) {
            localStorage.removeItem(name);
        }

        var optionsList = $("#Container_" + name)[0].children;
        var toStorage = [];
        for (var i = 0; i < optionsList.length; i++) {
            var inputCkbx = optionsList[i].children[0];
            if (inputCkbx.checked) {
                toStorage.push(inputCkbx.id);
            }
        }
        if (toStorage.length > 0) {
            localStorage.setItem(name, JSON.stringify(toStorage));
        }
    };

    var refresh = function (name) {
        var optionsList = $("#Container_" + name);
        var storageKey = localStorage.getItem(name);
        var storageToObj = JSON.parse(storageKey);

        $(optionsList).find('div').each(function () {
            var $this = $(this);
            if ($this.hasClass('d-none')) {
                $this.removeClass('d-none');
            }
            var chcBx = $this.find('input[type=checkbox]');
            if (jQuery.inArray(chcBx.attr('id'), storageToObj) !== -1) {
                chcBx.prop('checked', true);
            } else {
                chcBx.prop('checked', false);
            }
        });

        $("#CheckBoxId_" + name).prop('checked', false);

        if ($("#onlySelected").hasClass('active')) {
            $("#onlySelected").toggleClass('active');
            $("#onlySelected").find('i').removeClass('fa-eye-slash').addClass('fa-eye');
        }
    };

    var selectAll = function (name) {
        var btnOnlySelected = $("#onlySelected");
        var optionsList = $("#Container_" + name)[0].children;
        var inputSearch = $("#InputId_" + name).val().toLowerCase();
        if (inputSearch !== "") {
            $(optionsList).each(function () {
                var $this = $(this);
                var chcBx = $this.find('input[type=checkbox]');
                if (!$this.hasClass('d-none')) {
                    chcBx.prop('checked', true);
                }
            });
            $("#InputId_" + name).val('');
            $("#CheckBoxId_" + name).prop('checked', false);

            btnOnlySelected.toggleClass('active');
            btnOnlySelected.find('i').removeClass('fa-eye').addClass('fa-eye-slash');

        } else {
            showMatches(false, name);
        }
    };

    var onlySelected = function (name) {
        var btnOnlySelected = $("#onlySelected");
        var optionsList = $("#Container_" + name);
        var inputSearch = $("#InputId_" + name).val().toLowerCase();

        if ($('#CheckBoxId_ListPermissions').prop('checked') === true && inputSearch === "") {
            return;
        }

        if (btnOnlySelected.hasClass('active')) {
            showAll(name);

            btnOnlySelected.find('i').removeClass('fa-eye-slash').addClass('fa-eye');
            return;
        }

        $(optionsList).find('div').each(function () {
            var $this = $(this);
            var chcBx = $this.find('input[type=checkbox]');
            if (chcBx.prop('checked') === false) {
                $this.addClass('d-none');
            } else {
                if ($this.hasClass('d-none')) {
                    $this.removeClass('d-none');
                }
            }
        });

        $("#CheckBoxId_" + name).prop('checked', false);

        btnOnlySelected.toggleClass('active');
        btnOnlySelected.find('i').removeClass('fa-eye').addClass('fa-eye-slash');
    };

    var updateShownOptions = function (name) {
        var btnOnlySelected = $("#onlySelected");
        var inputSearch = $("#InputId_" + name).val().toLowerCase();
        var optionsList = $("#Container_" + name)[0].children;
        if (btnOnlySelected.hasClass('.active')) {
            btnOnlySelected.remove('active');
            btnOnlySelected.find('i').removeClass('fa-eye').addClass('fa-eye-slash');
        }

        if (inputSearch === "") {
            showAll(name);
        }
        else {
            for (var i = 0; i < optionsList.length; i++) {
                var optionValue = optionsList[i].getElementsByTagName('label').item(0).innerText;
                if (optionValue.toLowerCase().match(inputSearch)) {
                    if (optionsList[i].classList.contains('d-none')) {
                        optionsList[i].classList.remove('d-none');
                    }
                }
                else {
                    optionsList[i].classList.add('d-none');
                }
            }
        }

        $("#CheckBoxId_" + name).prop('checked', false);
    };

    var showMatches = function (param, name) {
        var checkBox = $("#CheckBoxId_" + name)[0];

        showAll(name);

        if (param) {
            checkBox.checked = false;
        }

        var optionsList = $("#Container_" + name)[0].children;

        for (var i = 0; i < optionsList.length; i++) {
            var inputCkbx = optionsList[i].children[0];
            inputCkbx.checked = checkBox.checked ? true : false;
        }
        return true;
    };

    var showAll = function (name) {
        var optionsList = $("#Container_" + name);

        $(optionsList).find('div').each(function () {
            var $this = $(this);
            if ($this.hasClass('d-none')) {
                $this.removeClass('d-none');
            }
        });

        if ($("#onlySelected").hasClass('active')) {
            $("#onlySelected").removeClass('active');
            $("#onlySelected").find('i').removeClass('fa-eye-slash').addClass('fa-eye');
        }
    };

    var evalShowMatches = function (name) {
        var checkBox = document.getElementById("CheckBoxId_" + name);
        if (checkBox.checked) {
            showMatches(false, name);
        }
    };

    var loadByIds = function (ids, name) {
        showMatches(false, name);
        var optionsList = $("#Container_" + name)[0].children;

        for (var i = 0; i < optionsList.length; i++) {
            var column = optionsList[i].children[0].id.replace('IsChecked', 'Value');
            var idValue = optionsList[i].children[column].value;
            var isinarray = ids.indexOf(idValue) > -1;
            if (isinarray) {
                var inputCkbx = optionsList[i].children[0];
                inputCkbx.checked = true;
            }
        }
    }

    var getSelectedIds = function (name) {
        var optionsList = $("#Container_" + name)[0].children;
        var result = [];
        for (var i = 0; i < optionsList.length; i++) {
            if (optionsList[i].children[0].checked)
            {
                var column = optionsList[i].children[0].id.replace('IsChecked', 'Value');
                var idValue = optionsList[i].children[column].value;
                addItemTo(result, idValue);
            }
        }
        return result.toString();
    }

    var getSelectedLabel = function (name) {
        var optionsList = $("#Container_" + name)[0].children;
        var result = [];
        for (var i = 0; i < optionsList.length; i++) {
            if (optionsList[i].children[0].checked) {
                var column = optionsList[i].children[0].id.replace('IsChecked', 'LabelName');
                var idValue = optionsList[i].children[column].value;
                addItemTo(result, idValue);
            }
        }
        return result.toString();
    }

    var loadMultiSelect = function (name, inputName, data) {
        $("#Container_" + name).html('');
        var multihtml = ''
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var item = data[i];
                var checked = item.isChecked ? 'checked="checked"' : '';
                var html = '<div class="custom-control custom-checkbox">'
                    + '<input type="checkbox" onclick="app.multiselect.evalShowMatches(\'' + name + '\')" class="custom-control-input" data-val="true" id="' + inputName + '_' + i + '__IsChecked" name="' + inputName.replace("_", ".") + '[' + i + '].IsChecked" ' + checked + ' value="true">'
                    + '<label class="custom-control-label" for="' + inputName + '_' + i + '__IsChecked">' + item.labelName + '</label>'
                    + '<input type="hidden" id="' + inputName + '_' + i + '__Value" name="' + inputName.replace("_", ".") + '[' + i + '].Value" value="' + item.value + '">'
                    + '<input type="hidden" id="' + inputName + '_' + i + '__LabelName" name="' + inputName.replace("_", ".") + '[' + i + '].LabelName" value="' + item.labelName + '">'
                    + '</div>';
                multihtml += html;
            }
            $("#Container_" + name).append(multihtml);
        }
    }

    function addItemTo(array, item) {
        array.push(item);
        return item;
    }

    return {
        Init: init,
        refresh: refresh,
        onlySelected: onlySelected,
        selectAll: selectAll,
        showAll: showAll,
        updateShownOptions: updateShownOptions,
        showMatches: showMatches,
        evalShowMatches: evalShowMatches,
        GetSelectedIds: getSelectedIds,
        GetSelectedLabel: getSelectedLabel,
        LoadByIds: loadByIds,
        LoadMultiSelect: loadMultiSelect
    };
})();