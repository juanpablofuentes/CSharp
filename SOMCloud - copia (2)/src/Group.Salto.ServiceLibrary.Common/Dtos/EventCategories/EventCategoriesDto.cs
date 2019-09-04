using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.EventCategories
{
    public class EventCategoriesDto : BaseNameIdDto<int>
    {
        public DateTime UpdateDate { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public int AvailabilityCategoriesId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
