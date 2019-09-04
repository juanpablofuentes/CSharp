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
    public class FinalClientSiteCalendarRepository : BaseRepository<FinalClientSiteCalendar>, IFinalClientSiteCalendarRepository
    {
        public FinalClientSiteCalendarRepository(ITenantUnitOfWork uow) : base(uow){}

        public IList<FinalClientSiteCalendar> GetFinalClientSiteCalendarsNotDeletedByFinalClientSiteId(int clientSiteId)
        {
            return Filter(pc => pc.FinalClientSiteId == clientSiteId && !pc.Calendar.IsDeleted)
                .Include(c => c.Calendar)
                    .ThenInclude(ce => ce.CalendarEvents)
                .OrderByDescending(o => o.CalendarPriority)
                .ToList();
        }

        public FinalClientSiteCalendar GetByCalendarIdAndCategoryId(int calendarId, int clientSiteId)
        {
            return Find(x => x.FinalClientSiteId == clientSiteId && x.CalendarId == calendarId);
        }

        public IList<FinalClientSiteCalendar> GetFinalClientSitePreferenceCalendar(int clientSiteId)
        {
            return Filter(pc => pc.FinalClientSiteId == clientSiteId && !pc.Calendar.IsDeleted)
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public SaveResult<FinalClientSiteCalendar> CreateFinalClientSiteCalendar(FinalClientSiteCalendar entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<FinalClientSiteCalendar> UpdateFinalClientSiteCalendar(FinalClientSiteCalendar entity)
        {
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<FinalClientSiteCalendar> DeleteFinalClientSiteCalendar(FinalClientSiteCalendar entity)
        {
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public FinalClientSiteCalendar DeleteOnContext(FinalClientSiteCalendar entity)
        {
            Delete(entity);
            return entity;
        }
    }
}