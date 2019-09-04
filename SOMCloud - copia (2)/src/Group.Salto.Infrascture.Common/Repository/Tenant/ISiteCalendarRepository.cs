using System.Collections.Generic;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface ISiteCalendarRepository
    {
        IList<LocationCalendar> GetSiteCalendarsNotDeletedBySiteId(int siteId);
        LocationCalendar GetByCalendarIdAndCategoryId(int calendarId, int siteId);
        IList<LocationCalendar> GetSitePreferenceCalendar(int siteId);
        SaveResult<LocationCalendar> CreateSiteCalendar(LocationCalendar entity);
        SaveResult<LocationCalendar> UpdateSiteCalendar(LocationCalendar entity);
        SaveResult<LocationCalendar> DeleteSiteCalendar(LocationCalendar entity);
        LocationCalendar DeleteOnContext(LocationCalendar entity);
    }
}