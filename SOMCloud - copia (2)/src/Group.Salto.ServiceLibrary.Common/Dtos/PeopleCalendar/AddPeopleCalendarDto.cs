using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;

namespace Group.Salto.ServiceLibrary.Common.Dtos.PeopleCalendar
{
    public class AddPeopleCalendarDto : AddCalendarBaseDto
    {
        public int PeopleId { get; set; }
    }
}