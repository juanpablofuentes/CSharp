var autoCompleteListSelector = autoCompleteListSelector || {};

autoCompleteListSelector = function () {
    var defaults = {
        selectedItems: [],
        classes: {
            select: "form-control selecTable",
            header: {
                root: "",
                group: "form-group"
            },
            buttons: {
                add: "btn btn-green-sm"
            }
        }
    };
    var container;
    var settings;
    var instancePrincipalSelector;
    var instanceList;
    var instanceButton;
    var principalSelectorText = "";
    var column1Text;
    var column2Text;
    var showId = true;
    var displayColumn;
    var column3Text;
    var urlPrincipalCombo;
    var selectedValues;
    var classes;
    var principalSelectorValues;
    var autoCompleteSelector;
    var minimumCharacters;
    var hasDefaultItem;
    var instanceSuggestions;
    var getDataMethod;
    var ajaxMethodType = "GET";
    var editMethod;
    var showComboSelector = true;
    var instanceDirtyDataInput;

    function setInitialValues() {
        instancePrincipalSelector = Math.random().toString().replace(".", "");
        instanceList = Math.random().toString().replace(".", "");
        instanceButton = Math.random().toString().replace(".", "");
        instanceDirtyDataInput = Math.random().toString().replace(".", "");
        urlPrincipalCombo = settings.urlPrincipalCombo;
        selectedValues = settings.selectedItems;
        column1Text = settings.column1Text;
        column2Text = settings.column2Text;
        column3Text = settings.column3Text;
        if (settings.showId === undefined) {
            settings.showId = true;
        }
        showId = settings.showId;
        minimumCharacters = settings.minimumCharacters;
        getDataMethod = settings.getDataMethod;
        ajaxMethodType = settings.ajaxMethodType;
        editMethod = settings.editMethod;
        if (selectedValues === undefined || selectedValues === null) {
            selectedValues = [];
        }
        if (settings.showComboSelector === false) {
            showComboSelector = false;
        }
        displayColumn = !showId ? 'd-none' : '';
    }

    function loadSkeleton() {
        var classCombo = "";
        if (!showComboSelector) {
            classCombo = " d-none";
        }
        const input =
            "<div class='" + settings.classes.header.root + "'>"
            + "<div class='input-group mb-2'>"
            + "<input id='dirtyData_" + instanceDirtyDataInput + "' type='hidden' class=\"form-control dirtyData\"></input>"
            + "<label for='slct_" + instancePrincipalSelector + "'>" + principalSelectorText + "</label>"
            + "<div id='slct_" + instancePrincipalSelector + "' class='form-control border-0 p-0" + classCombo + "'></div>"
            + "<span class='input-group-append" + classCombo + "'>"
            + "<button class='" + settings.classes.buttons.add + classCombo + "' id='btnAdd_" + instanceButton + "'>"
            + "<span class='fa fa-plus fa-lg text-primary'></span>"
            + "</button>"
            + "</span>"
            + "</div>"
            + "<ul id='list_" + instancePrincipalSelector + "' class='suggestions list-group d-none'></ul>"
            + "</div>";

        var table = "<div class='table-responsive'>"
            + "<table id='table_" + instanceList + "' class='table table-hover'>"
            + "<thead class='thead-som'><tr><th class='" + displayColumn + "'>" + column1Text + "</th><th>" + column2Text + "</th><th></th></tr></thead>"
            + "<tbody id='tableBody_" + instanceList + "'>"
            + "</tbody>"
            + "</table>"
            + "</div>";

        $(container).html(input);
        $(container).append(table);
        $("#btnAdd_" + instanceButton).on("click", addElementToList);
        if (showComboSelector) {
            loadAutoCompleteSelector("#slct_" + instancePrincipalSelector, settings.classes.header.root, settings.classes.header.group, principalSelectorText);
        }
    }

    function loadAutoCompleteSelector(container, cssRoot, cssHeader, selectorTxt) {
        autoCompleteSelector = new autocomplete();
        autoCompleteSelector.Init(container,
            {
                urlData: urlPrincipalCombo,
                minimumCharacters: minimumCharacters,
                hasDefaultItem: false,
                defaultItemContent: "",
                showCleanButton: false,
                ajaxMethodType: ajaxMethodType,
                getDataMethod: getDataMethod
            });
    }

    function sortArrayAlphabetically(array) {
        array = array.sort(function (a, b) {
            var result = 0;
            if (a.text < b.text) {
                result = -1;
            }
            else if (a.text > b.text) {
                result = 1;
            }

            return result;
        });

        return array;
    }

    function renderTable(table, items) {
        var tbody = $("#tableBody_" + instanceList);
        tbody.empty();
        for (var i = 0; i < items.length; i++) {
            var currentItem = items[i];
            var text = currentItem.Text;
            var secondaryText = currentItem.TextSecondary;

            if (text === null) {
                text = "";
            }
            if (secondaryText === null) {
                secondaryText = "";
            }
            var span = "></span>";
            if (showId) { span = ">" + currentItem.Value + "</span>"; }
            const row = $(
                "<tr>"
                + "<td class='" + displayColumn + "'>"
                + "<input type='hidden' class=\"form-control\" "
                + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemIdProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemIdProperty + "'"
                + " value = '" + currentItem.Value + "' readonly></input>"
                + "<span"
                + " id='" + settings.collectionProperty + "__" + i + "__" + settings.itemIdProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemIdProperty + "'"
                + span
                + "</td>"
                + "<td>"
                + "<input type='hidden' class=\"form-control\" "
                + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemTextProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemTextProperty + "'"
                + " value = '" + text + "' readonly></input>"
                + "<span"
                + " id='" + settings.collectionProperty + "__" + i + "__" + settings.itemTextProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemTextProperty + "'"
                + ">" + text + "</span>"
                + "</td>"
                + "</tr>");

            var htmlBtnEdit =
                "<a id=" + currentItem.Value + " href='#' class='editButton'>"
                + "<i class=\"fa fa-pencil fa-lg\"></i>"
                + "</a>";

            var htmlBtnDelete =
                "<a id=" + currentItem.Value + " href='#' class='deleteButton'>"
                + "<i class=\"fa fa-trash-o fa-lg\"></i>"
                + "</a>";

            if (editMethod !== undefined && editMethod !== null && app.common.validations.IsFunction(editMethod)) {
                var deleteBtn = "";
                if (currentItem.CanDelete) {
                    deleteBtn = htmlBtnDelete;
                }
                var tdContent = "<td><div class=\"col-update text-right\">" + htmlBtnEdit + deleteBtn + "</div></td>";

                row.append(tdContent);
            } else {
                if (currentItem.CanDelete) {

                    tdContent = "<td><div class=\"col-update text-right\">" + htmlBtnDelete + "</div></td>";
                    row.append(tdContent);
                }
            }
            removeItemFromSelector(currentItem);
            tbody.append(row);
            row.find('a[class=deleteButton]').on('click', onRemoving);
            row.find('a[class=editButton]').on('click', onEdit);
        }

        return table;
    }

    function removeItemFromSelector(item) {
        if (principalSelectorValues !== undefined && principalSelectorValues !== null) {
            var element = principalSelectorValues.find(x => x.value == item.Value);
            removeItemFrom(principalSelectorValues, element);
            sortArrayAlphabetically(principalSelectorValues);
        }
        updateElementsSelected();
    }

    function addElementToList(evt) {
        evt.preventDefault();
        if (autoCompleteSelector !== undefined) {
            var itemCombo1 = autoCompleteSelector.GetSelectedOption();
            if (itemCombo1 !== undefined && itemCombo1.key !== undefined) {
                var item = {
                    Value: itemCombo1.key,
                    Text: itemCombo1.value,
                    CanDelete: true,
                };
                item = addItemTo(selectedValues, item);
                autoCompleteSelector.RemoveSelectedOption();
                renderTable("#table_" + instanceList, selectedValues);
                app.common.validations.ChangeDirtyData();
            }
        }
    }

    function updateElementsSelected() {
        var elements = [];
        for (var i = 0; i < selectedValues.length; i++) {
            elements.push({
                key: selectedValues[i].Value,
                value: selectedValues[i].Text
            });
        }
        if (autoCompleteSelector !== undefined) {
            autoCompleteSelector.UpdateBlackList(elements);
        }
    }

    function addItemTo(array, item) {
        if (array === undefined) {
            array = [];
        }
        array.push(item);
        return item;
    }

    function onRemoving() {
        event.preventDefault();
        var item = selectedValues.find(function (element) {
            return "" + element.Value + "" === event.currentTarget.id;
        });
        addItemOnSelector();
        remove(item);
        app.common.validations.ChangeDirtyData();
    }

    function onEdit() {
        event.preventDefault();
        var item = selectedValues.find(function (element) {
            return "" + element.Value + "" === event.currentTarget.id;
        });
        editMethod(item.Value);
    }

    function addItemOnSelector() {
        updateElementsSelected();
    }

    function remove(item) {
        item = removeItemFrom(selectedValues, item);
        renderTable("#table_" + instanceList, selectedValues);
    }

    function removeItemFrom(array, item) {
        var index = array.indexOf(item);
        if (index !== -1) {
            item = array[index];
            array.splice(index, 1);
        }
        return item;
    }

    function getSelectedIds() {
        var result = [];
        for (var i = 0; i < selectedValues.length; i++) {
            addItemTo(result, selectedValues[i].Value)
        }
        return result.toString();
    }

    function getSelectedLabel() {
        var result = [];
        for (var i = 0; i < selectedValues.length; i++) {
            addItemTo(result, selectedValues[i].Text)
        }
        return result.toString();
    }

    function removeAllSelectedValues() {
        while (selectedValues.length > 0) {
            var item = selectedValues[0];
            item = removeItemFrom(selectedValues, item);
        }
        updateElementsSelected();
        renderTable("#table_" + instanceList, selectedValues);
        app.common.validations.ChangeDirtyData();
    }

    function getSelectedValues() {
        return selectedValues;
    }

    var init = function (startContainer, options) {
        classes = defaults.classes;
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton();
        renderTable("#table_" + instanceList, selectedValues);
    };
    return {
        Init: init,
        GetSelectedIds: getSelectedIds,
        GetSelectedLabel: getSelectedLabel,
        RemoveAllSelectedValues: removeAllSelectedValues,
        GetSelectedValues: getSelectedValues,
    };
};