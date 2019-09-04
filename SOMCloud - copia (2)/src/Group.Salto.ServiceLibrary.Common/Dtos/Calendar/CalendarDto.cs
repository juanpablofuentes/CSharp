using Group.Salto.ServiceLibrary.Common.Dtos.CalendarEvents;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Calendar
{
    public class CalendarDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public bool IsPrivate { get; set; }
        public int Priority { get; set; }
        public int DisponibilityCategoryId { get; set; }
        public int IsDisponible { get; set; }
        public IList<CalendarEventDto> Events { get; set; }
    }
}