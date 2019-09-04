/*
@license
dhtmlxScheduler v.5.1.6 Professional

This software is covered by DHTMLX Enterprise License. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
Scheduler.plugin(function (scheduler) {

    scheduler.config.repeat_date = "%d/%m/%Y";
    scheduler.locale = {
        date: {
            month_full: ["Gener", "Febrer", "Març", "Abril", "Maig", "Juny", "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre"],
            month_short: ["Gen", "Feb", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Oct", "Nov", "Des"],
            day_full: ["Diumenge", "Dilluns", "Dimarts", "Dimecres", "Dijous", "Divendres", "Dissabte"],
            day_short: ["Dg", "Dl", "Dm", "Dc", "Dj", "Dv", "Ds"]
        },
        labels: {
            dhx_cal_today_button: "Avui",
            day_tab: "Dia",
            week_tab: "Setmana",
            month_tab: "Mes",
            new_event: "Nou esdeveniment",
            icon_save: "Guardar",
            icon_cancel: "Cancel·lar",
            icon_details: "Detalls",
            icon_edit: "Editar",
            icon_delete: "Esborrar",
            confirm_closing: "Els canvis es perdran, Desitges continuar?", //"Els seus canvis es perdràn, continuar ?"
            confirm_deleting: "L'esdeveniment s'esborrarà definitivament, continuar ?",
            section_description: "Descripció",
            section_time: "Periode de temps",
            full_day: "Tot el dia",

            /*recurring events*/
            confirm_recurring: "¿Desitja modificar el conjunt d'esdeveniments repetits?",
            section_recurring: "Repeteixca l'esdeveniment",
            button_recurring: "Permetre",
            button_recurring_open: "No Permetre",
            button_edit_series: "Edit sèrie",
            button_edit_occurrence: "Edita Instància",

            /*custom fields*/
            category_field: "Categoria",
            description_field: "Descripció",
            color_field: "Color",
            calendar_field: "Calendari",
            name_field: "Nom <span class=\"required\">*</span>",
            cost_hour_field: "&euro;/Hora <span class=\"required\">*</span>",
            allday_field: "Tot el dia",
            view: "Veure",
            tasks: "Tasques",
            edit: "Editar",
            remove: "Eliminar",

            /*agenda view extension*/
            agenda_tab: "Agenda",
            date: "Data",
            description: "Descripció",

            /*year view extension*/
            year_tab: "Any",

            /*week agenda view extension*/
            week_agenda_tab: "Agenda",

            /*grid view extension*/
            grid_tab: "Taula",

            /* touch tooltip*/
            drag_to_create: "Arrossegar per a crear",
            drag_to_move: "Arrossegar per a moure",

            /* dhtmlx message default buttons */
            message_ok: "OK",
            message_cancel: "Cancel·lar",

            /* wai aria labels for non-text controls */
            next: "Següent",
            prev: "Anterior",
            year: "Any",
            month: "Mes",
            day: "Dia",
            hour: "Hora",
            minute: "Minut"
        }
    };
});