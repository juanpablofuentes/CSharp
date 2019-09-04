var app = app || {};
app.common.ui = app.common.ui || {};
app.Scheduler = app.Scheduler || {};
app.calendar = app.calendar || {};

app.Scheduler = (function () {
    var _settings;
    var dataCalendar = [];
    var filters = {};

    var init = function (settings) {
        _settings = settings;
        initScheduler();
        scheduler.init("SomScheduler", new Date(), "year");
        refreshData();
        loadCalendarName();
        loadCalendarEventCategories();
        loadAvariableCalendars();

        $('#menu-burger').on('click', function () {
            scheduler.init("SomScheduler", new Date(), "year");
            $('#SomScheduler').animate(
                { 'right': '0px', 'position': 'absolute' },
                function () {
                    scheduler.updateView();
                }
            );
        });
        $('.sidebar-minimizer').on('click', function () {
            scheduler.init("SomScheduler", new Date(), "year");
            $('#SomScheduler').animate(
                { 'right': '0px', 'position': 'absolute' },
                function () {
                    scheduler.updateView();
                }
            );
        });
    };

    function refreshData() {
        showSpinner();
        getdata(_settings.id, _settings.url, loadScheduler);
        hideSpinner();
    }

    function getdata(data, url, success) {
        $.ajax({
            url: url,
            type: 'GET',
            dataType: 'json',
            cache: false,
            async: false,
            data: { id: data },
            success: success,
            error: function (result) {

            }
        });
    }

    function loadScheduler(result) {
        dataCalendar = result;

        $.each(result, normalizeStartDate);

        if (_settings.initCalendar) {
            var optionsCalendar = {
                peopleId: _settings.id,
                data: result
            };
            app.calendar.Init(optionsCalendar);

            $(document).off("click", "i.visiblecalendar", onClickFilterCalendar);
            $(document).off("click", "i.deletecalendar", onClickModalDeleteCalendar);
            $(document).off("click", "#DeleteCalendarModalSave", onClickDeleteCalendar);
            $(document).off("click", "div.calendarevent", onClickEvents);
            $(document).off("dblclick", "div.calendarevent", onDoubleClickEvents);
            $(document).off("click", "#AddCalendarConfirmSave", onClickAddConfirmSave);

            $(document).on("click", "i.visiblecalendar", onClickFilterCalendar);
            $(document).on("click", "i.deletecalendar", onClickModalDeleteCalendar);
            $(document).on("click", "#DeleteCalendarModalSave", onClickDeleteCalendar);
            $(document).on("click", "div.calendarevent", onClickEvents);
            $(document).on("dblclick", "div.calendarevent", onDoubleClickEvents);
            $(document).on("click", "#AddCalendarConfirmSave", onClickAddConfirmSave);
        }

        scheduler.clearAll();
        var events = [];
        for (var i = 0; i < result.length; i++) {
            if (filters[result[i].id] === undefined) {
                filters[result[i].id] = true;
            }
            events = events.concat(result[i].events);
        }
        scheduler.parse(events, "json");
    }

    function loadAvariableCalendars() {
        $(document).off("click", "#addAvailableCalendars", onClickAddAvailableCalendars);
        getdata(_settings.id, _settings.avariableCalendarUrl, loadAvariableCalendar);
        $(document).on("click", "#addAvailableCalendars", onClickAddAvailableCalendars);
    }

    function loadAvariableCalendar(data) {
        app.common.ui.LoadSelector("availableCalendars", data);
    }

    function normalizeStartDate(idx, cal) {
        if (cal.events) {
            $.each(cal.events, function (idx, ev) {
                if (ev._start_date)
                    ev._start_date = new Date(ev._start_date);
            });
        }
    }

    function filterCalendar(calendarid) {
        if (filters[calendarid] !== undefined) {
            filters[calendarid] = !filters[calendarid];
            scheduler.updateView();
        }
    }

    function onClickFilterCalendar(e) {
        calendarVisibility(this);
        return false;
    }

    function onClickModalDeleteCalendar(elem) {
        var e = elem.currentTarget;
        $("#DeleteCalendarModalSave").attr("data-calid", $(e).attr('data-calid'));
    }

    function onClickDeleteCalendar(elem) {
        $('#DeleteCalendarModalCancel').click();
        showSpinner();
        var e = elem.currentTarget;
        var data =
        {
            id: _settings.id,
            calId: $(e).attr('data-calid')
        };

        var res = saveData(_settings.url, data, "DELETE");
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        loadAvariableCalendars();
        return false;
    }

    function onClickAddAvailableCalendars() {
        showSpinner();
        var data =
        {
            id: _settings.id,
            calId: $("#availableCalendars").val()
        };
        var res = saveData(_settings.url, data, "POST");
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        loadAvariableCalendars();
        return false;
    }

    function onClickAddConfirmSave() {
        if (!validateAddCalendar()) {
            return false;
        }

        $('#AddCalendarConfirmCancel').click();
        showSpinner();
        var data =
        {
            id: _settings.id,
            name: $("#calendarName").val(),
            description: $("#calendarDescription").val(),
            color: $("#calendarColorPickerInput").val(),
            IsPrivate: true
        };

        var res = saveData(_settings.addCalendarUrl, data, "POST");
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        loadAvariableCalendars();
        return false;
    }

    function cleanAddCalendarModal() {
        $("#calendarName").val('');
        $("#calendarDescription").val('');
        $("#calendarColorPickerInput").val('');
        $('*[data-valmsg-for="calendarName"]').text('');
    }

    function savepriority(e) {
        stopPropagation(e.parent);
        showSpinner();
        var data =
        {
            id: _settings.id,
            calId: $(e).attr('data-calid'),
            priority: $(e).val()
        };
        var res = saveData(_settings.url, data, "PUT");
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        loadAvariableCalendars();
        return false;
    }

    function onClickEvents(ev) {
        var date = $(ev.currentTarget).attr('data-start-date');
        scheduler.updateView(date);
        return false;
    }

    function onDoubleClickEvents(ev) {
        scheduler.showLightbox($(ev.currentTarget).attr('data-eventid'));
        return false;
    }

    function calendarVisibility(that) {
        var element = $(that);
        if (element.hasClass("fa-eye")) {
            element.removeClass("fa-eye");
            element.addClass("fa-eye-slash");
        } else {
            element.removeClass("fa-eye-slash");
            element.addClass("fa-eye");
        }
        filterCalendar(element.attr("data-calid"));
        return false;
    }

    function initScheduler() {
        scheduler.config.show_loading = true;
        scheduler.config.xml_date = "%Y-%m-%d %H:%i";
        scheduler.config.server_utc = true;
        scheduler.config.multi_day = true;
        scheduler.config.mark_now = true;
        scheduler.config.first_hour = 0;
        scheduler.config.limit_time_select = true;
        scheduler.locale.labels.timeline_tab = "Timeline";
        scheduler.locale.labels.section_custom = "Section";
        scheduler.config.occurrence_timestamp_in_utc = true;
        scheduler.config.include_end_by = true;
        scheduler.config.repeat_precise = true;
        scheduler.config.details_on_create = true;
        scheduler.config.details_on_dblclick = true;
        scheduler.config.icons_select = ["icon_details", "icon_delete"];
        scheduler.xy.min_event_height = 30;
        scheduler.config.container_autoresize = true;
        scheduler.config.auto_end_date = true;
        scheduler.calendar_options = [];
        scheduler.category_options = [];
        scheduler.config.full_day = true;
        scheduler.config.container_autoresize = true;

        scheduler.config.lightbox.sections = [
            { name: scheduler.locale.labels.name_field, height: 50, map_to: "text", type: "textarea", focus: true },
            { name: scheduler.locale.labels.description_field, height: 50, map_to: "description", type: "textarea" },
            { name: scheduler.locale.labels.color_field, map_to: "color", type: "color_picker" },
            { name: scheduler.locale.labels.calendar_field, height: 23, type: "select", options: scheduler.calendar_options, map_to: "calendar_id" },
            { name: scheduler.locale.labels.category_field, height: 23, type: "select", options: scheduler.category_options, map_to: "event_category_id" },
            { name: "recurring", height: 115, type: "recurring", map_to: "rec_type", button: "recurring" },
            { name: "time", height: 72, type: "time", map_to: "auto" }
        ];

        scheduler.form_blocks["color_picker"] = {
            render: function (sns) {
                return "<div class='row mt-2'><div class='col' id='colorPickerContainer'><input type='text' id='colorPickerInput' readonly></input><div id='colorPicker'></div></div></div>";
            },
            set_value: function (node, value, ev, config) {
                $('#' + config.inputId).val(ev.color || "#f1f1f1")
                    .css({
                        'background-color': ev.color || "#f1f1f1",
                        'color': '#555555'
                    });

                loadColorPicker(ev);
            },
            get_value: function (node, ev, config) {
                return $('#' + config.inputId).val();
            },
            focus: function (node) {
            }
        };
        scheduler.filter_month = scheduler.filter_day = scheduler.filter_week = scheduler.filter_year = function (id, event) {
            if (filters[event.calendar_id] || event.calendar_id === scheduler.undefined) {
                return true;
            }
            return false;
        };
        scheduler.attachEvent("onBeforeLightbox", onBeforeLightbox);
        scheduler.attachEvent("onEventSave", OnEventSave);
        scheduler.attachEvent("onEventDeleted", OnEventDeleted);
        scheduler.attachEvent("onEventChanged", OnEventChanged);
        scheduler.attachEvent("onMouseMove", onMouseMove);

        scheduler.attachEvent("onXLE", function () {
            hideSpinner();
        });
    }

    function loadColorPicker(e) {
        var initialColor = e.color || "#f1f1f1";
        var options = {
            btnSelect: _settings.btnSelect,
            btnCancel: _settings.btnCancel,
            color: initialColor,
            parent: app.calendar.constants.ColorPickerParent,
            skin: app.calendar.constants.ColorPickerSkin,
            input: app.calendar.constants.ColorPickerInput,
            setPosition: "right",
            toggle: true
        };
        app.colorPicker.Init(options);

        app.contrastColor.Init(
            {
                callFrom: 'lightbox',
                parentElem: '#colorPickerContainer',
                elemChild: '#colorPickerInput',
                childColor: e.color
            });
    }

    function onBeforeLightbox(id) {
        scheduler.resetLightbox();
        loadCalendarName();
        loadCalendarEventCategories();
        return true;
    }

    function OnEventChanged(id, event) {
        var res = null;
        if (event.event_pid === 0) {
            res = updateCalendarEvent(event);
        }
        else {
            dhtmlx.alert(_settings.SequenceNoMove);
            refreshData();
            return false;
        }
    }

    function OnEventSave(id, event, creationdate) {
        event.id = id;

        if (!validateEvent(event)) {
            return false;
        }

        var res = null;

        if (creationdate !== null) {
            res = createCalendarEvent(event);
        }
        else {
            if (event.event_pid || (event.id.search && event.id.search("#") > -1)) {
                if (isNaN(event.id)) {
                    event.event_pid = event.id.split('#')[0];
                    event.event_length = event.id.split('#')[1];
                    res = createCalendarEvent(event);
                } else {
                    res = updateCalendarEvent(event);
                }
            }
            else {
                res = updateCalendarEvent(event);
            }
        }
        return false;
    }

    function OnEventDeleted(id, ev) {
        if (isNaN(id)) {
            var parentEvent = scheduler._events[id.split('#')[0]];
            return false;
        }

        //actually is the cancel button being pressed on the lightbox when creating a new event
        if (scheduler.getState(id).new_event) {
            return false;
        }

        var res = deleteCalendarEvent(ev.id);
    }

    function onMouseMove(e) {
        if (e !== null) {
            app.contrastColor.Init(
                {
                    callFrom: 'scheduler',
                    parentElem: '.dhx_year_tooltip',
                    elemChild: '.dhx_tooltip_line',
                    childColor: '',
                    iconDetail: '.dhx_event_icon.icon_details'
                });
        }
    }

    function createCalendarEvent(event) {
        showSpinner();
        event.id = -1;
        event.color = $("#colorPickerInput").val();
        var res = saveData(_settings.url + _settings.eventUrl, event, "POST", SaveEventCallBack);
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        return res;
    }

    function updateCalendarEvent(event) {
        showSpinner();
        var res = saveData(_settings.url + _settings.eventUrl, event, "PUT", SaveEventCallBack);
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        return res;
    }

    function deleteCalendarEvent(id) {
        showSpinner();
        var res = saveData(_settings.url + _settings.eventUrl, id, "Delete", SaveEventCallBack);
        dhtmlx.alert(res.text);
        hideSpinner();
        refreshData();
        return res;
    }

    function saveData(url, data, type, successfunction) {
        var res = null;
        $.ajax({
            type: type,
            url: url,
            async: false,
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                res = result;
                if (successfunction !== undefined)
                    successfunction();
            },
            error: function (result) {
                res = result;
                if (successfunction !== undefined)
                    successfunction();
            }
        });
        return res;
    }

    function SaveEventCallBack() {
        scheduler.endLightbox(false);
    }

    function loadCalendarEventCategories() {
        var event_categories_options = [];
        $.each(_settings.eventCategory, function (idx, ec) {
            event_categories_options.push({ key: ec.Id, label: ec.Name });
        });
        scheduler.category_options = event_categories_options;
        update_select_options(scheduler.formSection(scheduler.locale.labels.category_field).control, event_categories_options);
    }

    function loadCalendarName() {
        var lightbox_calendar_options = [];
        $.each(dataCalendar, function (idx, c) {
            lightbox_calendar_options.push({ key: c.id, label: c.name });
        });
        scheduler.calendar_options = lightbox_calendar_options;
        update_select_options(scheduler.formSection(scheduler.locale.labels.calendar_field).control, lightbox_calendar_options);
    }

    var update_select_options = function (select, options) {
        select.options.length = 0;
        for (var i = 0; i < options.length; i++) {
            var option = options[i];
            select[i] = new Option(option.label, option.key);
        }
    };

    function validateEvent(event) {
        if (event.text.trim().length === 0) {
            dhtmlx.alert(_settings.NameEmpty);
            return false;
        }

        if (event.calendar_id.length === 0) {
            dhtmlx.alert(_settings.CalendarEmpty);
            return false;
        }

        if (!checkDateRange(event.start_date, event.end_date)) {
            return false;
        }

        return true;
    }

    function checkDateRange(start, end) {
        var startDate = Date.parse(start);
        var endDate = Date.parse(end);
        if (isNaN(startDate)) {
            dhtmlx.alert(_settings.startDateEmpty);
            return false;
        }
        if (isNaN(endDate)) {
            dhtmlx.alert(_settings.endDateEmpty);
            return false;
        }
        if (startDate >= endDate) {
            dhtmlx.alert(_settings.endDateGreaterThanStartDate);
            return false;
        }
        return true;
    }

    function validateAddCalendar() {
        if ($("#calendarName").val().trim().length === 0) {
            $('*[data-valmsg-for="calendarName"]').text(_settings.NameEmpty);
            return false;
        }
        return true;
    }

    function stopPropagation(e) { e = e || window.event; if (typeof e.stopPropagation !== "undefined") { e.stopPropagation(); } else { e.cancelBubble = true; } }

    return {
        Init: init,
        Savepriority: savepriority,
        CleanAddCalendarModal: cleanAddCalendarModal
    };
})();