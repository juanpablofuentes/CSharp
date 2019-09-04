using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderCategoryCalendarRepository : BaseRepository<WorkOrderCategoryCalendar>, IWorkOrderCategoryCalendarRepository
    {
        public WorkOrderCategoryCalendarRepository(ITenantUnitOfWork uow) : base(uow) { }

        public IList<WorkOrderCategoryCalendar> GetWorkOrderCategoryCalendarNotDeleted(int categoryId)
        {
            return Filter(pc => pc.WorkOrderCategoryId == categoryId && !pc.Calendar.IsDeleted)
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .Include(x => x.WorkOrderCategory)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public WorkOrderCategoryCalendar GetByCalendarIdAndCategoryId(int calendarId, int categoryId)
        {
            return Find(x => x.WorkOrderCategoryId == categoryId && x.CalendarId == calendarId);
        }

        public IList<WorkOrderCategoryCalendar> GetCategoryPreferenceCalendar(int categoryId)
        {
            return Filter(pc => pc.WorkOrderCategoryId == categoryId && !pc.Calendar.IsDeleted)
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public SaveResult<WorkOrderCategoryCalendar> CreateWorkOrderCategoryCalendar(WorkOrderCategoryCalendar entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<WorkOrderCategoryCalendar> UpdateWorkOrderCategoryCalendar(WorkOrderCategoryCalendar entity)
        {
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<WorkOrderCategoryCalendar> DeleteWorkOrderCategoryCalendar(WorkOrderCategoryCalendar entity)
        {
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}