var autocompleteJson = autocompleteJson || {};
autocompleteJson = function () {

    var container, selectControlId, placeholder, buttonId, buttonCssClass, buttonText, withoutButton, inputElement, json;
    var defaults = {
        selectedItems: [],
        classes: {
            select: "form-control",
            header: {
                root: "",
                group: "form-group"
            },
            buttons: {
                add: "btn btn-success mb-3 text-white",
                search: ""
            }
        }
    };

    function setInitialValues() {
        inputElement = settings.selectControlId;
        json = settings.arr;
        selectControlId = inputElement;
        placeholder = settings.placeholder;
        buttonId = settings.buttonId || '';
        buttonCssClass = settings.buttonCssClass || '';
        buttonText = settings.buttonText || '';
        withoutButton = settings.withoutButton;
    }

    function renderHtmlSelectControl() {
        var htmlSelect =
            '<input id="' + selectControlId + '" type="text" name="' + selectControlId + '" placeholder="' + placeholder + '">';

        var htmlButton = + '<div class="input-group-append">'
            + '<button id="' + buttonId + '" class="' + buttonCssClass + '" type="button">' + buttonText + '</button>'
            + '</div>';

        var htmlContainer = '<div class="input-group">' + htmlSelect + '</div >';
        var htmlContainerWithButton = '<div class="input-group">' + htmlSelect + htmlButton + '</div >';

        return html = withoutButton ? htmlContainerWithButton : htmlContainer;
    }

    function loadSkeleton() {
        var selectControlRow = renderHtmlSelectControl();
        $(container).append(selectControlRow);
        autocomplete(document.getElementById(inputElement), json);
    }

    function autocomplete(inputElement, json) {
        var currentFocus;
        inputElement.addEventListener("input", function (e) {
            var containerList, item, inputValue = this.value;
            closeAllLists();
            if (!inputValue) { return false; }
            currentFocus = -1;
            containerList = document.createElement("DIV");
            containerList.setAttribute("id", this.id + "autocomplete-list");
            containerList.setAttribute("class", "autocomplete-items");
            this.parentNode.appendChild(containerList);
            for (var i = 0; i < json.length; i++) {
                if (json[i].name.substr(0, inputValue.length).toUpperCase() === inputValue.toUpperCase()) {
                    item = document.createElement("DIV");
                    item.innerHTML = "<strong>" + json[i].name.substr(0, inputValue.length) + "</strong>";
                    item.innerHTML += json[i].name.substr(inputValue.length);
                    item.innerHTML += "<input type='hidden' value='" + json[i].name + "'>";
                    item.addEventListener("click", function (e) {
                        inputElement.value = this.getElementsByTagName("input")[0].value;
                        closeAllLists();
                    });
                    containerList.appendChild(item);
                }
            }
        });

        inputElement.addEventListener("keydown", function (e) {
            let containerList = document.getElementById(this.id + "autocomplete-list");
            if (containerList) {
                containerList = containerList.getElementsByTagName("div");
            }
            if (e.keyCode === 40) {
                currentFocus++;
                addActive(containerList);
            } else if (e.keyCode === 38) {
                currentFocus--;
                addActive(containerList);
            } else if (e.keyCode === 13) {
                e.preventDefault();
                if (currentFocus > -1) {
                    if (containerList) containerList[currentFocus].click();
                }
            }
        });

        function addActive(containerList) {
            if (!containerList) {
                return false;
            }
            removeActive(containerList);
            if (currentFocus >= containerList.length) { currentFocus = 0; }
            if (currentFocus < 0) { currentFocus = containerList.length - 1; }
            containerList[currentFocus].classList.add("autocomplete-active");
        }

        function removeActive(containerList) {
            for (var i = 0; i < containerList.length; i++) {
                containerList[i].classList.remove("autocomplete-active");
            }
        }

        function closeAllLists(allLists) {
            let containerList = document.getElementsByClassName("autocomplete-items");
            for (var i = 0; i < containerList.length; i++) {
                if (allLists !== containerList[i] && allLists !== inputElement) {
                    containerList[i].parentNode.removeChild(containerList[i]);
                }
            }
        }

        document.addEventListener("click", function (e) {
            closeAllLists(e.target);
        });
    }

    var init = function (startContainer, options) {
        container = startContainer;
        settings = $.extend({}, defaults, options);
        setInitialValues();
        loadSkeleton();
    };

    return {
        Init: init
    };
};