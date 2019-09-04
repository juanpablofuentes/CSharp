using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DataAccess.Common;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SiteCalendarRepository : BaseRepository<LocationCalendar>, ISiteCalendarRepository
    {
        public SiteCalendarRepository(ITenantUnitOfWork uow) : base(uow) { }

        public IList<LocationCalendar> GetSiteCalendarsNotDeletedBySiteId(int siteId)
        {
            return Filter(pc => pc.LocationId == siteId && !pc.Calendar.IsDeleted)
                .Include(c => c.Calendar)
                    .ThenInclude(ce => ce.CalendarEvents)
                .OrderByDescending(o => o.CalendarPriority)
                .ToList();
        }

        public LocationCalendar GetByCalendarIdAndCategoryId(int calendarId, int siteId)
        {
            return Find(x => x.LocationId == siteId && x.CalendarId == calendarId);
        }

        public IList<LocationCalendar> GetSitePreferenceCalendar(int siteId)
        {
            return Filter(pc => pc.LocationId == siteId && !pc.Calendar.IsDeleted)
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public SaveResult<LocationCalendar> CreateSiteCalendar(LocationCalendar entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<LocationCalendar> UpdateSiteCalendar(LocationCalendar entity)
        {
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<LocationCalendar> DeleteSiteCalendar(LocationCalendar entity)
        {
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public LocationCalendar DeleteOnContext(LocationCalendar entity)
        {
            Delete(entity);
            return entity;
        }
    }
}