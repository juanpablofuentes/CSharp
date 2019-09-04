var treeView = treeView || {};

treeView = function () {
    var defaults = {
        values: [],
        classes: {
            buttonOk: {
                add: 'btn btn-success'
            }
        }
    };
    var lastIndex = 0, container, settings, values, instanceTree, collectionProperty, textProperty, idProperty, descriptionProperty,
        baseColor, instanceButtonAddRoot, placeHolderText, placeHolderDescription, instanceInputSearcher, instanceButtonSearcher,
        validationTextMessage, validationDescriptionMessage, stringAddNew, btnAddNewRow, btnRemoveRow, canDeleteMethod, canDeleteTreeLevel,
        additionalProperties;

    function setInitialValues() {
        instanceTree = Math.random().toString().replace('.', '');
        values = prepareValues(settings.values, 0);
        collectionProperty = settings.collectionProperty;
        textProperty = settings.textProperty;
        idProperty = settings.idProperty;
        descriptionProperty = settings.descriptionProperty;
        baseColor = settings.baseColor;
        instanceButtonAddRoot = Math.random().toString().replace('.', '');
        placeHolderText = settings.placeHolderText;
        placeHolderDescription = settings.placeHolderDescription;
        validationTextMessage = settings.validationTextMessage;
        canDeleteMethod = settings.canDeleteMethod;
        canDeleteTreeLevelMethod = settings.canDeleteTreeLevelMethod;
        stringAddNew = settings.stringAddNew;
        btnAddNewRow = settings.stringAddNew;
        btnRemoveRow = settings.btnRemoveRow;
        validationDescriptionMessage = settings.validationDescriptionMessage;
        additionalProperties = settings.additionalProperties;
        instanceButtonSearcher = Math.random().toString().replace('.', '');
        instanceInputSearcher = Math.random().toString().replace('.', '');
    }

    function prepareValues(valuesToMap, parentId) {
        var tempValues = [];
        if (valuesToMap !== undefined && valuesToMap !== null && valuesToMap.length > 0) {
            for (var i = 0; i < valuesToMap.length; i++) {
                lastIndex += 1;
                var item = {
                    Content: valuesToMap[i],
                    IsCollapsed: true,
                    TemporalIndex: lastIndex,
                    ParentId: parentId
                };
                item.Content.Childs = prepareValues(valuesToMap[i].Childs, lastIndex);
                tempValues.push(item);
            }
        }
        return tempValues;
    }

    function getText(text) {
        if (text === undefined || text === null || text === 'null') {
            return '';
        }
        return text;
    }

    function jsGuid(dt) {
        var n = Math.floor(dt + Math.random() / 16);
        var result = n.toString().slice(8);
        return parseInt(result);
    }

    function renderBranch(items, level, collectionLevelProperty, searchValue) {
        var html = "";
        for (var i = 0; i < items.length; i++) {
            var collectionPropertyName = collectionLevelProperty + "[" + i + "].";
            var text = getText(items[i].Content.Name);
            var description = getText(items[i].Content.Description);
            var lum = level > 0 ? parseFloat(level * 0.14) : .05;
            var colorIcon = lum >= 0.6 ? " text-white" : "";
            var color = colorLuminance(baseColor, lum);
            var parentRow = level === 0 ? "parent-row" : "";
            var childrens = items[i].Content.Childs;

            var tempIndex = items[i].Clone ? items[i].Content.Id : items[i].TemporalIndex;
            var idClonedItem = items[i].Clone ? 0 : items[i].Content.Id;
            var isCollapased = items[i].IsCollapsed && app.common.validations.IsEmpty(searchValue) ? "" : "show";
            var classShowThisNode = app.common.validations.StringContains(text, searchValue) ? '' : 'd-none';
            var containsChildren = childrens !== undefined && childrens.length > 0;
            var spanToCollapse = containsChildren
                ? "<span class='fa fa-chevron-right rotate mr-1" + classShowThisNode + "'></span>" : "";
            const tmpNodeHtml =
                "<div class='row offset-" + level + " " + parentRow + " " + classShowThisNode + "' >"
                + "<div>"
                + "<input type='hidden' class='form-control dirtyData' "
                + "id='" + collectionProperty + "_" + i + "__" + idProperty + "' "
                + "name='" + collectionPropertyName + idProperty + "' "
                + "value='" + items[i].Content.Id + "'></input>"
                + getInputsAdditionalValues(collectionPropertyName, items[i], i)

                + "<input type='hidden' class='form-control dirtyData' "
                + "id='" + collectionProperty + "_" + i + "__" + idProperty + "' "
                + "name='" + collectionPropertyName + "TempId" + "' "
                + "value='" + tempIndex + "'></input>"

                + "<input type='hidden' class='form-control dirtyData' "
                + "id='" + collectionProperty + "_" + i + "__" + idProperty + "_Clone' "
                + "name='" + collectionPropertyName + "IdClonedItem" + "' "
                + "value='" + idClonedItem + "'></input>"

                + "<input type='hidden' class='form-control dirtyData' "
                + "id='" + collectionProperty + "_" + i + "__" + textProperty + "' "
                + "name='" + collectionPropertyName + textProperty + "' "
                + "value='" + text + "' placeholder='" + placeHolderText + "' "
                + "style='background-color: " + color + ";' readonly></input>"

                + "<input type='hidden' class='form-control dirtyData' "
                + "id='" + collectionProperty + "_" + i + "__" + descriptionProperty + "' "
                + "name='" + collectionPropertyName + descriptionProperty + "' "
                + "value='" + description + "' placeholder='" + placeHolderDescription + "' readonly></input>"
                + "</div>"

                + "<div class='form-control" + colorIcon + "' style='background-color: " + color + ";'>"
                + "<div class='row align-items-center'>"
                + "<div id=\"txt-" + tempIndex + "\" class='col'>"
                + spanToCollapse
                + "<span><strong>" + text + "</strong> - </span>"
                + "<span>" + description + "</span>"
                + "</div>"

                + "<div class='col'>"
                + "<div class='btn-group pull-right' role='group' aria-label='Buttons for edit tree'>"
                + "<a id='c_" + tempIndex + "' href='#' class='btn cloneButton'>"
                + "<i class='fa fa-copy fa-lg" + colorIcon + "'></i>"
                + "</a>"
                + "<a id='d_" + items[i].Content.Id + "' href='#' class='btn deleteButton'>"
                + "<i class='fa fa-trash-o fa-lg" + colorIcon + "'></i>"
                + "</a>"
                + "<a id='a_" + tempIndex + "' href='#' class='btn addButton'>"
                + "<i class='fa fa-plus fa-lg" + colorIcon + "'></i>"
                + "</a>"
                + "<a id='e_" + tempIndex + "' href='#' class='btn editButton'>"
                + "<i class='fa fa-pencil fa-lg" + colorIcon + "'></i>"
                + "</a>"
                + "</div>"
                + "</div>"

                + "</div>"
                + "</div>"
                + "</div>";

            var childs = "";

            if (containsChildren) {
                childs = renderBranch(childrens, level + 1, collectionPropertyName + collectionProperty, searchValue);
            }

            var rowWithChilds = "<a id='" + tempIndex + "'class='collpase collapsedItems" + " " + colorIcon + "' href='#childs_" + tempIndex + "' " +
                "data-toggle='collapse' data-parent='#childs_" + tempIndex + "' aria-expanded='false' aria-controls='childs_" + tempIndex + "'>" +
                tmpNodeHtml +
                getTextAddNewItem(tempIndex, color) +
                getInputEditItem(items[i], color) +
                "</a>";

            var rowWithoutChilds = tmpNodeHtml + getTextAddNewItem(tempIndex, color) + getInputEditItem(items[i], color);
            var rowToShow = containsChildren ? rowWithChilds : rowWithoutChilds;

            var structure =
                "<div id=\"row-" + tempIndex + "\" class='item'>" +
                rowToShow +
                "<div id='childs_" + tempIndex + "' class='collapse " + isCollapased + "' role='tabpanel' >" +
                childs +
                "</div>" +
                "</div>";

            html = html + structure;
        }

        return html;
    }

    function getInputsAdditionalValues(collectionPropertyName, item, index) {
        var result = '';
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var input = "<input type='hidden' class='form-control dirtyData' "
                    + " id='" + collectionProperty + "_" + index + "__" + additionalProperties[i].PropertyName + "'"
                    + " name='" + collectionPropertyName + additionalProperties[i].PropertyName + "'"
                    + " value='" + item.Content[additionalProperties[i].PropertyName] + "'></input>";
                result = result + input;
            }
        }
        return result;
    }

    function getInputEditItem(item, color) {
        var text = getText(item.Content.Name);
        var description = getText(item.Content.Description);
        var rowEditor =
            "<div class='form-row bg-light p-2 d-none editor w-100' id ='addEditItem_" + item.TemporalIndex + "' > "
            + "<div class='col-9'>"
            + "<div class='row'>"
            + "<div class='col-4'>"
            + "<input type='text' class='form-control dirtyData' "
            + "id='addEditItemName_" + item.TemporalIndex + "' value='" + text + "' "
            + "placeholder='" + placeHolderText + "'></input>"
            + "<span class='text-danger field-validation-error d-none' id='validationTextEdit_" + item.TemporalIndex + "'>" + validationTextMessage + "</span>"
            + "</div>"
            + "<div class='col-8'>"
            + "<input type='text' class='form-control dirtyData' "
            + "id='addEditItemDescription_" + item.TemporalIndex + "' value='" + description + "' "
            + "placeholder='" + placeHolderDescription + "'></input>"
            + "<span class='text-danger field-validation-error d-none' id='validationDescriptionEdit_" + item.TemporalIndex + "'>" + validationDescriptionMessage + "</span>"
            + "</div>"
            + "</div>"
            + "<div class='row'>"
            + getAdditionalValuesEdit(item)
            + "</div>"
            + "</div>"
            + "<div class='col-3 text-right'>"
            + "<div class=\"btn-group\" role =\"group\" aria-label=\"btns edit\">"
            + "<a id=\"" + item.TemporalIndex + "\" href=\"#\" class=\"btn btn-danger cancelButtonEdit\"><span>" + btnRemoveRow + "</span></a>"
            + "<a id=\"" + item.TemporalIndex + "\" href=\"#\" class=\"btn btn-success acceptButtonEdit\"><span>" + btnAddNewRow + "</span></a>"
            + "</div>"
            + "</div>"
            + "</div>";

        return rowEditor;
    }

    function getTextAddNewItem(itemId) {
        var input =
            "<div class='form-row bg-light p-2 d-none editor w-100' id='addNewItem_" + itemId + "'>"
            + "<div class='col-9'>"
            + "<div class='row'>"
            + "<div class='col-4'>"
            + "<input type='text' class=\"form-control dirtyData w-100\" "
            + "id='addNewItemName_" + itemId + "' "
            + "placeholder='" + placeHolderText + "'></input>"
            + "<span class='text-danger field-validation-error d-none' id='validationTextAdd_" + itemId + "'>" + validationTextMessage + "</span >"
            + "</div>"
            + "<div class='col-8'>"
            + "<input type=\"text\" class=\"form-control dirtyData w-100\" "
            + "id='addNewItemDescription_" + itemId + "' "
            + "placeholder='" + placeHolderDescription + "'></input>"
            + "<span class='text-danger field-validation-error d-none' id='validationDescriptionAdd_" + itemId + "'>" + validationDescriptionMessage + "</span >"
            + "</div>"
            + "</div>"
            + "<div class='row'>"
            + getAdditionalValuesAdd(itemId)
            + "</div>"
            + "</div>"
            + "<div class='col-3 text-right'>"
            + "<div class=\"btn-group\" role =\"group\" aria-label=\"btns edit\">"
            + "<a id=\"" + itemId + "\" href='#' class='btn btn-danger cancelButton'><span>" + btnRemoveRow + "</span></a>"
            + "<a id=\"" + itemId + "\" href='#' class='btn btn-success acceptButton'><span>" + btnAddNewRow + "</span></a>"
            + "</div>"
            + "</div>"
            + "</div>";

        return input;
    }

    function getAdditionalValuesAdd(parentId) {
        var result = '';
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var validationMesssage = additionalProperties[i].IsMandatory
                    ? "<span class='text-danger field-validation-error d-none' id='validation" +
                    additionalProperties[i].PropertyName + "Add_" + parentId + "'>" +
                    additionalProperties[i].MessageValidation + "</span >"
                    : "";

                var input =
                    "<div class='col-4 mt-2'>"
                    + "<input type='text' class=\"form-control dirtyData w-100\" "
                    + "id='addNewItem" + additionalProperties[i].PropertyName + "_" + parentId + "' "
                    + " placeholder='" + additionalProperties[i].PlaceHolder + "'></input>"
                    + validationMesssage
                    + "</div>";
                result = result + input;
            }
        }
        return result;
    }

    function getAdditionalValuesEdit(item) {
        var result = '';
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var validationMesssage = additionalProperties[i].IsMandatory
                    ? "<span class='text-danger field-validation-error d-none' id='validation" +
                    additionalProperties[i].PropertyName +
                    "edit_" +
                    item.TemporalIndex +
                    "'>" +
                    additionalProperties[i].MessageValidation +
                    "</span >"
                    : '';

                var input = "<div class='col-4 mt-2'>"
                    + "<input type='" + additionalProperties[i].Type + "' class='form-control dirtyData' "
                    + "id='addEditItem" + additionalProperties[i].PropertyName + "_" + item.TemporalIndex + "' "
                    + "placeholder='" + additionalProperties[i].PlaceHolder + "'></input>"
                    + validationMesssage
                    + "</div>";
                result = result + input;
            }
        }
        return result;
    }

    function onRemoving() {
        event.preventDefault();
        var cleanId = event.currentTarget.id;
        var idElem = cleanId.split('_').pop(-1);
        var action = cleanId.split('_', 1);
        if (canDeleteTreeLevelMethod !== undefined && app.common.validations.IsFunction(canDeleteTreeLevelMethod)) {
            canDeleteTreeLevelMethod(findElementOnTree(values, parseInt(idElem), action), removeItem);
        }
        else {
            removeItem(parseInt(idElem));
        }
    }

    function removeItem(temporalIndex) {
        var deleted = removeItemTree(values, temporalIndex);
        renderTree('#tree_' + instanceTree, values);
    }

    function onAdd() {
        event.preventDefault();
        var cleanId = event.currentTarget.id;
        var idElem = cleanId.split('_').pop(-1);
        changeBtnColor(cleanId);
        $('#addNewItem_' + idElem).removeClass('d-none');
    }

    function onEdit() {
        event.preventDefault();
        var itemId = event.currentTarget.id.split('_').pop(-1);
        changeBtnColor(event.currentTarget.id);
        $('#addEditItem_' + itemId).removeClass('d-none');

        var item = findElementOnTree(values, itemId);
        $('#addEditItemName_' + itemId).val(getText(item.Content.Name));
        $('#addEditItemDescription_' + itemId).val(getText(item.Content.Description));
        fillAdditionalPropertiesEdit(item, itemId);
    }

    function onClone() {
        event.preventDefault();
        var elem = event.currentTarget;
        var idBtn = elem.id.split('_').pop(-1);
        var parent = event.currentTarget.closest('#row-' + idBtn);
        var intParentId = parent.id.split('-').pop(-1);
        var item = findElementOnTree(values, intParentId);

        var newItem = clone(item, intParentId);

        addItemOnList(item.ParentId, values, newItem);
        renderTree('#tree_' + instanceTree, values);
    }

    function clone(item) {
        var guid = jsGuid(new Date().getTime());
        var newIndex = guid + lastIndex;

        var newItem = {
            Content: cloneContent(item, newIndex),
            IsCollapsed: true,
            TemporalIndex: newIndex,
            ParentId: item.ParentId,
            Clone: true
        };

        newItem.Content.Id = newIndex;
        newItem.Content.Childs = cloneChilds(item.Content.Childs, newIndex);
        newItem.Content.Name = newItem.Content.Name.concat(" - copy");
        return newItem;
    }

    function cloneContent(content, lastIndex) {
        var newItem = {
            Id: lastIndex,
            Name: content.Content.Name,
            Description: content.Content.Description || ""
        };

        fillAdditionalProperties(newItem, content);
        return newItem;
    }

    function fillAdditionalProperties(content, item) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                content[additionalProperties[i].PropertyName] = item[additionalProperties[i].PropertyName];
            }
        }
    }

    function cloneChilds(valuesToMap, lastIndex) {
        var tempValues = [];
        if (valuesToMap !== undefined && valuesToMap !== null) {
            for (var i = 0; i < valuesToMap.length; i++) {

                lastIndex = lastIndex + 1;
                var item = {
                    Content: cloneContent(valuesToMap[i], lastIndex),
                    IsCollapsed: true,
                    TemporalIndex: lastIndex,
                    ParentId: lastIndex - 1
                };
                item.Content.Id = lastIndex;
                item.Content.Childs = valuesToMap[i].Content.Childs.length > 0 ? cloneChilds(valuesToMap[i].Content.Childs, lastIndex) : [];
                tempValues.push(item);
            }
        }
        return tempValues;
    }

    function findElementOnTree(valuesOnFind, id, action) {
        for (var i = 0; i < valuesOnFind.length; i++) {

            if (valuesOnFind[i].TemporalIndex === parseInt(id) || action !== undefined && action.toString() === "d" && valuesOnFind[i].Content.Id === id) {
                return valuesOnFind[i];
            }
            else if (valuesOnFind[i].Content.Childs !== undefined &&
                valuesOnFind[i].Content.Childs !== null &&
                valuesOnFind[i].Content.Childs.length > 0) {
                var item = findElementOnTree(valuesOnFind[i].Content.Childs, id, action);
                if (item !== undefined) {
                    return item;
                }
            }
        }
    }

    function removeItemTree(valuesOnFind, id) {
        var result = false;
        for (var i = 0; i < valuesOnFind.length; i++) {
            if (valuesOnFind[i].TemporalIndex === parseInt(id)) {
                removeItemFrom(valuesOnFind, valuesOnFind[i]);
                return true;
            }
            else if (valuesOnFind[i].Content.Childs !== undefined && valuesOnFind[i].Content.Childs !== null &&
                valuesOnFind[i].Content.Childs.length > 0) {
                result = removeItemTree(valuesOnFind[i].Content.Childs, id);
                if (result === true) {
                    return result;
                }
            }
        }
        return result;
    }

    function colorLuminance(hex, lum) {
        var rgb = hexToRgb(hex);
        var a = parseInt(lum * 255, 10);
        return rgbToHex(rgb.r, rgb.g, rgb.b, lum);
    }

    function hexToRgb(hex) {
        var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);

        return result ? {
            r: parseInt(result[1], 16),
            g: parseInt(result[2], 16),
            b: parseInt(result[3], 16)
        } : null;
    }

    function rgbToHex(r, g, b, a) {
        return rgba = 'rgba(' + r + ', ' + g + ', ' + b + ', ' + a + ')';
    }

    function removeItemFrom(array, item) {
        var index = array.indexOf(item);
        if (index !== -1) {
            item = array[index];
            array.splice(index, 1);
        }
        return item;
    }

    function onAcceptAdd() {
        event.preventDefault();
        var itemId = parseInt(event.currentTarget.id.split('_').pop(-1));
        $('#validationTextAdd_' + itemId).addClass('d-none');
        $('#validationDescriptionAdd_' + itemId).addClass('d-none');
        removeAdditionalValidationsAdd(itemId);
        var name = $('#addNewItemName_' + itemId).val();
        var description = $('#addNewItemDescription_' + itemId).val();
        if (!app.common.validations.IsEmpty(name) && !app.common.validations.IsEmpty(description) && validateAdditionalProperties(itemId)) {
            var guid = jsGuid(new Date().getTime());
            lastIndex = guid + lastIndex;

            var newChild = {
                Content: {
                    Childs: [],
                    Id: lastIndex,
                    Name: name,
                    Description: description || ""
                },
                IsCollapsed: false,
                TemporalIndex: lastIndex,
                ParentId: itemId,
                Clone: true
            };

            fillItemAddWithAdditionalProperties(newChild, itemId);

            addItemOnList(itemId, values, newChild);

            closeFormAdd(itemId);
            clearSearcher();
            renderTree('#tree_' + instanceTree, values);
        } else {
            if (app.common.validations.IsEmpty(name)) {
                $('#validationTextAdd_' + itemId).removeClass('d-none');
            }

            if (app.common.validations.IsEmpty(description)) {
                $('#validationDescriptionAdd_' + itemId).removeClass('d-none');
            }
            showAdditionalValidationsOnAdd(itemId);
        }
    }

    function validateAdditionalPropertiesEdit(itemId) {
        var result = true;
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                if (additionalProperties[i].IsMandatory) {
                    var value = $('#addEditItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                    result = result && !app.common.validations.IsEmpty(value);
                }
            }
        }
        return result;
    }

    function validateAdditionalProperties(itemId) {
        var result = true;
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                if (additionalProperties[i].IsMandatory) {
                    var value = $('#addNewItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                    result = result && !app.common.validations.IsEmpty(value);
                }
            }
        }

        return result;
    }

    function showAdditionalValidationsEdit(itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = $('#addEditItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                if (additionalProperties[i].IsMandatory && app.common.validations.IsEmpty(value)) {
                    $('#validation' + additionalProperties[i].PropertyName + "edit_" + itemId).removeClass('d-none');
                }
            }
        }
    }

    function fillItemAddWithAdditionalProperties(item, itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = $('#addNewItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                item.Content[additionalProperties[i].PropertyName] = value;
            }
        }
    }

    function showAdditionalValidationsOnAdd(itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = $('#addNewItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                if (additionalProperties[i].IsMandatory && app.common.validations.IsEmpty(value)) {
                    $('#validation' + additionalProperties[i].PropertyName + "Add_" + itemId).removeClass('d-none');
                }
            }
        }
    }

    function removeAdditionalValidationsAdd(itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = $('#addNewItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                if (additionalProperties[i].IsMandatory && app.common.validations.IsEmpty(value)) {
                    $('#validation' + additionalProperties[i].PropertyName + "Add_" + itemId).addClass('d-none');
                }
            }
        }
    }

    function removeAdditionalValidationsEdit(itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = $('#addEditItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                if (additionalProperties[i].IsMandatory && app.common.validations.IsEmpty(value)) {
                    $('#validation' + additionalProperties[i].PropertyName + "edit_" + itemId).addClass('d-none');
                    $('#addEditItem' + additionalProperties[i].PropertyName + "_" + itemId).val('');
                }
            }
        }
    }

    function fillAdditionalPropertiesEdit(item, itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = item.Content[additionalProperties[i].PropertyName];
                $('#addEditItem' + additionalProperties[i].PropertyName + "_" + itemId).val(value);
            }
        }
    }

    function fillItemEditWithAdditionalProperties(item, itemId) {
        if (additionalProperties !== undefined && additionalProperties.length > 0) {
            for (var i = 0; i < additionalProperties.length; i++) {
                var value = $('#addEditItem' + additionalProperties[i].PropertyName + "_" + itemId).val();
                item.Content[additionalProperties[i].PropertyName] = value;
            }
        }
    }

    function addItemOnList(parentId, valuesOnFind, item) {
        var result = false;

        if (parseInt(parentId) === 0) {
            if (values === undefined || values === null) {
                values = [];
            }
            values.push(item);
            return true;
        }

        for (var i = 0; i < valuesOnFind.length; i++) {
            var rootLavel = valuesOnFind[i].Content;
            var childsElem = rootLavel.Childs;
            var indexTemp = valuesOnFind[i].TemporalIndex;
            var idParent = parseInt(parentId);

            if (indexTemp === idParent) {

                childsElem.push(item);
                return true;
            }
            else if (childsElem !== undefined && childsElem !== null && childsElem.length > 0) {
                if (indexTemp !== idParent) {
                    result = addItemOnList(idParent, childsElem, item);
                }

                if (result === true) {
                    return result;
                }
            }
        }
        return result;
    }

    function onCancelAdd() {
        event.preventDefault();
        var itemId = event.currentTarget.id;
        changeBtnColor(itemId);
        closeFormAdd(itemId);
    }

    function onCancelEdit() {
        event.preventDefault();
        var itemId = event.currentTarget.id;
        changeBtnColor(itemId);
        closeFormEdit(itemId);
    }

    function onAcceptEdit() {
        event.preventDefault();
        var itemId = event.currentTarget.id;
        $('#validationTextEdit_' + itemId).addClass('d-none');
        $('#validationDescriptionEdit_' + itemId).addClass('d-none');
        var name = $('#addEditItemName_' + itemId).val();
        var description = $('#addEditItemDescription_' + itemId).val();

        if (!app.common.validations.IsEmpty(name) && !app.common.validations.IsEmpty(description) && validateAdditionalPropertiesEdit(itemId)) {
            var item = findElementOnTree(values, itemId);
            item.Content.Name = name;
            item.Content.Description = description;

            fillItemEditWithAdditionalProperties(item, itemId);

            closeFormEdit(itemId);

            clearSearcher();

            renderTree('#tree_' + instanceTree, values);
        } else {
            if (app.common.validations.IsEmpty(name)) {
                $('#validationTextEdit_' + itemId).removeClass('d-none');
            }
            if (app.common.validations.IsEmpty(description)) {
                $('#validationDescriptionEdit_' + itemId).removeClass('d-none');
            }
            showAdditionalValidationsEdit(itemId);
        }
    }

    function closeFormAdd(id) {
        $('#addNewItem_' + id).addClass('d-none');
        $('#addNewItemName_' + id).val('');
        $('#addNewItemDescription_' + id).val('');
        $('#validationTextAdd_' + id).addClass('d-none');
        $('#validationDescriptionAdd_' + id).addClass('d-none');
        removeAdditionalValidationsAdd(id);
    }

    function closeFormEdit(id) {
        $('#addEditItem_' + id).addClass('d-none');
        $('#addEditItemName_' + id).val('');
        $('addEditItemDescription_' + id).val('');
        $('#validationTextEdit_' + id).addClass('d-none');
        $('#validationDescriptionEdit_' + id).addClass('d-none');
        removeAdditionalValidationsEdit(id);
    }

    function renderTree(instanceTree, items, searchValue) {
        $(instanceTree).empty();
        var code = renderBranch(items, 0, collectionProperty, searchValue);

        $(instanceTree).html(code);
        $(instanceTree).find('a[class~=deleteButton]').on('click', onRemoving);
        $(instanceTree).find('a[class~=addButton]').on('click', onAdd);
        $(instanceTree).find('a[class~=editButton]').on('click', onEdit);
        $(instanceTree).find('a[class~=acceptButton]').on('click', onAcceptAdd);
        $(instanceTree).find('a[class~=cloneButton]').on('click', onClone);
        $('div[id=addNewItem_0]').children().find('.acceptButton').on('click', onAcceptAdd);
        $('div[id=addNewItem_0]').children().find('.cancelButton').on('click', onCancelAdd);
        $(instanceTree).find('a[class~=cancelButton]').on('click', onCancelAdd);
        $(instanceTree).find('a[class~=acceptButtonEdit]').on('click', onAcceptEdit);
        $(instanceTree).find('a[class~=cancelButtonEdit]').on('click', onCancelEdit);
        $(instanceTree).find('a[class~=collapsedItems]').on('click', onCollapseItems);
    }

    function onCollapseItems() {
        var id = event.currentTarget.id;
        if (id !== undefined) {
            var item = findElementOnTree(values, id);
            if (item !== undefined && item !== null) {
                item.IsCollapsed = !item.IsCollapsed;
                event.currentTarget.children[0].children[0].classList.toggle('down');
            }
        }
    }

    function loadSkeleton(intanceTree) {
        var color = colorLuminance(baseColor, 0);
        const rowSearch =
            "<div class=\"form-inline bg-light p-3\">"
            + "<div class=\"form-group\">"
            + "<button class='" + settings.classes.buttonOk.add + " mr-5' id='btnAdd_" + instanceButtonAddRoot + "'>"
            + "<i class='fa fa-plus mr-1'></i>" + stringAddNew
            + "</button>"
            + searcher()
            + "</div>"
            + "</div>"
            + getTextAddNewItem(0, color)
            + "</div>"
            + "<div class=\"row pl-3 pr-3\">"
            + "<div class=\"col-12\">"
            + "<div id='tree_" + intanceTree + "' class=\"w-100\"></div>"
            + "</div>"
            + "</div>";

        $(container).html(rowSearch);
        $("#btnAdd_" + instanceButtonAddRoot).on("click", onRootAdd);
        $(container).find('a[class=acceptButton]').on('click', onAcceptAdd);
        $(container).find('a[class=cancelButton]').on('click', onCancelAdd);
        $('#btnSearch_' + instanceButtonSearcher).on('click', onSearch);
    }

    function onSearch() {
        event.preventDefault();
        var value = $('#text_' + instanceInputSearcher).val();
        if (value === undefined || app.common.validations.IsEmpty(value)) {
            value = '';
        }
        renderTree('#tree_' + instanceTree, values, value);
    }

    function clearSearcher() {
        $('#text_' + instanceInputSearcher).val('');
    }

    function searcher() {
        var blockSearch =
            "<div class=\"input-group\">"
            + "<input type='text' class='searcher form-control' id='text_" + instanceInputSearcher + "' "
            + "placeholder='" + settings.searchPlaceHolder + "'/>"
            + "<div>"
            + "<div class=\"input-group-append\">"
            + "<button type='button' id='btnSearch_" + instanceButtonSearcher + "' class=\"btn btn-info\">"
            + "<span class='fa fa-search fa-lg text-white'></span>"
            + "</button> "
            + "</div>"
            + "</div>";
        return blockSearch;
    }

    function onRootAdd() {
        event.preventDefault();
        closeOpenedEditors();
        $('#addNewItem_' + 0).removeClass('d-none');
    }

    function closeOpenedEditors() {
        $(container).find('div[class~=editor]').each(function () {
            if ($(this).is(':visible')) {
                $(this).addClass('d-none');
                return true;
            }
        });
    }

    function changeBtnColor(idBtn) {
        var btnEvent = event.currentTarget.id;
        var btnEventId = btnEvent.split('_').pop(-1);

        $(container).find('a[class~=btn-primary]').each(function () {
            var btnType = $(this).attr('id').includes('e_') || $(this).attr('id').includes('a_');
            if (btnType) {
                $(this).removeClass('btn-primary');
                return true;
            }
        });

        if (idBtn.includes('e_')) {
            $('#' + idBtn).addClass('btn-primary');
            $('#a_' + btnEventId).removeClass('btn-primary');
        } else {
            $('#' + btnEvent).addClass('btn-primary');
            $('#e_' + btnEventId).removeClass('btn-primary');
        }
        closeOpenedEditors();
    }

    var init = function (startContainer, options) {
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton(instanceTree);
        renderTree('#tree_' + instanceTree, values);
    };

    return {
        Init: init
    };
};