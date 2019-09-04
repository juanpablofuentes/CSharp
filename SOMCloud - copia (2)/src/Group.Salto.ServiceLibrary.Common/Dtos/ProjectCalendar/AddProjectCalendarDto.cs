using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ProjectCalendar
{
    public class AddProjectCalendarDto : AddCalendarBaseDto
    {
        public int ProjectId { get; set; }
    }
}