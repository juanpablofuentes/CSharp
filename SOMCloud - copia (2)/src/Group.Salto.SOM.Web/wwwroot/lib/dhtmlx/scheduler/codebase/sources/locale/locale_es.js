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
            month_full: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"],
            month_short: ["Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic"],
            day_full: ["Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado"],
            day_short: ["Dom", "Lun", "Mar", "Mié", "Jue", "Vie", "Sáb"]
        },
        labels: {
            dhx_cal_today_button: "Hoy",
            day_tab: "Día",
            week_tab: "Semana",
            month_tab: "Mes",
            new_event: "Nuevo evento",
            icon_save: "Guardar",
            icon_cancel: "Cancelar",
            icon_details: "Detalles",
            icon_edit: "Editar",
            icon_delete: "Eliminar",
            confirm_closing: "", //"Sus cambios se perderán, continuar ?"
            confirm_deleting: "El evento se borrará definitivamente, ¿continuar?",
            section_description: "Descripción",
            section_time: "Período",
            full_day: "Todo el día",

            /*recurring events*/
            confirm_recurring: "¿Desea modificar el conjunto de eventos repetidos?",
            section_recurring: "Repetir evento",
            button_recurring: "Permitir",
            button_recurring_open: "No Permitir",
            button_edit_series: "Editar la serie",
            button_edit_occurrence: "Editar este evento",

            /*custom fields*/
            category_field: "Categoría",
            description_field: "Descripción",
            color_field: "Color",
            calendar_field: "Calendario",
            name_field: "Nombre <span class=\"required\">*</span>",
            cost_hour_field: "&euro;/Hora <span class=\"required\">*</span>",
            allday_field: "Todo el día",
            view: "Ver",
            tasks: "Tareas",
            edit: "Editar",
            remove: "Eliminar",

            /*agenda view extension*/
            agenda_tab: "Día",
            date: "Fecha",
            description: "Descripción",

            /*year view extension*/
            year_tab: "Año",

            /*week agenda view extension*/
            week_agenda_tab: "Día",

            /*grid view extension*/
            grid_tab: "Grilla",

            /* touch tooltip*/
            drag_to_create: "Arrastrar para crear",
            drag_to_move: "Arrastrar para mover",

            /* dhtmlx message default buttons */
            message_ok: "OK",
            message_cancel: "Cancelar",

            /* wai aria labels for non-text controls */
            next: "Siguiente",
            prev: "Anterior",
            year: "Año",
            month: "Mes",
            day: "Día",
            hour: "Hora",
            minute: "Minuto"
        }
    };
});