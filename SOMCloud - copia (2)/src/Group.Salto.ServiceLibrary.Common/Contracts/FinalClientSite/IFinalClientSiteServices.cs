using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using Group.Salto.ServiceLibrary.Common.Dtos.FinalClientSiteCalendar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Contracts.FinalClientSite
{
    public interface IFinalClientSiteCalendarServices
    {
        ResultDto<IList<CalendarDto>> GetFinalClientSiteCalendarsNotDeletedByFinalClientSiteId(int projectId);
        ResultDto<FinalClientSiteCalendarDto> Create(FinalClientSiteCalendarDto model);
        ResultDto<FinalClientSiteCalendarDto> Update(FinalClientSiteCalendarDto model);
        ResultDto<bool> Delete(FinalClientSiteCalendarDto model);
    }
}
