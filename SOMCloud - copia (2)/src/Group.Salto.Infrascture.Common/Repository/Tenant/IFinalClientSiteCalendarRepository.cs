using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using System.Collections.Generic;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IFinalClientSiteCalendarRepository
    {
        IList<FinalClientSiteCalendar> GetFinalClientSiteCalendarsNotDeletedByFinalClientSiteId(int clientSiteId);
        FinalClientSiteCalendar GetByCalendarIdAndCategoryId(int calendarId, int clientSiteId);
        IList<FinalClientSiteCalendar> GetFinalClientSitePreferenceCalendar(int clientSiteId);
        SaveResult<FinalClientSiteCalendar> CreateFinalClientSiteCalendar(FinalClientSiteCalendar entity);
        SaveResult<FinalClientSiteCalendar> UpdateFinalClientSiteCalendar(FinalClientSiteCalendar entity);
        SaveResult<FinalClientSiteCalendar> DeleteFinalClientSiteCalendar(FinalClientSiteCalendar entity);
        FinalClientSiteCalendar DeleteOnContext(FinalClientSiteCalendar entity);
    }
}