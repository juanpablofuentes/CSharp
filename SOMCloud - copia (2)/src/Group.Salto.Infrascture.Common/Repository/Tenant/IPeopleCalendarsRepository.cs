using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IPeopleCalendarsRepository : IRepository<PeopleCalendars>
    {
        IList<PeopleCalendars> GetPeopleCalendarNotDeleted(int peopleId);
        PeopleCalendars GetByCalendarIdAndPeopleId(int calendarId, int peopleId);
        IList<PeopleCalendars> GetPeoplePreferenceCalendar(int peopleId);
        SaveResult<PeopleCalendars> CreateCalendarPeople(PeopleCalendars entity);
        SaveResult<PeopleCalendars> UpdateCalendarPeople(PeopleCalendars entity);
        SaveResult<PeopleCalendars> DeleteCalendarPeople(PeopleCalendars entity);
    }
}