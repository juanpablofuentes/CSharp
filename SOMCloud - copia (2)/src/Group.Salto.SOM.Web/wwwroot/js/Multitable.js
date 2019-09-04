function MultiTableScript(className) {
    $("input[type='checkbox'][data-select-all='true']").click(
        function (evt) {
            var isChecked = $(this).is(':checked');
            var allChecks =
                $("input[type='checkbox'][data-select-all='false']");
            allChecks.prop('checked', isChecked);
            var evt = document.createEvent('Event');
            evt.initEvent('change', true, true);
            document.getElementsByClassName(className).dispatchEvent(evt);
        });

    $("a[data-order]").click(function (evt) {
        evt.preventDefault();
        var op = $("#order-property");
        var prev = op.val();
        var current = $(this).attr("data-order");
        if (prev == current) {
            var asc = $("#order-asc").val();
            $("#order-asc").val(asc === "true" ? "false" : "true");
        }
        op.val(current);
        $("#filter-form").submit();
    });
};