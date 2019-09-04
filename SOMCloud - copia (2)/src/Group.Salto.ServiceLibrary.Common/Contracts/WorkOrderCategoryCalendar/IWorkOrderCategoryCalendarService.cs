using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoryCalendar;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoryCalendar
{
    public interface IWorkOrderCategoryCalendarService
    {
        ResultDto<IList<CalendarDto>> GetByWorkOrderCategoryIdNotDeleted(int workOrderCategoryId);
        ResultDto<WorkOrderCategoryCalendarDto> Create(WorkOrderCategoryCalendarDto model);
        ResultDto<WorkOrderCategoryCalendarDto> Update(WorkOrderCategoryCalendarDto model);
        ResultDto<bool> Delete(WorkOrderCategoryCalendarDto model);
    }
}