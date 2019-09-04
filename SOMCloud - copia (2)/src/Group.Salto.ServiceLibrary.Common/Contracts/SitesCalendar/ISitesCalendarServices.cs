using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.SitesCalendar
{
    public interface ISitesCalendarServices
    {
        ResultDto<IList<CalendarDto>> GetSitesCalendarsNotDeleted_BySiteId(int siteId);
        ResultDto<SiteCalendarDto> Create(SiteCalendarDto model);
        ResultDto<SiteCalendarDto> Update(SiteCalendarDto model);
        ResultDto<bool> Delete(SiteCalendarDto model);
    }
}
