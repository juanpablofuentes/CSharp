(function ($) {
    $(function () {
        $('.chevron').parent().on('click', toggleChevron);

        $('.accordion').on('hidden.bs.collapse', toggleChevron);
        $('.accordion').on('shown.bs.collapse', toggleChevron);
    });

})(jQuery);


function toggleChevron(e) {
    e.preventDefault();
    if (e.type === "hidden" || e.type === "shown") {
        $(e.target)
            .prev()
            .find("i.chevron")
            .toggleClass('down');
    }

    if (e.type === 'click') {
        var $elem = e.target;
        var btnCollapse = $elem.parentElement.parentElement.parentElement;
        if (btnCollapse.classList.contains('task-active')) {
            btnCollapse.classList.remove("task-active");
        }
    }
}