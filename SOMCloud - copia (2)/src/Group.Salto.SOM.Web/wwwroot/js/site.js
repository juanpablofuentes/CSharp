(function ($) {
    $(window).on("submit",
        function () {
            showSpinner();
        });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip();

        $('#workordersearch').on("click",
            function (e) {
                if ($("#SearchType").val() === "" || $("#SearchString").val() === "") {
                    e.preventDefault();
                    return false;
                }
                else
                    return true;
            });

        $('#search-box .dropdown-menu').find('a').on("click",
            function (e) {
                e.preventDefault();
                searchBoxSelect($(this));
            });

        checkSideBarMenu();
        checkAsideMenu();

        if ($("#side-menu").length > 0) {
            $("#side-menu").find('button').on("click", function () {
                if ($('body').hasClass('sidebar-lg-show')) {
                    $('#menu-burger').click();
                    reziseGrid('menu-burger');
                }
            });
        }

        sidebarMenuActiveItem();
    });

    function checkSideBarMenu() {
        if (app.common.ui.constants !== undefined) {
            var menuSideBar = app.common.ui.constants.SideBarMenuId;
            if ($(menuSideBar).length > 0 && $(menuSideBar).html().trim().length > 0) {
                $(app.common.ui.constants.SideMenuId).removeClass("d-none");
            }
        }
    }

    function checkAsideMenu() {
        var url = window.location.pathname;

        if (url.match(/WorkOrder/) || url.match(/People/)) {
            $('body').addClass('wo');
            $('#sideBarMenu').addClass('aside-menu-wo');
        } else {
            $('body').removeClass('wo');
            $('#sideBarMenu').removeClass('aside-menu-wo');
        }
    }

    function searchBoxSelect(elem) {
        var txtLabel = elem.html();
        elem.parent().children().each(function (k, v) {
            if (!jQuery.isEmptyObject($(this).data())) {
                $(this).removeData();
            }
        });

        elem.data("selected", true);
        $('.dropdown-toggle').html(txtLabel).addClass('bg-warning');
        setSearchType();
    }

    function setSearchType() {
        var workorder = $("#searchworkorder").data("selected");
        var active = $("#searchactive").data("selected");
        var location = $("#searchlocation").data("selected");

        if (workorder !== undefined && workorder === true) $("#SearchType").val(1);
        if (active !== undefined && active === true) $("#SearchType").val(2);
        if (location != undefined && location === true) $("#SearchType").val(3);
    }

})(jQuery);

function showSpinner() {
    $("#block-spinner").removeClass("d-none");
}

function hideSpinner() {
    $("#block-spinner").addClass("d-none");
}

function apiCall(url, typeMethod, dataType, data) {
    var settings = {
        "cache": false,
        "url": url,
        "method": typeMethod,
        "dataType": dataType,
        "data": data
    };

    return $.ajax(settings);
}

function navToUrl() {
    var url = $('#navUrlDocInput').val();
    if (url) {
        window.open(url, "_blank");
    }
}

function sidebarMenuActiveItem() {
    var url = window.location.pathname;
    var $itemMenu = "";

    $('.sidebar-nav ul li').find('a').each(function () {
        $itemMenu = $(this);
        var $parent = $itemMenu.parent('li.nav-item.nav-dropdown');
        if ($parent.hasClass('open')) {
            $parent.removeClass('open');
            if ($itemMenu.hasClass('active')) {
                $itemMenu.removeClass('active');
            }
        }
    });

    $('.sidebar-nav ul li').find('a').each(function () {
        $itemMenu = $(this);
        var $parent = $itemMenu.parents('li.nav-item.nav-dropdown');
        if ($itemMenu[0].pathname === url && !$itemMenu.hasClass('nav-dropdown-toggle')) {
            $parent.addClass('open');
            $itemMenu.addClass('active');
        }
    });
}

function uploadFileSelect(inputPictureId, mode, modalId) {

    var input_picture = $(inputPictureId);
    var preview = mode ? '#thumbnailPreview' : '#imagePreview';
    var image_picture = $(preview);
    var inputImgValue;

    if (preview === '#thumbnailPreview' && image_picture.attr('src').indexOf('nopic') === -1) {
        $('.upload--icons').removeClass('d-none');
    }

    input_picture.on('change', function () {
        $('.upload--icons').removeClass('d-none');
        inputImgValue = window.URL.createObjectURL(this.files[0]);
        image_picture.attr('src', inputImgValue);
    });

    image_picture.on('click', function () {
        input_picture.trigger('click');
    });

    $('#viewImg').on('click', function (e) {
        e.preventDefault();
        var imgSrc = inputImgValue === undefined ? $('#thumbnailPreview').attr('src') : inputImgValue;
        $('#imgInToModal').attr('src', imgSrc);

        $(modalId).modal();

    });
    $('#removeImg').on('click', function (e) {
        e.preventDefault();
        $('.upload--icons').addClass('d-none');
        $(preview).attr('src', '/images/nopic.png');
        $(input_picture).val('');
    });
}

function ExcelForm(url) {
    var excelForm = document.getElementById('excelForm');
    if (excelForm === null) {
        excelForm = document.createElement("Form");
        excelForm.action = url;
        excelForm.name = 'excelForm';
        excelForm.id = 'excelForm';
        excelForm.method = 'post';
        excelForm.target = 'hiddenDownloader';
        document.body.appendChild(excelForm);
    }
    return excelForm;
}

function GenerateExcelForm(url, form) {
    var excelForm = ExcelForm(url);
    excelForm.innerHTML = form;
    excelForm.submit();
    excelForm.innerHTML = '';
}

function GenerateExcelObject(url, excel) {
    var excelForm = ExcelForm(url);

    var inputParams = document.createElement("input");
    inputParams.type = "hidden";
    inputParams.id = 'filterParameterJSON';
    inputParams.name = 'filterParameterJSON';
    inputParams.value = JSON.stringify(excel);

    excelForm.appendChild(inputParams);
    excelForm.submit();
    excelForm.innerHTML = '';
}

function GetCultureForDatePicker(lang, withTime) {
    switch (lang) {
        case "es":
            return withTime === true ? "%d/%m/%Y %H:%i" : "%d/%m/%Y";
        case "ca":
            return withTime === true ? "%d/%m/%Y %H:%i" : "%d/%m/%Y";
        default:
            return withTime === true ? "%m.%d.%Y %H:%i" : "%m.%d.%Y";
    }
}

function showLoading(fl) {
    if (fl === true) {
        showSpinner();
    } else {
        hideSpinner();
    }
}

function reziseGrid(elem) {
    var burger = 'menu-burger', minimizer = 'sidebar-minimized';
    var sideBarBg = $('body').hasClass('sidebar-lg-show');
    var sideBarLt = $('body').hasClass(minimizer);
    var isWidth = $('.xhdr').parent().hasClass('w-100');

    if (!sideBarBg && !sideBarLt && elem === burger) {
        if (!isWidth) {
            $('.xhdr').parent().toggleClass('w-100');
            addBootstrapClass();
        }
    }

    if (sideBarBg && sideBarLt && elem === minimizer) {
        if (!isWidth) {
            $('.xhdr').parent().toggleClass('w-100');
            addBootstrapClass();
        }
    }

    if (!sideBarBg && sideBarLt && elem === burger) {
        if (!isWidth) {
            $('.xhdr').parent().toggleClass('w-100');
            addBootstrapClass();
        }
    }

    if (sideBarBg && !sideBarLt && elem === minimizer || sideBarBg && !sideBarLt && elem === burger) {
        if (!isWidth) {
            $('.xhdr').parent().toggleClass('w-100');
            addBootstrapClass();
        }
    }
}

function addBootstrapClass() {
    $.ajaxSetup({ cache: false });
    $('.xhdr').addClass('w-100');
    $('table.hdr').addClass('w-100');
    $('.objbox').addClass('w-100');
    $('table.obj').addClass('w-100');
}

function loadAjaxView(param, url, placeholder, success, showloading) {
    var completUrl = url + param;
    $.get(completUrl)
        .done(function (data) {
            $("#" + placeholder).html('');
            $("#" + placeholder).html(data);
            if (success !== undefined) {
                success(data);
            }
        }).fail(function (xhr, ajaxOptions, thrownError) {
            toastr.options.closeButton = 'False';
            toastr.options.newestOnTop = 'False';
            var optionsOverride = {};
            toastr['error']("Error", thrownError.message, optionsOverride);
        });
    if (showloading !== undefined)
        showLoading(showloading);
}

function renderSimpleModal(status, modalSize, title, msg) {
    var htmlModal = '<div id ="renderSimpleModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySimpleModal" aria-hidden="true">'
        + '<div class="modal-dialog modal-dialog-centered ' + status + ' ' + modalSize + '">'
        + '<div class="modal-content">'
        + ' <div class="modal-header">'
        + '   <h5 class="modal-title" id="simpleModal">' + title + '</h5>'
        + '   <button type="button" class="close" data-dismiss="modal" aria-label="Close">'
        + '        <span aria-hidden="true">&times;</span>'
        + '   </button>'
        + '</div >'
        + '<div class="modal-body">'
        + '<p>' + msg + '</p>'
        + ' </div>'
        + '    <div class="modal-footer">'
        + '        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>'
        + '    </div>'
        + '</div>'
        + '</div>'
        + '</div>';

    $('body').append(htmlModal);
    $('#renderSimpleModal').modal();
}

function concatenatedArrayValues(arr) {
    var arrayIdAndName = [];
    for (var i = 0; i < arr.length; i++) {

        var fullname = arr[i].surname !== null
            ? arr[i].secondSurname !== null ? arr[i].surname + " " + arr[i].secondSurname : arr[i].surname
            : arr[i].secondSurname !== null ? arr[i].secondSurname : "";

        var item = {
            id: arr[i].id,
            name: arr[i].name + " " + fullname
        };
        arrayIdAndName.push(item);
    }
    return arrayIdAndName;
}