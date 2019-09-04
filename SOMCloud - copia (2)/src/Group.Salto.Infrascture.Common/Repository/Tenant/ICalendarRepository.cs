using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ICalendarRepository : IRepository<Calendars>
    {
        IQueryable<Calendars> GetAllGlobals();
        Calendars GetCalendarAndEventsById(int id);
        Calendars GetByIdWithEventsAndWorkOrderCategoryCalendar(int id);
        Calendars GetByIdWithEventsAndProjectCalendar(int id);
        Calendars GetById(int id);
        IList<Calendars> GetGlobalPreferenceCalendar(int id);
        SaveResult<Calendars> UpdateCalendar(Calendars entity);
        SaveResult<Calendars> CreateCalendar(Calendars entity);
        SaveResult<Calendars> DeleteCalendar(Calendars entity);
        Calendars GetByIdWithEventsAndPeople(int id);
        Dictionary<int, string> GetKeyValuesAvailablePeopleCalendarsToAssign(int peopleId);
        Dictionary<int, string> GetKeyValuesAvailableWorkOrderCategoryCalendarsToAssign(int workOrderCategoryId);
        Dictionary<int, string> GetKeyValuesAvailableProjectCalendarsToAssign(int projectId);
        Dictionary<int, string> GetKeyValuesAvailableFinalClientSiteCalendarsToAssign(int finalClientSiteId);
        Dictionary<int, string> GetKeyValuesAvailableSiteCalendarsToAssign(int siteId);
        Calendars DeleteOnContext(Calendars entity);
    }
}