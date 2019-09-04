var list = list || {};
list.ModulesSelector = list.ModulesSelector || {};

list.ModulesSelector = (function () {
    const cmb_crossed = '#cmbModulesCustomer';
    var settings;
    var elementsSelected = [];
    var defaults = {
        dataCmbModules: []
    };

    var removeModule = function (e) {
        var elementLi = $(this).closest('li');
        addOptionsToSelect(elementLi, cmb_crossed);
        elementLi.remove();
    };

    var addModule = function (e) {
        var selectedId = $(cmb_crossed).val();
        var selectedText = $(cmb_crossed + ' option:selected').text();
        if (selectedId !== null) {
            selectElement(selectedId, selectedText);
            $(cmb_crossed + ' option:selected').remove();
        }
    };

    function selectElement(selectedId, selectedText) {
        elementsSelected.push(selectedId);
        addLiElementToUlList(selectedId, selectedText);
    }

    function addLiElementToUlList(selectedId, selectedText) {
        $('ul.list-group').append("<li id='" + selectedId + "' class='list-group-item moduleElement'><span>" + selectedText
            + "</span><span class='fa fa-remove fa-lg text-danger mt-1 pull-right'></span></li>");
        $('.fa.fa-remove').off('click');
        $('.fa.fa-remove').click(removeModule);
    }

    function addOptionsToSelect(elementLi, cmb) {
        var id = elementLi.attr('id');
        if (removeElement(id)) {
            var aText = elementLi.find('span')[0];
            $(cmb).append($('<option>', {
                value: id,
                text: aText.textContent
            }));
        }
    }

    function removeElement(id) {
        var index = elementsSelected.indexOf(id);
        if (index >= 0) {
            elementsSelected.splice(index, 1);
            return true;
        }
        return false;
    }

    function loadModulesCombo() {
        var selectedModules = settings.dataCmbModules;
        var modules = settings.allModules;
        if (selectedModules !== null) {
            selectedModules.forEach(function (element) {
                var elementToAdd = findElement(modules, element.id);
                $(cmb_crossed).val(element.id);
                $(cmb_crossed + ' option:selected').remove();
                selectElement(element.id, elementToAdd.text);
            });
        }
    }

    function findElement(modules, id) {
        return modules.find(o => o.id === id);
    }

    function saveCustomer() {
        $('#ModuleIdsSelected').val(elementsSelected.join(","));
        $('#customerEditForm').submit();
    }

    function initializeEvents() {
        $('.fa.fa-remove').click(removeModule);
        $('#btn-add-modules').click(addModule);
        $('#btnSaveCustomer').click(saveCustomer);
    }

    var init = function (options) {
        settings = $.extend({}, defaults, options);
        loadModulesCombo();
        initializeEvents();
    };

    return {
        Init: init
    };
})();