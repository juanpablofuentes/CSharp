using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class OrderedCalendarsDto
    {
        public List<Calendars> Calendars { get; set; }
        public int Preference { get; set; }
        public EntitiesWithCalendarsEnum Type { get; set; }

        public DateTime ActiveStart { get; set; }
        public DateTime ActiveEnd { get; set; }

        public bool HasFutureEvents(DateTime start)
        {
            if (ActiveEnd == null) return false;
            else return (start < ActiveEnd);
        }
    }
}