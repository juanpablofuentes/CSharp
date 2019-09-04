var app = app || {};
app.peoplecalendar = app.peoplecalendar || {};

app.peoplecalendar = (function () {
    var _settings;

    var init = function (settings) {
        _settings = settings;
        loadAccordion(_settings.data);
    };

    function loadAccordion(data) {
        var itemsCalendar = [];
        $("#calendarAccordion").html('');


        var calendarAccordion = new dhtmlXAccordion({
            parent: "calendarAccordion"
        });

        calendarAccordion.enableMultiMode("auto", "350");

        $.each(data, function (idx, cal) {
            $("#calendarAccordionContainer").append("<div id='events" + cal.id + "' class='events'></div>");

            var text = "<div class='cell_hdr_text bg-light'>" +
                "<div class='cell_hdr_icons'><span class='som-colored-square' style='background-color:" + cal.color + ";'></span>" +
                "<i class='fa fa-eye fa-lg visiblecalendar' data-calid='" + cal.id + "'></i>" +
                "<a data-toggle='modal' data-target='#CalendarDeletePeopleModal'><i class='fa fa-trash-o fa-lg deletecalendar' data-calid='" + cal.id + "'></i></a></div>" +
                "<div class='cell_hdr_title'><div class='call_hdr_name'>" + cal.name + "</div></div></div > ";
            calendarAccordion.addItem(cal.id, text, false);
            calendarAccordion._closeItem(cal.id);
            calendarAccordion.cells(cal.id).setHeight('*');
        });

        var eventTemplate = "<div data-eventid='#eventid#' data-start-date='#date#' id='event#eventid#' class='calendarevent contextual-event'>" +
            "<div class='som-colored-square' style='background-color:#color#;'></div>" +
            "<div id='nameEvent'>#name#</div>" +
            "</div>";

        $.each(data, function (idx, cal) {
            var htmlPriority = "<div class='p-2'>" +
                "<div class='input-group'><div class='input-group-prepend'><span class='input-group-text'>Priority</span></div><input type='number' data-calid='" + cal.id + "' class='form-control calendar_priority_textbox input-mini' value='" + cal.priority + "' onchange='app.Scheduler.Savepriority(this)'></div></div>";
            calendarAccordion.cells(cal.id).attachHTMLString(htmlPriority);

            var titleEvent = "<strong><em>Events</em></strong>";
            var eventAccordion = new dhtmlXAccordion({
                parent: 'events' + cal.id,
                items: [{ id: "" + cal.id + "", text: titleEvent, height: "*" }]
            });

            eventAccordion.enableMultiMode();

            calendarAccordion.cells("" + cal.id + "").appendObject("events" + cal.id);

            eventAccordion.cells(cal.id).attachList({
                type: {
                    template: eventTemplate,
                    padding: 5,
                    width: 350
                }
            });

            var eventsList = [];
            $.each(cal.events, function (idx, e) {
                var eventItem = { eventid: e.id, name: e.text, color: e.color, date: e.start_date };
                eventsList.push(eventItem);
            });
            eventAccordion.cells(cal.id).dataObj.parse(eventsList, "json");
        });
    }

    return {
        Init: init
    };
})();