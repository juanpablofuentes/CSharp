//calendars constants
var app = app || {};
app.calendar = app.calendar || {};
app.calendar.constants = app.calendar.constants || {};
app.calendar.constants = (function () {
    var calendarFilter = '#filterCalendar';
    var calendarFilterNameFiledId = '#CalendarFilterName';
    var calendarFilterDescriptionFieldId = '#CalendarFilterDescription';
    var calendarButtonApplyFilter = '#btnCalendarApplyFilter';
    var calendarDeleteButtonId = '.deleteButton';
    var calendarGrid = "CalendarsGrid";
    var confirmationModalId = '#confirmationModal';
    var confirmationModalConfirmSave = '#confirmationModalConfirmSave';
    var confirmationModalConfirmCancel = '#confirmationModalConfirmCancel';
    var confirmationWitPeopleModalId = '#confirmationWitPeopleModal';
    var confirmationWitPeopleModalConfirmSave = '#confirmationWitPeopleModalConfirmSave';
    var confirmationWitPeopleModalConfirmCancel = '#confirmationWitPeopleModalConfirmCancel';
    var colorPickerInput = 'colorPickerInput';
    var colorPickerParent = 'colorPicker';
    var calendarColorPickerInput = 'calendarColorPickerInput';
    var calendarColorPickerParent = 'calendarColorPicker';
    var colorPickerSkin = 'material';
    var calendarColorPickerInitialColor = '#ffffff';

    return {
        CalendarFilter: calendarFilter,
        CalendarFilterNameFiledId: calendarFilterNameFiledId,
        CalendarFilterDescriptionFieldId: calendarFilterDescriptionFieldId,
        CalendarButtonApplyFilter: calendarButtonApplyFilter,
        CalendarDeleteButtonId: calendarDeleteButtonId,
        CalendarGrid: calendarGrid,
        ConfirmationModalId: confirmationModalId,
        ConfirmationModalConfirmSave: confirmationModalConfirmSave,
        ConfirmationModalConfirmCancel: confirmationModalConfirmCancel,
        ColorPickerInput: colorPickerInput,
        ColorPickerParent: colorPickerParent,
        ColorPickerSkin: colorPickerSkin,
        ConfirmationWitPeopleModalId: confirmationWitPeopleModalId,
        ConfirmationWitPeopleModalConfirmSave: confirmationWitPeopleModalConfirmSave,
        ConfirmationWitPeopleModalConfirmCancel: confirmationWitPeopleModalConfirmCancel,
        CalendarColorPickerInput: calendarColorPickerInput,
        CalendarColorPickerParent: calendarColorPickerParent,
        CalendarColorPickerInitialColor: calendarColorPickerInitialColor
    };
})();