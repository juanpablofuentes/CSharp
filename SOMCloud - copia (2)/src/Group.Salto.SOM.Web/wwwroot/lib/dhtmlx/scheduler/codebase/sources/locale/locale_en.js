/*
@license
dhtmlxScheduler v.5.1.6 Professional

This software is covered by DHTMLX Enterprise License. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
Scheduler.plugin(function (scheduler) {
    scheduler.locale = {
        date: {
            month_full: ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"],
            month_short: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
            day_full: ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"],
            day_short: ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"]
        },
        labels: {
            dhx_cal_today_button: "Today",
            day_tab: "Day",
            week_tab: "Week",
            month_tab: "Month",
            new_event: "New event",
            icon_save: "Save",
            icon_cancel: "Cancel",
            icon_details: "Details",
            icon_edit: "Edit",
            icon_delete: "Delete",
            confirm_closing: "Your changes will be lost, are your sure ?",//Your changes will be lost, are your sure ?
            confirm_deleting: "Event will be deleted permanently, are you sure?",
            section_description: "Description",
            section_time: "Time period",
            full_day: "Full day",

            /*custom fields*/
            category_field: "Category",
            description_field: "Description",
            color_field: "Color",
            calendar_field: "Calendar",
            name_field: "Name <span class=\"required\">*</span>",
            cost_hour_field: "&euro;/Hour <span class=\"required\">*</span>",
            allday_field: "All day",
            view: "View",
            tasks: "Tasks",
            edit: "Edit",
            remove: "Remove",

            /*recurring events*/
            confirm_recurring: "Do you want to edit the whole set of repeated events?",
            section_recurring: "Repeat event",
            button_recurring: "Enabled",
            button_recurring_open: "Disabled",
            button_edit_series: "Edit series",
            button_edit_occurrence: "Edit occurrence",

            /*agenda view extension*/
            agenda_tab: "Agenda",
            date: "Date",
            description: "Description",

            /*year view extension*/
            year_tab: "Year",

            /* week agenda extension */
            week_agenda_tab: "Agenda",

            /*grid view extension*/
            grid_tab: "Grid",

            /* touch tooltip*/
            drag_to_create: "Drag to create",
            drag_to_move: "Drag to move",

            /* dhtmlx message default buttons */
            message_ok: "OK",
            message_cancel: "Cancel",

            /* wai aria labels for non-text controls */
            next: "Next",
            prev: "Previous",
            year: "Year",
            month: "Month",
            day: "Day",
            hour: "Hour",
            minute: "Minute"
        }
    };
});