using Group.Salto.SOM.Web.Models.CalendarEvent;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Calendar
{
    public class CalendarViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool IsPrivate { get; set; }
        public int Priority { get; set; }
        public int DisponibilityCategoryId { get; set; }
        public int IsDisponible { get; set; }
        public IList<CalendarEventResponse> Events { get; set; }
    }
}