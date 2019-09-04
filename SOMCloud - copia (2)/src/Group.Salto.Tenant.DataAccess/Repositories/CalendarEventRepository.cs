using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class CalendarEventRepository : BaseRepository<CalendarEvents>, ICalendarEventRepository
    {
        public CalendarEventRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public CalendarEvents GetById(int Id)
        {
            return Find(x => x.Id == Id );
        }

        public IList<CalendarEvents> GetByCalendarId(int calendarid)
        {
            return Filter(x => x.CalendarId == calendarid).ToList();
        }

        public IList<CalendarEvents> GetByCalendarIds(int[] calendarids)
        {
            return Filter(x => calendarids.Contains(x.CalendarId.Value)).ToList();
        }

        public SaveResult<CalendarEvents> CreateCalendarEvent(CalendarEvents calendarEvent)
        {
            calendarEvent.UpdateDate = DateTime.UtcNow;
            Create(calendarEvent);
            var result = SaveChange(calendarEvent);
            return result;
        }

        public SaveResult<CalendarEvents> UpdateCalendarEvent(CalendarEvents calendarEvent)
        {
            calendarEvent.UpdateDate = DateTime.UtcNow;
            Update(calendarEvent);
            var result = SaveChange(calendarEvent);
            return result;
        }

        public SaveResult<CalendarEvents> DeleteCalendarEvent(CalendarEvents calendarEvent)
        {
            calendarEvent.UpdateDate = DateTime.UtcNow;
            Delete(calendarEvent);
            SaveResult<CalendarEvents> result = SaveChange(calendarEvent);
            result.Entity = calendarEvent;
            return result;
        }

        public CalendarEvents DeleteOnContext(CalendarEvents entity)
        {
            Delete(entity);
            return entity;
        }
    }
}