var performTask = performTask || {};
performTask = function () {

    var defaults = {
        classes: {
            select: "form-control selecTable",
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

    var workOrderId, supplantTechnicianList, selectedSupplantTechnician, changes, technicians, htmlContainer, newArrTech = [], idTechnicianList, body, row;

    function setInitialValues() {
        if (settings.arrTypes !== undefined && settings.arrTypes !== null) {
            changes = settings.arrTypes.changes;
            workOrderId = settings.woId;

            if (settings.arrTypes.technicians !== null) {
                newArrTech = concatenatedArrayValues(settings.arrTypes.technicians);
                technicians = newArrTech;
            }

            loadSkeleton();
        }
        if (settings.supplantTechnicianList !== undefined && settings.supplantTechnicianList !== null) {
            settings = settings.supplantTechnicianList;
            supplantTechnicianAutocomplete();
        }
    }

    function loadSkeleton() {
        if (changes !== null) {
            let options = {
                trigger: 'changes',
                cardActualHead: 'Actual',
                cardChangeToHead: 'Change to',
                idFrom: changes.from.id,
                nameFrom: changes.from.name,
                idTo: changes.to.id,
                nameTo: changes.to.name,
                color: changes.color
            };
            htmlContainer = modalHtmlConstructor(options);
            $(container).append(htmlContainer);
        } else {

            var containerList = 'autocomplete';
            let options = {
                itemCollection: newArrTech,
                selectControlId: 'technicianList',
                placeholder: 'Select select a technician...',
                withoutButton: false
            };
            var optConstructor = {
                trigger: 'tech',
                cardActualHead: 'Actual',
                cardChangeToHead: 'Change to',
                actualTech: settings.assignationTechnician,
                containerAutocomplete: containerList,
                color: ''
            };

            htmlContainer = modalHtmlConstructor(optConstructor);
            $(container).append(htmlContainer);

            initializeAutocomplete(containerList, options);
        }

        $('#perfomTask').modal();
        $('#perfomTask').modal('handleUpdate');
    }

    function modalHtmlConstructor(opt) {
        var row;
        if (opt.trigger === 'changes') {
            var inputHidden = '<input type="hidden" name="' + opt.idTo + '">';

            let htmlFrom = '<div class="card bg-light mb-3 text-center" style="max-width: 18rem;">'
                + '<div class="card-header">' + opt.cardActualHead + '</div>'
                + ' <div class="card-body">'
                + ' <h5 class="card-title">' + opt.nameFrom + '</h5>'
                + ' </div>'
                + '</div>';

            let htmlTo = '<div class="card bg-light mb-3 text-center" style="max-width: 18rem;background-color: ' + opt.color + '!important" >'
                + ' <div class="card-header" style="background-color: ' + opt.color + '!important">' + opt.cardChangeToHead + '</div>'
                + ' <div class="card-body">'
                + ' <h5 class="card-title">' + opt.nameTo + '</h5>'
                + ' </div>'
                + '</div>';
            let htmlSeparation = '<div class="text-center"><i class="fa fa-arrow-right fa-4x text-secondary"></i></div>';
            var columns = '<div class="col">' + htmlFrom + '</div><div class="col">' + htmlSeparation + '</div><div class="col">' + htmlTo + '</div>';
            row = '<div class="form-row align-items-center">' + inputHidden + columns + '</div>';
        }

        if (opt.trigger === 'tech') {
            let htmlContainer = '<div id="' + opt.containerAutocomplete + '"></div>';

            let htmlFrom = '<div class="card bg-light mb-3 text-center" style="max-width: 18rem;">'
                + '<div class="card-header">' + opt.cardActualHead + '</div>'
                + ' <div class="card-body">'
                + ' <h5 class="card-title">' + opt.actualTech + '</h5>'
                + ' </div>'
                + '</div>';

            let htmlSeparation = '<div class="text-center"><i class="fa fa-arrow-right fa-4x text-secondary"></i></div>';

            let htmlTo = '<div class="card bg-light mb-3 text-center" style="max-width: 18rem;background-color: ' + opt.color + '!important" >'
                + ' <div class="card-header" style="background-color: ' + opt.color + '!important">' + opt.cardChangeToHead + '</div>'
                + ' <div class="card-body">'
                + ' <h5 class="card-title">' + htmlContainer + '</h5>'

                + ' <div class="form-group row">'
                + ' <div class="col">'
                + ' <div class="custom-control custom-checkbox">'
                + ' <input asp-for="Customer.IsActive" type="checkbox" class="custom-control-input" id="customCheck1" />'
                + ' <label class="custom-control-label" for="customCheck1"><span>Técnico fijado</span></label>'
                + ' </div>'
                + ' </div>'
                + ' </div>'
                + ' </div>'
                + '</div>';

            row = '<div class="form-row align-items-center"><div class="col-4">' + htmlFrom + '</div><div class="col">' + htmlSeparation + '</div><div class="col-4">' + htmlTo + '</div></div>';
        }
        return row;
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

    function correctContrast(elem, color, zona) {
        app.contrastColor.Init(
            {
                callFrom: zona,
                parentElem: '#' + elem,
                elemChild: '',
                childColor: color,
                defaulClassColor: options.color.defaultClassColor
            });
    }

    function getWOInfo(url, action, dataType, idWO) {
        return apiCall(url, action, dataType, { id: idWO });
    }

    function supplantTechnicianAutocomplete(options) {
        let supplantTechnician = new autocomplete();
        supplantTechnician.Init(container,
            {
                hasDefaultItem: false,
                urlData: app.config.Urls.getSupplantTechnician,
                minimumCharacters: 1,
                nColumns: false,
                getDataMethod: 'json',
                ajaxMethodType: app.constants.Get,
                onVoidClean: true,
                voidValue: 0
            });
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