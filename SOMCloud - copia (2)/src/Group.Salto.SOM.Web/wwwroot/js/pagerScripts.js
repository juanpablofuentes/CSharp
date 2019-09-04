var pager = pager || {};
pager.PagerSelector = pager.PagerSelector || {};

pager.PagerSelector = (function () {
    var settings;
    var defaults = {
        page: 1
    };
    var page;
    $('#pager .dropdown-menu a').click(function () {
        $('.dropdown-toggle').html($(this).html() + '<div class="pull-right"> <span class="my-caret"></span></div>');
    });

    $('a[data-page-size]').click(function (evt) {
        evt.preventDefault();
        var size = $(this).attr('data-page-size');
        $("#pager-size").val(size);
        $("#filter-form").submit();
    });

    $('a[data-page]').click(function (evt) {
        evt.preventDefault();
        var page = $(this).attr('data-page');
        goToPage(page);
    });

    function goToNextPage() {
        goToPage(page + 1);
    }

    function goToPreviousPage() {
        goToPage(page - 1);
    }

    $("#search").click(function (evt) {
        page = 1;
        $("#pager-page").val(page);
    });

    function goToPage(page) {
        $("#pager-page").val(page);
        $("#filter-form").submit();
    }

    var init = function (options) {
        settings = $.extend({}, defaults, options);
        page = settings.PageData.Page;
    };

    $("#pager-next").click(function (evt) {
        evt.preventDefault();
        goToNextPage();
    });

    $("#pager-prev").click(function (evt) {
        evt.preventDefault();
        goToPreviousPage();
    });

    return {
        Init: init
    };
})();