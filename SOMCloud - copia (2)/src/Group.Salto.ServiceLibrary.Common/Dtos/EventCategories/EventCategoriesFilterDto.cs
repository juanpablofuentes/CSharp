using Group.Salto.Controls.Entities;

namespace Group.Salto.ServiceLibrary.Common.Dtos.EventCategories
{
    public class EventCategoriesFilterDto : ISortableFilter
    {
        public string OrderBy { get; set; }
        public bool Asc { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailabilityCategoriesId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
