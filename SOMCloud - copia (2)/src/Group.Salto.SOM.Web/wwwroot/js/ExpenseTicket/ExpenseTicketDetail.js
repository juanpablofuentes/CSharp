var app = app || {};
app.expenseticket = app.expenseticket || {};
app.expenseticket.detail = app.expenseticket.detail || {};

app.expenseticket.detail = (function () {

    var init = function (options) {
        initializePeopleCombo(options.peopleCombo);
        initializeStatesCombo(options.statesCombo);
    };

    function initializePeopleCombo(peopleOptions) {
        var peopleCombo = new autoCompleteListSelector();
        peopleCombo.Init("#peopleContainer",
            {
                selectedItems: peopleOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getPeopleExpense,
                collectionProperty: 'NamePeople',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                showId: false,
                minimumCharacters: 0,
                column1Text: peopleOptions.column1Text,
                column2Text: peopleOptions.column2Text,
                getDataMethod: peopleOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return peopleCombo;
    }

    function initializeStatesCombo(statesOptions) {
        var statesCombo = new autoCompleteListSelector();
        statesCombo.Init("#statesContainer",
            {
                selectedItems: statesOptions.selectedItems,
                urlPrincipalCombo: app.config.Urls.getStates,
                collectionProperty: 'States',
                itemIdProperty: 'Value',
                itemTextProperty: 'Text',
                itemIdSecondaryProperty: 'ValueSecondary',
                itemTextSecondaryProperty: 'TextSecondary',
                showId: false,
                minimumCharacters: 0,
                column1Text: statesOptions.column1Text,
                column2Text: statesOptions.column2Text,
                getDataMethod: statesOptions.getDataMethod,
                ajaxMethodType: app.constants.Post
            });
        return statesCombo;
    }

    var contextualMenu = function (options) {
        const tableBloc = document.getElementById(options.container);
        let menuVisible = false;
        var menu, menuClass = options.menuClass, idTicket = 0, statusDataset = "";

        const toggleMenu = command => {
            if (command === "show") {
                $(menu).removeClass('d-none');
            } else {
                $(menu).addClass('d-none');
            }
            menuVisible = !$(menu).hasClass('d-none');
        };

        const setPosition = ({ top, left }) => {
            var styles = {
                left: `${left - 220}px`,
                top: `${top - 200}px`
            };
            menu.css(styles);
            toggleMenu('show');
        };

        window.addEventListener("click", e => {
            if (menuVisible) { toggleMenu("hide"); }
        });

        tableBloc.addEventListener("contextmenu", e => {
            e.preventDefault();
            e.stopPropagation();

            $('#' + options.container).find(menuClass).each(function () {
                if (menuVisible) { toggleMenu("hide"); }
            });
            if (e.target.tagName === 'A') { return false; }

            var trTarget = $(e.target).closest('tr');

            menu = $(trTarget).find(menuClass);

            const origin = {
                left: e.pageX,
                top: trTarget.offset().top
            };

            setPosition(origin);

            var children = $(menu).children();
            var lastTdTrTarget = $(trTarget).children()[$(trTarget).children().length - 1];

            statusDataset = $(menu).data('status');
            idTicket = $(lastTdTrTarget).find('.col-update').data('expenseticket');

            document.getElementById('expense-ticket-id').setAttribute('value', idTicket);

            for (var i = 0; i < children.length; i++) {
                if (children[i].dataset.target === undefined) {
                    continue;
                }
                if (options.status.accepted === statusDataset) {
                    if (children[i].dataset.target === '#PaidModal') {
                        children[i].classList.remove('d-none');
                    }
                    if (children[i].dataset.target === '#FinishedModal') {
                        children[i].classList.remove('d-none');
                    }
                } else if (options.status.escaled === statusDataset) {
                    if (children[i].dataset.target === '#AcceptedModal') {
                        children[i].classList.remove('d-none');
                    }
                    if (children[i].dataset.target === '#RejectedModal') {
                        children[i].classList.remove('d-none');
                    }
                } else {
                    if (options.status.pending === statusDataset) {
                        if (children[i].dataset.target === '#AcceptedModal') {
                            children[i].classList.remove('d-none');
                        }
                        if (children[i].dataset.target === '#RejectedModal') {
                            children[i].classList.remove('d-none');
                        }
                    }
                }
            }

            return false;
        });

        $('.' + options.listItems).on('click', function (elem) {
            elem.preventDefault();
            var type = elem.target.getAttribute('data-target');

            $(menu).addClass('d-none');
            if (type === '#AcceptedModal') {
                loadAjaxView('?id=' + idTicket + '&status=' + statusDataset + "|AcceptedModal", options.GetExpense, 'modalcontaineraccepted');
            }
            else if (type === '#PaidModal') {
                loadAjaxView('?id=' + idTicket + '&status=' + statusDataset + "|PaidModal", options.GetExpense, 'modalcontainerpaid');
            } else if (type === '#EscaledModal') {
                loadAjaxView('?id=' + idTicket + '&status=' + statusDataset + "|EscaledModal", options.GetExpense, 'modalcontainerescaled');
            } else if (type === '#RejectedModal') {
                loadAjaxView('?id=' + idTicket + '&status=' + statusDataset + "|RejectedModal", options.GetExpense, 'modalcontainerrejected');
            } else if (type === '#FinishedModal') {
                loadAjaxView('?id=' + idTicket + '&status=' + statusDataset + "|FinishedModal", options.GetExpense, 'modalcontainerfinished');
            } else {
                if (type === '#PendingModal') {
                    loadAjaxView('?id=' + idTicket + '&status=' + statusDataset + "|PendingModal", options.GetExpense, 'modalcontainerpending');
                }
            }
        });
    };

    return {
        Init: init,
        ContextualMenu: contextualMenu
    };
})();