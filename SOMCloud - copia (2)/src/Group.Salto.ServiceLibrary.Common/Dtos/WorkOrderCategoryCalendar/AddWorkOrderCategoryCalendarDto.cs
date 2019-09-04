using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar
{
    public class AddWorkOrderCategoryCalendarDto : AddCalendarBaseDto
    {
        public int WorkOrderCategoryId { get; set; }
    }
}