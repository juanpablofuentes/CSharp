using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class AvailabilityCategories : BaseEntity
    {
        public string Name { get; set; }
        public bool? IsAvailable { get; set; }

        public ICollection<CalendarEventCategories> CalendarEventCategories { get; set; }
    }
}
