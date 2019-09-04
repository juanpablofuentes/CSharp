(function ($) {
    const defaults = {
        selectedItems: [],
        availableItems: [],
        columns: {
            show: false,
            name: null
        },
        classes: {
            buttons: {
                add: "",
                remove: ""
            },
            header: {
                root: "input-group",
                group: "input-group-append"
            },
            select: "form-control selecTable",
            table: "table table-hover non-border",
            tableHead: "d-none"
        },
        collectionProperty: "Items",
        itemIdProperty: "Id",
        itemTextProperty: "Text",
        allowNulls: true,
        textPlaceHolder: ""
    };

    $.fn.selecTable = function (options) {
        var settings = {};
        var instanceId = null;
        var self = null;

        function addItemTo(array, item) {
            array.push(item);
            return item;
        }

        function removeItemFrom(array, item) {
            var index = array.indexOf(item);
            if (index !== -1) {
                item = array[index];
                array.splice(index, 1);
            }
            return item;
        }

        function sortArrayAlphabetically(array) {
            array = array.sort(function (a, b) {
                var result = 0;
                if (a.value < b.value) {
                    result = -1;
                }
                else if (a.value > b.value) {
                    result = 1;
                }

                return result;
            });

            return array;
        }

        function renderTable(table, items) {
            var tbody = table.find("tbody");
            tbody.empty();
            for (var i = 0; i < items.length; i++) {
                var currentItem = items[i];
                var text = currentItem.text;
                if (text === null) {
                    text = "";
                }
                const input = $(
                    "<input type='hidden' data-val='true' data-val-required='The " + settings.itemIdProperty + " field is required.' class='hidden'"
                    + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemIdProperty + "'"
                    + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemIdProperty + "'"
                    + " value = '" + currentItem.value + "'></input>");
                const row = $(
                    "<tr>"
                    + "<td>"
                    + "<input type='text' class=\"form-control dirtyData\" "
                    + " id='" + settings.collectionProperty + "_" + i + "__" + settings.itemTextProperty + "'"
                    + " name = '" + settings.collectionProperty + "[" + i + "]." + settings.itemTextProperty + "'"
                    + " value = '" + text + "' placeholder='" + settings.textPlaceHolder + "'></input>"
                    + "<input class='hidden needConfirmClass' data-val='true' type='hidden' value='" + currentItem.needConfirmDelete + "'></input>"
                    + "</button>"
                    + "</div>"
                    + "</td>"
                    + "<td class=\"align-middle\">"
                    + "<a id='" + currentItem.value + "' href='#' class='deleteButton'>"
                    + "<span class='fa fa-lg fa-trash-o'></span>"
                    + "</a>"
                    + "</td>"
                    + "</tr>");

                row.append(input);
                tbody.append(row);
                row.find('a').on('click', onRemoving);
                row.find('input[type=text]').on('change', function () {
                    app.common.validations.ChangeDirtyData();
                });
            }

            return table;
        }

        var render = function () {
            renderTable(self.find("table"), settings.selectedItems);
        };

        var onAdding = function (event) {
            var selected = $(".textelement");
            event.preventDefault();
            //var selected = self.find("option:selected");
            if (selected.length !== 0) {
                if (settings.allowNulls === true || selected.val().length > 0) {
                    var item = {
                        value: 0,
                        text: selected.val(),
                        needConfirmDelete: false,
                    };
                    item = addItemTo(settings.selectedItems, item);
                    settings.selectedItems = sortArrayAlphabetically(settings.selectedItems);
                    render();
                    selected.val("");
                    app.common.validations.ChangeDirtyData();
                }
            }
        };

        var onRemoving = function (event) {
            event.preventDefault();
            var item = settings.selectedItems.find(function (element) {
                return "" + element.value + "" === event.currentTarget.id;
            });
            if (settings.confirmaMethod && typeof settings.confirmaMethod === "function" && item.needConfirmDelete === true) {
                settings.confirmaMethod(remove, item);
            }
            else {
                remove(item);
            }
        };

        function remove(item) {
            removeItemFrom(settings.selectedItems, item);
            settings.selectedItems = sortArrayAlphabetically(settings.selectedItems);
            settings.availableItems = sortArrayAlphabetically(settings.availableItems);
            render();
            app.common.validations.ChangeDirtyData();
        }

        function initialize(container) {
            const headerColumnName = settings.columns.show === true ? settings.columns.name : "";
            const skeleton =
                "<div class='" + settings.classes.header.root + "'>"
                + "<input type = 'text' class='textelement " + settings.classes.select + "' id='slct_" + instanceId + "'"
                + "placeholder='" + settings.textPlaceHolder + "'/>"
                + "<div class='" + settings.classes.header.group + "'>"
                + "<button class='" + settings.classes.buttons.add + "' id='btnAdd_" + instanceId + "'>"
                + "<span class='fa fa-plus fa-lg'></span>"
                + "</button> "
                + "</div>"
                + "</div>"
                + "<div class=\"table-responsive\">"
                + "<table class='" + settings.classes.table + "' id='tbl_" + instanceId + "'>"
                + "<thead class='" + settings.classes.tableHead + "'>"
                + "<tr>"
                + "<th>"
                + "</th>"
                + "<th>"
                + headerColumnName
                + "</th>"
                + "</tr>"
                + "</thead>"
                + "<tbody>"
                + "</tbody>"
                + "</table>"
                + "</div>";

            container.html(skeleton);
            container.find("button").on('click', onAdding);
            render();

            return container;
        }

        return this.each(function () {
            settings = $.extend(true, {}, defaults, options);
            if (settings.selectedItems === null) {
                settings.selectedItems = [];
            }
            self = $(this);
            instanceId = Math.random();
            initialize(self);
        });
    };
}(jQuery));