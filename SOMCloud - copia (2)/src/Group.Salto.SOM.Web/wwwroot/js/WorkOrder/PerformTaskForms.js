var performTaskForms = performTaskForms || {};
performTaskForms = function () {

    var defaults = {
        classes: {
            select: "",
            header: {
                root: "",
                group: ""
            },
            buttons: {
                add: "btn btn-success",
                delete: "btn btn-success"
            }
        }
    };

    var arrService, arrClosingCodes, collectionsExtraField;
    function setInitialValues() {

        arrService = settings.arrTypes;
        arrClosingCodes = arrService.closingCodes;
        collectionsExtraField = arrService.collectionsExtraField;
        loadSkeleton();
        $('#perfomTask').modal();
        $('#perfomTask').modal('handleUpdate');
    }

    function loadSkeleton() {
        var bodyContainer, htmlCollectionEstraField = '', htmlInstalation, htmlclosingCodes = '';
        var titleService = '<h5 class="card-title">' + arrService.name + '</h5>';
        var titleCollectionEstraField = '<div id="headContentExtraField">'
            + '<h6 class="card-subtitle mb-2 text-muted">' + collectionsExtraField.name + '</h6>'
            + '</div>';
        var extraFieldValues = collectionsExtraField.extraFieldValues;

        for (let i = 0; i < extraFieldValues.length; i++) {
            var idExtraFieldValue = collectionsExtraField.extraFieldValues[i].id;
            var nameExtraFieldValue = collectionsExtraField.extraFieldValues[i].name;
            var mandatory = collectionsExtraField.extraFieldValues[i].isMandatory;
            var itemForSelect = collectionsExtraField.extraFieldValues[i].allowedStringValues;
            var typeExtraFieldValues = extraFieldValues[i].typeId;

            switch (typeExtraFieldValues) {
                case 10:
                    var instalationHtml = createInstalation(collectionsExtraField.extraFieldValues[i].materialList, idExtraFieldValue, nameExtraFieldValue);
                    htmlCollectionEstraField += instalationHtml;
                    break;
                case 9:
                case 8:
                case 4:
                    var inputTypeTextHtml = '';
                    var typeInput = typeExtraFieldValues === 4 ? 'number' : 'text';
                    if (itemForSelect.length > 0) {
                        var arrStringValue = itemForSelect.split(';');
                        inputTypeTextHtml = createSelects(arrStringValue, idExtraFieldValue, nameExtraFieldValue);
                    } else {
                        inputTypeTextHtml = createInputs(nameExtraFieldValue, idExtraFieldValue, typeInput, mandatory);
                    }

                    let htmlContainer = '<div id="type-' + idExtraFieldValue + '">' + inputTypeTextHtml + '</div>';
                    htmlCollectionEstraField += htmlContainer;
                    break;
                case 12:
                    var txtArea = createTextArea(idExtraFieldValue, nameExtraFieldValue);
                    var htmlCont = '<div id="textArea-' + idExtraFieldValue + '">' + txtArea + '</div>';

                    htmlCollectionEstraField += htmlCont;
                    break;
                case 6:
                    var switchHtml = createBoolean(nameExtraFieldValue, idExtraFieldValue, 'switchName', 'YES', 'NO');
                    var switchCont = '<div id="switch-' + idExtraFieldValue + '">' + switchHtml + '</div>';

                    htmlCollectionEstraField += switchCont;
                    break;
                case 2:
                    var timeHtml = createDatePicker(idExtraFieldValue, nameExtraFieldValue, true, false);
                    var timeCont = '<div id="time-' + idExtraFieldValue + '">' + timeHtml + '</div>';

                    htmlCollectionEstraField += timeCont;
                    break;
                default:

            }

        }

        bodyContainer = titleService + titleCollectionEstraField + '<div id="extraFieldValues-Container">' + htmlCollectionEstraField + '</div>';
        var htmlContainer = '<div class="form-group"><div id="extraFieldValuesContent">' + bodyContainer + '</div><div class="col-12"></div></div>';

        $(container).append(htmlContainer);
    }

    function createInstalation(extraFieldValueInstalation, idSelect, textLabel) {

        var arrMaterialList = [];

        for (let i = 0; i < extraFieldValueInstalation.length; i++) {
            var itemExists = false;
            $.each(arrMaterialList, function (k, v) {
                if (v.key === extraFieldValueInstalation[i].reference) {
                    itemExists = true;
                }
            });
            if (!itemExists) {
                var item = {
                    key: extraFieldValueInstalation[i].reference,
                    value: extraFieldValueInstalation[i].description
                };
                arrMaterialList.push(item);
            }
        }

        var htmlSelect = createSelects(arrMaterialList, idSelect, textLabel);
        var htmlContainer = '<div id="instalation">' + htmlSelect + '</div>';

        return htmlContainer;
    }

    function createSelects(arrOptionsSelect, idSelect, txtLabel) {
        var htmlOptions, arrOptions = [];

        for (let i = 0; i < arrOptionsSelect.length; i++) {
            var key = arrOptionsSelect[i]["key"] !== undefined ? arrOptionsSelect[i].key : i;
            var value = arrOptionsSelect[i]["value"] !== undefined ? arrOptionsSelect[i].value : arrOptionsSelect[i];
            var optionContainer = '<option value="' + key + '">' + value.trim() + '</option>';

            arrOptions.push(optionContainer);
        }

        htmlOptions = arrOptions.join('');

        var htmlSelect = '<label for="' + idSelect + '">' + txtLabel + '</label>'
            + '<select class="form-control" id="' + idSelect + '">'
            + htmlOptions
            + '</select>';


        return htmlSelect;
    }

    function createInputs(label, idInput, typeInput, required) {
        var htmlRequired = '<span class="required">*</span>';
        var addRequired = required ? htmlRequired : '';
        var htmlInput = " <div class='form-group'>"
            + "<label for='" + idInput + "'>" + label + addRequired + "</label>"
            + "<input type='" + typeInput + "' class='form-control' id='" + idInput + "' '" + required + "'>"
            + "</div>";
        return htmlInput;
    }

    function createFile(idInput, txtLabel) {
        var htmlFile = '<div class="custom-file">'
            + '<input type="file" class="custom-file-input" id="' + idInput + '">'
            + '<label class="custom-file-label" for="' + idInput + '">' + txtLabel + '</label>'
            + '</div>';
        return htmlFile;
    }

    function createTextArea(idSelect, txtLabel) {
        var htmlTextArea = '<div class="form-group">'
            + '<label for="' + idSelect + '">' + txtLabel + '</label >'
            + '<textarea class="form-control" id="' + idSelect + '" rows="1"></textarea>'
            + '</div >';
        return htmlTextArea;
    }

    function initializeAutocomplete(containerAutocomplete, options) {
        var constructCombo = new autocompleteJson();
        constructCombo.Init("#" + containerAutocomplete + "",
            {
                arr: options.itemCollection,
                selectControlId: options.selectControlId,
                withoutButton: options.withoutButton,
                placeholder: options.placeholder
            });

        return constructCombo;
    }

    function createBoolean(labelSwitch, idSwitch, nameSwitch, statusYes, statusNo) {
        var htmlSwitch =
            '<span class="d-block">' + labelSwitch + '</span>'
            + '<label class="switch switch-label switch-pill switch-primary mt-2">'
            + '<input type="checkbox" class="switch-input" id="' + idSwitch + '" data-val="false" name="' + nameSwitch + '/">'
            + '<span class="switch-slider" data-checked="' + statusYes + '" data-unchecked="' + statusNo + '"></span>'
            + '</label>';
        return htmlSwitch;
    }

    function createDatePicker(idCalendar, txtLabel, onlyTime, showTime) {
        var idDatePicker = 'calendar-' + idCalendar;
        var inputCalendar = '<label for="' + idDatePicker + '">' + txtLabel + '</label>'
            + '<input type="text" id="' + idDatePicker + '" data-onlyTime="' + onlyTime + '" data-showTime="' + showTime + '" class="form-control">';
        var cultureInfo = getCookie("culture-code").toLowerCase();

        $('#perfomTask').on('show.bs.modal', function (event) {
            var datePicker = new dhtmlXCalendarObject(idDatePicker);

            datePicker.loadUserLanguage(cultureInfo);

            if (onlyTime) {
                datePicker.hide();
                datePicker.showTime();
            }

            if (showTime) {
                datePicker.showTime();
            }
            datePicker.attachEvent("onShow", function () {
                this.contDates.style.display = "none";
                this.contDays.style.display = "none";
                this.contMonth.style.display = "none";

            });
            datePicker.attachEvent("onTimeChange", function (d) {
                this.i[this._activeInp].input.value = d.getHours() + ":" + d.getMinutes();
            });

        });

        return inputCalendar;
    }

    var init = function (startContainer, options) {
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
    };

    return {
        Init: init
    };
};