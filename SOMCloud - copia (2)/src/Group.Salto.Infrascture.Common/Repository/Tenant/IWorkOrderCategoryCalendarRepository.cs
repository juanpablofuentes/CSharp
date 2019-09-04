using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IWorkOrderCategoryCalendarRepository : IRepository<WorkOrderCategoryCalendar>
    {
        IList<WorkOrderCategoryCalendar> GetWorkOrderCategoryCalendarNotDeleted(int categoryId);
        WorkOrderCategoryCalendar GetByCalendarIdAndCategoryId(int calendarId, int categoryId);
        IList<WorkOrderCategoryCalendar> GetCategoryPreferenceCalendar(int categoryId);
        SaveResult<WorkOrderCategoryCalendar> CreateWorkOrderCategoryCalendar(WorkOrderCategoryCalendar entity);
        SaveResult<WorkOrderCategoryCalendar> UpdateWorkOrderCategoryCalendar(WorkOrderCategoryCalendar entity);
        SaveResult<WorkOrderCategoryCalendar> DeleteWorkOrderCategoryCalendar(WorkOrderCategoryCalendar entity);
    }
}