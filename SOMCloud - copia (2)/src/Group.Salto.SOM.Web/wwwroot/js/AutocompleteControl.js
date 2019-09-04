var autocomplete = autocomplete || {};
autocomplete = function () {
    var defaults = {
        classes: {
            select: "form-control selecTable",
            header: {
                root: "",
                group: "form-group"
            },
            buttons: {
                add: ""
            },
            inputContainer: {
                css: ""
            },
            padding: {
                remove: "p-0"
            }
        }
    };

    var classes, container, settings, urlData, minimumCharacters = 0, searchKeyWord, minimumCharactersWord, instanceSearch, instanceInput, instanceSuggestions, originalList,
        filtredList, selectedItemProperty, selectedTextProperty, instanceList, additionalData, hasDefaultItem, nColumns = true, defaultItemContent, instanceCleanButton,
        selectedItem, showCleanButton = true, blackList = [], principalSelectorText = "", instancePrincipalSelector = "", instanceButton, getDataMethod, onVoidClean, voidValue, ajaxMethodType = "GET", isEnabled = true, removePadding = "";

    function addToInput(event) {
        var elem;
        if (event.target.localName === 'li') {
            elem = event.target.firstChild;
        }
        else {
            elem = event.target;
        }
        var item = {
            key: elem.id,
            value: elem.innerText
        };
        selectItem(item, false, true);
    }

    function selectItem(item, initialLoad, raiseEvent) {
        if (hasDefaultItem !== undefined && hasDefaultItem === true && item.value === defaultItemContent) {
            if (raiseEvent && settings.setEvent !== undefined) {
                settings.setEvent(null, initialLoad);
            }
            clean();
        }
        else {
            selectedItem = item;
            $("#cmb_" + instanceSearch).val(item.value);
            $("#inpt_" + instanceInput).val(item.key);
            $("#inptTxt_" + instanceInput).val(item.value);
            $("#lstElement_" + instanceList).each(function () {
                if (!$(this).hasClass("d-none")) {
                    $(this).addClass("d-none");
                }
            });
            $("#list_" + instanceSuggestions).addClass('d-none');
            if (raiseEvent && settings.setEvent !== undefined) {
                settings.setEvent(item, initialLoad);
            }
            if (showCleanButton) {
                $("#cleanButton_" + instanceCleanButton).removeClass("d-none");
            }
        }
    }

    function onGetDataFiltred(elements) {
        originalList = elements;
        filtredList = filterList(filtredList, originalList, searchKeyWord);
        showMatches(filtredList);
        if (originalList.length === 1) {
            selectItem(originalList[0], false, true);
        }
    }

    function filterList(target, source, filterText) {
        target = source;
        if (target !== undefined && target !== null && target.length > 0) {
            target = source.filter(function (itm) {
                if (itm.name !== undefined && itm.name !== null) {
                    if (itm.name.substring(0, filterText.length).toUpperCase() === filterText.toUpperCase()) {
                        return itm.name.toLowerCase();
                    }
                } else {
                    return itm.value.toLowerCase().includes(filterText);
                }
            });
        }
        return target;
    }

    function getData(data, url, success) {
        var dataToSend = getDataToSend(data);

        if (url !== undefined && url !== null) {
            $.ajax({
                url: url,
                type: ajaxMethodType,
                dataType: 'json',
                cache: false,
                data: dataToSend,
                success: success,
                error: function (xhr, ajaxOptions, thrownError) {
                    toastr.options.closeButton = 'False';
                    toastr.options.newestOnTop = 'False';
                    var optionsOverride = {};
                    toastr['error']("Error", thrownError.message, optionsOverride);
                }
            });
        }
    }

    function getDataToSend(data) {
        if (getDataMethod !== undefined && getDataMethod !== null && app.common.validations.IsFunction(getDataMethod)) {
            return getDataMethod(data, additionalData);
        } else {
            return defatultGetDataToSend(data);
        }
    }

    function defatultGetDataToSend(data) {
        if (additionalData !== undefined) {
            return { id: additionalData, text: data };
        } else {
            return { text: data };
        }
    }

    function removeSelectedOption() {
        $("#list_" + instanceSuggestions).addClass('d-none');
        $("#cmb_" + instanceSearch).val("");
        $("#inpt_" + instanceInput).val(voidValue);
        $("#inptTxt_" + instanceInput).val("");
        if (showCleanButton) {
            $("#cleanButton_" + instanceCleanButton).addClass("d-none");
        }
        selectedItem = undefined;
    }

    function clean() {
        searchKeyWord = undefined;
        minimumCharactersWord = undefined;
        originalList = [];
        filtredList = [];
        removeSelectedOption();
    }

    function displayMatches(event) {
        event.preventDefault();
        var name = event.currentTarget.value.toLowerCase();
        searhByName(name);
    }

    function searhByName(name) {
        if ((name === undefined || name === null || name === "") && minimumCharactersWord > 0) {
            clean();
            name = "";
        }

        if (onVoidClean !== undefined && onVoidClean === true) {
            if (name === undefined || name === null || name === "")
                cleanFilter();
        }

        if (searchKeyWord === name) {
            if (!$("#list_" + instanceSuggestions).is(':visible')) {
                searchKeyWord = name;
                showMatches(filtredList);
            }
        }
        else if (name.substring(0, minimumCharacters) === minimumCharactersWord) {
            searchKeyWord = name;
            filtredList = filterList(filtredList, originalList, searchKeyWord);
            showMatches(filtredList);
        }
        else if (name.length >= minimumCharacters) {
            searchKeyWord = name;
            minimumCharactersWord = searchKeyWord.substring(0, minimumCharacters);
            getData(minimumCharactersWord, urlData, onGetDataFiltred);
        }
    }

    function setInitialFilterValue(item) {
        if (item !== undefined && item !== null
            && item.key !== undefined && item.value !== undefined
            && item.key !== null && item.value !== null && item.value !== "null" && item.key !== 0) {
            selectItem(item, true, true);
        }
    }

    function setInitialValues() {
        urlData = settings.urlData;
        minimumCharacters = settings.minimumCharacters;
        instanceSearch = Math.random().toString().replace(".", "");
        instanceInput = Math.random().toString().replace(".", "");
        instanceSuggestions = Math.random().toString().replace(".", "");
        selectedItemProperty = settings.selectedItemProperty;
        selectedTextProperty = settings.selectedTextProperty;
        instanceList = Math.random().toString().replace(".", "");
        hasDefaultItem = settings.hasDefaultItem;
        nColumns = settings.nColumns;
        defaultItemContent = settings.defaultItemContent;
        instanceCleanButton = Math.random().toString().replace(".", "");
        instanceButton = Math.random().toString().replace(".", "");
        getDataMethod = settings.getDataMethod;
        ajaxMethodType = settings.ajaxMethodType;
        onVoidClean = settings.onVoidClean;
        voidValue = settings.voidValue;
        isEnabled = settings.isEnabled;

        if (settings.removePadding) {
            removePadding = defaults.classes.padding.remove;
        }

        if (settings.ajaxMethodType === undefined ||
            settings.ajaxMethodType === null ||
            settings.ajaxMethodType === '') {
            ajaxMethodType = app.constants.Get;
        }
        if (settings.showCleanButton !== undefined && settings.showCleanButton === false) {
            showCleanButton = false;
        }
        defaults.classes.inputContainer.css = nColumns === true ? "input-group" : "form-group";
        if (isEnabled === undefined) {
            isEnabled = true;
        }

        if (voidValue === undefined) {
            voidValue === 0;
        }
    }

    function loadSkeleton() {
        var cssInputContent = defaults.classes.inputContainer.css;
        var rowInput = "";
        var enabledText = '';
        if (!isEnabled)
            enabledText = "readonly";

        if (settings.nColumns) {
            rowInput =
                "<div class='" + cssInputContent + "'>"
                + "<div class='input-group " + cssInputContent + "'>"
                + "<label class='m-0' for='slct_" + instancePrincipalSelector + "'>" + principalSelectorText + "</label>"
                + "<div id='slct_" + instancePrincipalSelector + "' class='form-control p-0'></div>"
                + "<span class='input-group-append'>"
                + "<button class='" + settings.classes.buttons.add + "' id='btnAdd_" + instanceButton + "'>"
                + "<span class='fa fa-plus fa-lg text-primary'></span>"
                + "</button>"
                + "</span>"
                + "</div>"
                + "<ul id='list_" + instanceSuggestions + "' class='suggestions list-group d-none'></ul>"
                + "</div>";
        } else {
            rowInput =
                "<div class='row'>"
                + "<div class='col-12 " + removePadding + "'>"
                + "<input id='cmb_" + instanceSearch + "' class='custom-select search' autocomplete='off' " + enabledText + " />"
                + "<a href='#' class='inline-remove deleteButton d-none' id='cleanButton_" + instanceCleanButton + "'>"
                + "<span class='fa fa-remove text-secondary'></span>"
                + "</a>"
                + "<input type='hidden' value='0' id='inpt_" + instanceInput + "' name='" + selectedItemProperty + "' />"
                + "<input type='hidden' value='0' id='inptTxt_" + instanceInput + "' name='" + selectedTextProperty + "' />"
                + "<ul id='list_" + instanceSuggestions + "' class='suggestions list-group d-none'></ul>"
                + "</div>"
                + "</div>";
        }
        $(container).html(rowInput);
    }

    function cleanFilter() {
        clean();
        if (settings.setEvent !== undefined) {
            settings.setEvent(null);
        }
    }

    function showMatches(elements) {
        var defaultItems = [];
        setDefaultItem(defaultItems);

        if (elements !== undefined) {
            defaultItems.push(...elements);
        }

        $("#list_" + instanceSuggestions).empty();

        var i = 0;
        for (i = 0; i < defaultItems.length; i++) {
            if (defaultItems[i].key) {
                var found = existsElementInList(blackList, defaultItems[i].key);
                if (!found) {
                    var elementList = $("<li class='list-group-item'><span class='name list-group-item-data' id='" + defaultItems[i].key + "'>" + defaultItems[i].value + "</span></li>");
                    $("#list_" + instanceSuggestions).append(elementList);
                    elementList.on("click", addToInput);
                }
            }
            if (defaultItems[i].name) {
                let newArrTech = concatenatedArrayValues(defaultItems);
                let elementList = $("<li class='list-group-item'><span class='name list-group-item-data' id='" + newArrTech[i].id + "'>" + newArrTech[i].name + "</span></li>");
                $("#list_" + instanceSuggestions).append(elementList);
                elementList.on("click", addToInput);
            }
        }

        $("#list_" + instanceSuggestions).removeClass('d-none');
    }

    function existsElementInList(elements, key) {
        var found = elements.some(function (el) {
            return el.key == key;
        });
        return found;
    }

    function setDefaultItem(elements) {
        if (hasDefaultItem !== undefined && hasDefaultItem === true) {
            var item = {
                key: 0,
                value: defaultItemContent,
            };
            elements.push(item);
        }
    }

    function setEvents() {
        $("#cmb_" + instanceSearch).on("change", displayMatches);
        $("#cmb_" + instanceSearch).on("keyup", displayMatches);
        $("#cmb_" + instanceSearch).on("focus", displayMatches);
        $(document).on("click", hideList);
        $("#cleanButton_" + instanceCleanButton).on('click', cleanFilter);
    }

    function hideList(e) {
        if (!e.target.classList.contains('search') && !e.target.classList.contains('suggestions')) {
            if ($("#list_" + instanceSuggestions).is(':visible')) {
                $("#list_" + instanceSuggestions).addClass('d-none');
            }
        }
    }

    var init = function (startContainer, options) {
        classes = defaults.classes;
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton();
        setInitialFilterValue(settings.initValue);
        setEvents();
    };

    function changeData(data, needClean) {
        if (needClean) {
            clean();
        }
        additionalData = data;
    }

    function getSelectedOption() {
        return selectedItem;
    }

    function updateBlackList(elements) {
        blackList = elements;
        if (blackList === undefined) {
            blackList = [];
        }
    }

    function getFilterText() {
        return minimumCharactersWord;
    }

    function enabled(value) {
        $("#cmb_" + instanceSearch).prop('readonly', value);
    }

    function searchByDefault() {
        getData(null, urlData, onGetDataFiltred);
    }

    return {
        Init: init,
        ChangeData: changeData,
        Clean: clean,
        GetSelectedOption: getSelectedOption,
        RemoveSelectedOption: removeSelectedOption,
        UpdateBlackList: updateBlackList,
        GetFilterText: getFilterText,
        Enabled: enabled,
        SearchByDefault: searchByDefault
    };
};