using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PeopleCalendarsRepository : BaseRepository<PeopleCalendars>, IPeopleCalendarsRepository
    {
        public PeopleCalendarsRepository(ITenantUnitOfWork uow) : base(uow) {}

        public IList<PeopleCalendars> GetPeopleCalendarNotDeleted(int peopleId)
        {
            return All()
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .Include(x => x.People)
                    .Where(pc => pc.PeopleId == peopleId && !pc.Calendar.IsDeleted)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public PeopleCalendars GetByCalendarIdAndPeopleId(int calendarId, int peopleId)
        {
            return Find(x => x.PeopleId == peopleId && x.CalendarId == calendarId);
        }

        public IList<PeopleCalendars> GetPeoplePreferenceCalendar(int peopleId)
        {
            return Filter(pc => pc.PeopleId == peopleId && !pc.Calendar.IsDeleted)
                    .Include(c => c.Calendar)
                        .ThenInclude(c => c.CalendarEvents)
                    .OrderByDescending(o => o.CalendarPriority)
                    .ToList();
        }

        public SaveResult<PeopleCalendars> CreateCalendarPeople(PeopleCalendars entity)
        {
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<PeopleCalendars> UpdateCalendarPeople(PeopleCalendars entity)
        {
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<PeopleCalendars> DeleteCalendarPeople(PeopleCalendars entity)
        {
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}