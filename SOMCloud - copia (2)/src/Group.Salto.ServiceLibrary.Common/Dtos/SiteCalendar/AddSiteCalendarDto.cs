using Group.Salto.ServiceLibrary.Common.Dtos.Calendar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.SiteCalendar
{
    public class AddSiteCalendarDto : AddCalendarBaseDto
    {
        public int LocationId { get; set; }
    }
}