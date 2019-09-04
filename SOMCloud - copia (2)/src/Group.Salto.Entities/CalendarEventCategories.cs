using Group.Salto.Common.Entities;

namespace Group.Salto.Entities
{
    public class CalendarEventCategories : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int AvailabilityCategoriesId { get; set; }
        public AvailabilityCategories AvailabilityCategories { get; set; }
    }
}
