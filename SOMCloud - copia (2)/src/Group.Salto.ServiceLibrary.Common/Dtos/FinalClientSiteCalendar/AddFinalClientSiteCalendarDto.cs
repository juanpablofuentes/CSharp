using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.FinalClientsSiteCalendar
{
    public class AddFinalClientSiteCalendarDto : AddCalendarBaseDto
    {
        public int FinalClientSiteId { get; set; }
    }
}
