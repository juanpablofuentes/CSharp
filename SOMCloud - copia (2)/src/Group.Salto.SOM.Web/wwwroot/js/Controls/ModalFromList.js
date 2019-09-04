var modalFromList = modalFromList || {};
modalFromList = function (onCreateOverride, onEditOverride, onSaveModal, onValidationError) {

    var defaults = {
        selectedItems: [],
        classes: {
            select: "form-control selecTable",
            header: {
                root: "",
                group: "form-group"
            },
            buttons: {
                add: "btn btn-success mb-3 text-white"
            }
        }
    };

    var container, settings, instanceList, selectedValues, linkText, modalName, modalId, columnsText, columnsModal, columnsValues, hiddenInputs,
        dataUrl, placeholderElement, saveButton, dateColumns, initialIdValue, actionCreate, actionEdit, actionDelete, deleteModalButton, formatDate, columnPositionLinkItems, linkItems = false,
        state, multiSelect, textYes, textNo, canDeleteUrl, withOrder, showButtonOnTheGrid = true, containerBtnCreate;
    var onCreateOverride = onCreateOverride;
    var onEditOverride = onEditOverride;
    var onSaveModal = onSaveModal;
    var onValidationError = onValidationError;

    function setInitialValues() {
        instanceList = Math.random().toString().replace(".", "");
        selectedValues = settings.selectedItems;
        linkText = settings.linkText;
        modalName = settings.modalName;
        modalId = settings.id;
        columnsText = settings.columnsText;
        columnsModal = settings.columnsModal;
        columnsValues = settings.columnsValues;
        linkItems = settings.linkItems;
        columnPositionLinkItems = settings.columnPositionLinkItems;
        hiddenInputs = columnsModal.split(",");
        dataUrl = settings.dataUrl;
        placeholderElement = $("#" + settings.placeholderElement);
        saveButton = settings.saveButton;
        dateColumns = settings.dateColumns;
        initialIdValue = settings.initialIdValue;
        actionCreate = settings.actionCreate;
        actionEdit = settings.actionEdit;
        actionDelete = settings.actionDelete;
        deleteModalButton = settings.deleteModalButton;
        state = settings.state;
        multiSelect = settings.multiSelect;
        formatDate = settings.formatDate;
        textYes = settings.textYes;
        textNo = settings.textNo;
        canDeleteUrl = settings.canDeleteUrl;
        withOrder = settings.withOrder;
        containerBtnCreate = settings.containerBtnCreate;

        if (selectedValues === undefined || selectedValues === null) {
            selectedValues = [];
        }

        if (dateColumns !== undefined) {
            dateColumns = dateColumns.split(",");
        }

        if (multiSelect !== undefined) {
            multiSelect = multiSelect.split(",");
        }
        if (settings.showButtonOnTheGrid !== undefined) {
            showButtonOnTheGrid = settings.showButtonOnTheGrid;
        }
    }

    function formatValueSelectedDate() {
        var spandvalues = columnsValues.split(",");
        for (var i = 0; i < selectedValues.length; i++) {
            var currentItem = selectedValues[i];
            for (var s = 0; s < spandvalues.length; s++) {
                if (dateColumns !== undefined && dateColumns.filter(x => x === spandvalues[s]).length === 1) {
                    if (currentItem[spandvalues[s]] !== null) {
                        currentItem[spandvalues[s]] = moment(currentItem[spandvalues[s]]).format(formatDate);
                    }
                    else {
                        currentItem[spandvalues[s]] = "";
                    }
                }
            }
        }
    }

    function loadSkeleton() {
        var splitColumnsText = columnsText.split(",");
        const btnOnTheGrid = $(
            //"<div class='" + settings.classes.header.root + "'>"
            "<div>"
            + "<a id='CreateNew" + instanceList + "' class='" + settings.classes.buttons.add + "' data-toggle='modal' data-target='#" + modalName + "' data-url='" + dataUrl + "' > "
            + "<i class='fa fa-plus mr-1'></i>"
            + "<span>" + linkText + "</span>"
            + "</a></div>");
        //+ "</div>");

        btnOnTheGrid.find("#CreateNew" + instanceList).on('click', onCreate);

        var hr = "";
        for (var i = 0; i < splitColumnsText.length; i++) {
            hr += "<th>" + splitColumnsText[i] + "</th>";
        }

        var table = "<div class=\"table-responsive\">"
            + "<table id='table_" + instanceList + "' class=\"table\">"
            + "<thead class='thead-som'><tr>"
            + hr
            + "<th></th></thead>"
            + "<tbody id='tableBody_" + instanceList + "'>"
            + "</tbody>"
            + "</table>"
            + "</div>";

        if (showButtonOnTheGrid) {
            $(container).append(btnOnTheGrid);
        } else {
            $(containerBtnCreate).append(btnOnTheGrid);
        }
        $(container).append(table);
    }

    function renderTable(table, items) {
        var tbody = $("#tableBody_" + instanceList);
        tbody.empty();

        var spandvalues = columnsValues.split(",");
        for (var i = 0; i < items.length; i++) {
            var columnsCounter = 1;
            var currentItem = items[i];
            var spanrow = '';
            if (withOrder === undefined || withOrder === true) {
                spanrow = "<td><span>" + (i + 1) + "</span></td>";
            }
            for (var s = 0; s < spandvalues.length; s++) {
                var currentValue = currentItem[spandvalues[s]] || '';
                if (currentValue === true || currentValue === "true") {
                    spanrow += "<td><span>" + textYes + "</span></td>";
                }
                else if (currentValue === false || currentValue === "false") {
                    spanrow += "<td><span>" + textNo + "</span></td>";
                }
                else {
                    if (linkItems && columnsCounter === columnPositionLinkItems) {
                        var link = '<a href="' + currentValue + '" target="_blank">' + currentValue + '</a>';
                        spanrow += "<td><span>" + link + "</span></td>";
                    } else {
                        spanrow += "<td><span>" + currentValue + "</span></td>";
                    }
                }
                columnsCounter += 1;
            }
            spanrow += "<td><div class='col-update text-right'><a id='" + currentItem[modalId] + "' href='#' class='editButton' data-toggle='modal' data-target='#" + modalName + "'><span class='fa fa-lg fa-pencil'></span></a>";
            var deletemodal = "";
            if (actionDelete !== undefined)
                deletemodal = "data-toggle='modal' data-target='#deleteconfirmationmodal'";
            spanrow += "<a id='" + currentItem[modalId] + "' href='#' class='deleteButton'" + deletemodal + "><span class='fa fa-lg fa-trash-o'></span></a></div></td>";

            var hiddenRow = "";
            for (var c = 0; c < hiddenInputs.length; c++) {
                var value = "";
                if (currentItem[hiddenInputs[c]] !== null) {
                    value = "value = '" + currentItem[hiddenInputs[c]] + "'";
                }
                hiddenRow += "<input type='hidden' class='hidden' id='" + settings.collectionPropertyId + "_" + i + "__" + hiddenInputs[c] + "' name='" + settings.collectionPropertyName + "[" + i + "]." + hiddenInputs[c] + "'" + value + ">";
            }
            var hiddenState = '';
            if (state !== undefined) {
                var stateValue = currentItem[state] !== undefined ? currentItem[state] : '';
                hiddenRow += "<input type='hidden' class='hidden' id='" + settings.collectionPropertyId + "_" + i + "__" + state + "' name='" + settings.collectionPropertyName + "[" + i + "]." + state + "'value = '" + stateValue + "'>";

                if (stateValue === 'D')
                    hiddenState = "style = 'display:none;'";
            }

            const row = $(
                "<tr " + hiddenState + ">"
                + spanrow
                + hiddenRow
                + "</tr>");

            tbody.append(row);
            row.find('a.editButton').on('click', onEdit);
            if (actionDelete === undefined) {
                row.find('a.deleteButton').on('click', onRemoving);
            }
        }
        return table;
    }

    function onCreate(event) {
        setAction(actionCreate);
        event.preventDefault();
        if (multiSelect !== undefined) {
            app.multiselect.Init(multiSelect[0]);
            app.multiselect.showMatches(false, multiSelect[0]);
        }
        for (var i = 0; i < hiddenInputs.length; i++) {
            if ($("#" + hiddenInputs[i])[0].type === "checkbox") {
                $("#" + hiddenInputs[i]).val('false');
                $("#" + hiddenInputs[i]).prop("checked", false);
            }
            else {
                $("#" + hiddenInputs[i]).val('');
            }
            $('*[data-valmsg-for="' + hiddenInputs[i] + '"]').text('');
        }
        $("#" + modalId).val(initialIdValue);
        onCreateOverride();
    }

    function onEdit(event) {
        setAction(actionEdit);
        event.preventDefault();
        var item = selectedValues.find(function (element) {
            var id = modalId;
            return "" + element[id] + "" === event.currentTarget.id;
        });

        if (multiSelect !== undefined) {
            var ids = item[multiSelect[1]].split(",");
            var arrayIds = []
            for (var i = 0; i < ids.length; i++) {
                addItemTo(arrayIds, ids[i]);
            }

            app.multiselect.Init(multiSelect[0]);
            app.multiselect.LoadByIds(arrayIds, multiSelect[0]);
        }

        for (var num = 0; num < hiddenInputs.length; num++) {
            $("#" + hiddenInputs[num]).val(item[hiddenInputs[num]]);
            if ($("#" + hiddenInputs[num])[0].type === "checkbox") {
                var value = $("#" + hiddenInputs[num]).val();
                $("#" + hiddenInputs[num]).prop("checked", (/true/i).test(value));
            }
            $('*[data-valmsg-for="' + hiddenInputs[i] + '"]').text('');
        }
        if (onEditOverride !== undefined)
            onEditOverride();
    }

    var onSaveButton = function () {
        var item = selectedValues.find(function (element) {
            var id = modalId;
            return "" + element[id] + "" === $("#" + id).val();
        });

        if (multiSelect !== undefined) {
            var multiSelectIds = app.multiselect.GetSelectedIds(multiSelect[0]);
            var multiSelectLabels = app.multiselect.GetSelectedLabel(multiSelect[0]);
            multiSelectLabels = multiSelectLabels.replace(/,/g, ", ");
            if (multiSelectLabels.length > 40) {
                multiSelectLabels = multiSelectLabels.substring(0, 40);
                multiSelectLabels += "...";
            }

            $("#" + multiSelect[1]).val(multiSelectIds);
            $("#" + multiSelect[2]).val(multiSelectLabels);
        }

        if (item === undefined || item === null) {
            item = [];
            setData(item);
            setState(item, "C");
            item[modalId] = Math.floor(Math.random() * 200000) + 1;
            item = addItemTo(selectedValues, item);
        }
        else {
            setData(item);
            if (item[state] !== "C")
                setState(item, "U");
        }
        if (onSaveModal !== undefined)
            onSaveModal(item);
        refresh();
    };

    function setData(item) {
        for (var c = 0; c < hiddenInputs.length; c++) {
            item[hiddenInputs[c]] = $("#" + [hiddenInputs[c]]).val();
        }
    }

    function setState(item, value) {
        if (state !== undefined)
            item[state] = value;
    }

    function ensureCanDelete(id) {
        var res = { result: true, text: '' };
        if (canDeleteUrl !== undefined) {
            $.ajax({
                url: canDeleteUrl,
                type: 'GET',
                dataType: 'json',
                cache: false,
                async: false,
                data: { id: id },
                success: function (result) {
                    res = result;
                },
                error: function (result) { res = result; alert(result); }
            });
        }
        return res;
    }

    function onRemoving(event) {
        event.preventDefault();
        var item = selectedValues.find(function (element) {
            var id = modalId;
            return "" + element[id] + "" === event.currentTarget.id;
        });

        var canDeleteResult = ensureCanDelete(item[modalId]);
        if (canDeleteResult.result) {
            if (state !== undefined) {
                setState(item, "D");
                refresh();
            }
            else
                remove(item);
        }
        else {
            dhtmlx.alert(canDeleteResult.text);
        }
    }

    function remove(item) {
        item = removeItemFrom(selectedValues, item);
        refresh();
    }

    function removeItemFrom(array, item) {
        var index = array.indexOf(item);
        if (index !== -1) {
            item = array[index];
            array.splice(index, 1);
        }
        return item;
    }

    function calendarLoad() {
        if (dateColumns !== undefined) {
            var jsondatecolumn = "[";
            for (var c = 0; c < dateColumns.length; c++) {
                jsondatecolumn += '"' + dateColumns[c] + '",';
            }
            jsondatecolumn = jsondatecolumn.substring(0, jsondatecolumn.length - 1);
            jsondatecolumn += "]";
            var cultureInfo = getCookie("culture-code").toLowerCase();
            var datePicker = new dhtmlXCalendarObject(JSON.parse(jsondatecolumn));
            datePicker.hideTime();
            datePicker.loadUserLanguage(cultureInfo);
            datePicker.setDateFormat(GetCultureForDatePicker(cultureInfo, false), "%Y-%m-%d %H:%i");
        }
    }

    function addItemTo(array, item) {
        array.push(item);
        return item;
    }

    function setAction(action) {
        if (action !== undefined) {
            var form = placeholderElement.find('form');
            form.attr('action', action);
        }
    }

    var init = function (startContainer, options) {
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        formatValueSelectedDate();
        loadSkeleton();
        refresh();
        downloadModal(placeholderElement);
        placeholderElement.on('click', '#' + saveButton, placholderOnClick);
    };

    function placholderOnClick(event) {
        event.preventDefault();
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var dataToSend = form.serialize();

        $.post(actionUrl, dataToSend).done(function (data) {
            receiveData(data);
        });
    }

    function receiveData(data) {
        var newBody = $('.modal-body', data);
        placeholderElement.find('.modal-body').replaceWith(newBody);
        calendarLoad();

        var isValid = newBody.find('#IsValid').val() === 'True';
        if (isValid) {
            onSaveButton();
            $('button[data-dismiss="modal"]').click();
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
        }
        else {
            if (onValidationError !== undefined)
                onValidationError();
        }
    }

    function downloadModal(placeholderElement) {
        var url = $('#CreateNew' + instanceList).data('url');
        $.get(url).done(function (data) {
            placeholderElement.html(data);
            calendarLoad();
        });
    }

    function refresh() {
        renderTable("#table_" + instanceList, selectedValues);
        calendarLoad();
        if (actionDelete !== undefined) {
            $("a.deleteButton").on("click", function (evt) {
                evt.preventDefault();
                var id = $(this).attr('id');
                deleteElement(id, "#" + deleteModalButton);
            });
        }
    }

    function deleteElement(data, saveButtonId) {
        $(saveButtonId).on("click",
            function () {
                $.post(actionDelete, { id: data }).done(function (result) {
                    var item = selectedValues.find(function (element) {
                        var id = modalId;
                        return element[id] === result;
                    });
                    remove(item);
                });
            });
    }
  

    return {
        Init: init
    };
};