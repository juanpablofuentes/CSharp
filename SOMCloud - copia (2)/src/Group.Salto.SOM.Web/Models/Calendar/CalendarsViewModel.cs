using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Calendar
{
    public class CalendarsViewModel
    {
        public MultiItemViewModel<CalendarBaseViewModel, int> Calendars { get; set; }
        public CalendarFilterViewModel Filters{ get; set; }
    }
}
