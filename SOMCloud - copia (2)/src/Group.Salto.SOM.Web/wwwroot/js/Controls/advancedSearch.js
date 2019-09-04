var advancedSearch = advancedSearch || {};
advancedSearch = function () {

    var options;
    var resultData;
    var minimumCharacters = 0;
    var searchKeyWord = '';
    var additionalData;

    var init = function (_options) {
        options = _options;
        initializeEvents();
        setInitialValues(options);
    };

    $('#AdvancedSearchModal').on('hidden.bs.modal', function () {
        $('#AdvancedSearchModalAdd').off('click', onAddClick);
        $('#AdvancedSearchModalChoose').off('click', onChooseClick);
        $('#searchinput').off('change', onSearchinputChange);
        $('#searchinput').off('keyup', onSearchinputChange);
        $('#SelectDataMultiple').off('change', onSelectDataMultipleChange);
    })

    function initializeEvents() {
        $('#AdvancedSearchModalAdd').on('click', onAddClick);
        $('#AdvancedSearchModalChoose').on('click', onChooseClick);
        $('#searchinput').on('change', onSearchinputChange);
        $('#searchinput').on('keyup', onSearchinputChange);
        $('#SelectDataMultiple').on('change', onSelectDataMultipleChange);
    }

    function onSearchinputChange(event) {
        event.preventDefault();
        var name = event.currentTarget.value.toLowerCase();
        searhByName(name);
    }

    function searhByName(name) {
        $("#infoData").html('');

        var type = $("#SelectType").val();
        var additional = null;
        if (additionalData !== undefined) {
            additional = additionalData;
        }
        if (searchKeyWord !== name && name.length >= minimumCharacters) {
            searchKeyWord = name;
            getData(type, name, additional, options.advancedSearchUrl, searchResultRequest);
        }
        else if (name.length == 0) {
            searchKeyWord = '';
            $('#SelectDataMultiple').empty();
        }
    }

    function searchResultRequest(result) {
        resultData = result;
        app.common.ui.LoadSelectorKeyValue("SelectDataMultiple", result, "id", "name", false);
    }

    function onSelectDataMultipleChange(sel) {
        var index = sel.currentTarget.selectedIndex;
        var resultDetail = resultData[index];

        if (options.onAdvancedSearchSelectChange !== undefined) {
            options.onAdvancedSearchSelectChange(resultDetail);
        }

        var dl = '';
        for (var i = 0; i < options.detail.length; i++) {
            dl += '<dt class="col-sm-3 p-0">' + options.detail[i] + '</dt >';
            dl += '<dd class="col-sm-9 p-0">' + resultDetail.details[i] + '</dt >';
        }
        $("#infoData").html(dl);
    }

    function setInitialValues(options) {
        searchKeyWord = '';
        $('#SelectType').empty();
        if (options.showSelectValues === undefined)
            $("#divSelectType").addClass('d-none');
        else {
            app.common.ui.LoadSelectorKeyValue('SelectType', options.showSelectValues, "key", "value", false);
            $("#divSelectType").removeClass('d-none');
        }

        if (options.urlAdd === undefined) {
            $("#AdvancedSearchModalAdd").addClass('d-none');
        }
        else {
            $("#AdvancedSearchModalAdd").removeClass('d-none');
        }
        minimumCharacters = options.minimumCharacters;
        $("#headerTitle").text(options.headerTitle);
        $("#infoData").html('');
        $('#searchinput').val('');
        $('#SelectDataMultiple').empty();
    }

    function onAddClick() {
        location.href = options.urlAdd;
    }

    function onChooseClick() {
        var index = -1;
        index = $('#SelectDataMultiple')[0].selectedIndex;

        if (index != -1) {
            var resultDetail = resultData[index];

            if (options.setEvent !== undefined) {
                options.setEvent(resultDetail);
            }
            $("#AdvancedSearchModalCancel").click();
        }
    }

    var getData = function (type, searchText, secundariText, url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            async: false,
            data: { SearchType: type, Value: searchText, Text: secundariText },
            success: success,
            error: function (xhr, ajaxOptions, thrownError) {
                toastr.options.closeButton = 'False';
                toastr.options.newestOnTop = 'False';
                var optionsOverride = {};
                toastr['error']("Error", thrownError.message, optionsOverride);
            }
        });
    };

    function changeData(data) {
        additionalData = data;
    }

    return {
        Init: init,
        ChangeData: changeData,
    };
};