var comboInputListSelector = comboInputListSelector || {};

comboInputListSelector = function () {
    var defaults = {
        selectedItems: [],
        classes: {
            select: "form-control selecTable mt-1",
            header: {
                root: "row",
                group: "form-group"
            },
            buttons: {
                add: "btn btn-green-sm"
            },
            columns: {
                first: "col-sm-8 pr-0",
                second: "col-sm-2 p-0",
                last: "col-sm-2 p-0"
            }
        }
    };
    var container;
    var settings;
    var instancePrincipalSelector;
    var instanceInputText;
    var instanceList;
    var instanceButton;
    var principalSelectorText = "";
    var column1Text, column2Text, column3Text;
    var urlPrincipalCombo;
    var selectedValues;
    var principalSelectorValues;
    var inputPlaceHolder;
    var colFirst, colSecond, colLast;

    function setInitialValues() {
        instancePrincipalSelector = Math.random().toString().replace(".", "");
        instanceInputText = Math.random().toString().replace(".", "");
        instanceList = Math.random().toString().replace(".", "");
        instanceButton = Math.random().toString().replace(".", "");
        urlPrincipalCombo = settings.urlPrincipalCombo;
        selectedValues = settings.selectedItems;
        column1Text = settings.column1Text;
        column2Text = settings.column2Text;
        column3Text = settings.column3Text || " ";
        inputPlaceHolder = settings.inputPlaceHolder;
        if (selectedValues === undefined || selectedValues === null) {
            selectedValues = [];
        }

        colFirst = settings.colFirst || settings.classes.columns.first;
        colSecond = settings.colSecond || settings.classes.columns.second;
        colLast = settings.colLast || settings.classes.columns.last;
    }

    function loadSkeleton() {
        const input =
            "<div class='" + settings.classes.header.root + "'>"
            + "<div class='" + settings.classes.header.group + " " + colFirst + "'>"
            + "<label class='m-0' for='slct_" + instancePrincipalSelector + "'>" + principalSelectorText + "</label>"
            + "<select id='slct_" + instancePrincipalSelector + "' class='form-control my-1 mr-sm-2'>"
            + "</select>"
            + "</div>"
            + "<div class='" + settings.classes.header.group + " " + colSecond + "'>"
            + "<label class='m-0' for='slct_" + instanceInputText + "'></label>"
            + "<input type = 'text' class='textelement " + settings.classes.select + "' id='input_" + instanceInputText + "'"
            + "placeholder='" + inputPlaceHolder + "'/>"
            + "</div>"
            + "<div class='" + colLast + "'>"
            + "<label class='m-0'></label>"
            + "<div class='align-bottom mt-1'>"
            + "<button class='" + settings.classes.buttons.add + "' id='btnAdd_" + instanceButton + "'>"
            + "<span class='fa fa-plus fa-lg text-primary'></span>"
            + "</button>"
            + "</div>"
            + "</div>"
            + "</div>";

        var table = "<div class='table-responsive'>" +
            "<table id='table_" + instanceList + "' class='table'>"
            + "<thead class='thead-som'><tr>"
            + "<th>" + column1Text + "</th><th>" + column2Text + "</th><th>" + column3Text + "</th>"
            + "</tr ></thead > "
            + "<tbody id='tableBody_" + instanceList + "'>"
            + "</tbody>"
            + "</table>"
            + "</div>";
        $(container).html(input);
        $(container).append(table);
        $("#btnAdd_" + instanceButton).on("click", addElementToList);
    }

    function getdata(url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            success: success
        });
    }

    function loadPrincipalCombo() {
        getdata(urlPrincipalCombo, onGetPrincipalComboInfo);
    }

    function onGetPrincipalComboInfo(elements) {
        principalSelectorValues = elements;
        sortArrayAlphabetically(principalSelectorValues);
        loadSelector("#slct_" + instancePrincipalSelector, principalSelectorValues);
        renderTable("#table_" + instanceList, selectedValues);
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

    function loadSelector(selectorId, items) {
        var select = $(selectorId);
        select.empty();
        for (var i = 0; i < items.length; i++) {
            var option = "<option value='" + items[i].value + "'>" + items[i].text + "</option>";
            select.append($(option));
        }
        return select;
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
            const input = $(
                "<input type='hidden' data-val='true' data-val-required='The " + settings.itemIdProperty + " field is required.' class='hidden'"
                + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemIdProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemIdProperty + "'"
                + " value = '" + currentItem.Value + "'></input>"
                + "<input type='hidden' class=\"form-control\" "
                + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemTextProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemTextProperty + "'"
                + " value = '" + text + "' readonly></input>"
                + "<input type='hidden' class=\"form-control\" "
                + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemIdSecondaryProperty + "'"
                + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemIdSecondaryProperty + "'"
                + " value = '" + currentItem.ValueSecondary + "' readonly></input>"

            );
            const row = $(
                "<tr>"
                    + "<td>"
                    
                        + "<span"
                        + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemTextProperty + "'"
                        + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemTextProperty + "'"
                        + " >" + text + "</span>"
                    + "</td>"
                    + "<td>"
                        + "<input type='text' class=\"form-control\" "
                        + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemTextSecondaryProperty + "'"
                        + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemTextSecondaryProperty + "'"
                        + " value = '" + secondaryText + "' placeholder='" + inputPlaceHolder + "'></input>"
                    + "</td>"
                    + "<td id='col-btn-remove'>"
                        + "<div class='text-right'>"
                        + "<a id='" + currentItem.Value + "' href='#' class='deleteButton'>"
                        + "<span class='fa fa-lg fa-remove text-danger'></span>"
                        + "</a>"
                        + "</div>"
                    + "</td>"
                + "</tr>");

            removeItemFromSelector(currentItem);
            tbody.append(input);
            tbody.append(row);
            row.find('a').on('click', onRemoving);
        }

        return table;
    }

    function removeItemFromSelector(item) {
        var element = principalSelectorValues.find(x => x.value === item.Value.toString());
        removeItemFrom(principalSelectorValues, element);
        sortArrayAlphabetically(principalSelectorValues);
        loadSelector("#slct_" + instancePrincipalSelector, principalSelectorValues);
    }

    function addElementToList(evt) {
        evt.preventDefault();
        var itemCombo1 = $("#slct_" + instancePrincipalSelector + " option:selected");
        var itemCombo2 = $("#input_" + instanceInputText);
        var item = {
            Value: itemCombo1.val(),
            Text: itemCombo1.text(),
            ValueSecondary: 0,
            TextSecondary: itemCombo2.val()
        };
        if (item.Text !== "" && item.TextSecondary !== "") {
            itemCombo2.val("");
            item = addItemTo(selectedValues, item);
            renderTable("#table_" + instanceList, selectedValues);
        }
    }

    function addItemTo(array, item) {
        array.push(item);
        return item;
    }

    function onRemoving() {
        event.preventDefault();
        var item = selectedValues.find(function (element) {
            return "" + element.Value + "" === event.currentTarget.id;
        });
        addItemOnSelector(item);
        remove(item);
    }

    function addItemOnSelector(element) {
        var item = {
            value: element.Value,
            text: element.Text,
            valueSecondary: element.ValueSecondary,
            textSecondary: element.TextSecondary
        };
        addItemTo(principalSelectorValues, item);
        sortArrayAlphabetically(principalSelectorValues);
        loadSelector("#slct_" + instancePrincipalSelector, principalSelectorValues);
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

    var init = function (startContainer, options) {
        classes = defaults.classes;
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton();
        loadPrincipalCombo();
    };

    return {
        Init: init
    };
};