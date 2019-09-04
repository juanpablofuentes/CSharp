using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.AvailabilityCategories
{
    public class AvailabilityCategoriesDto
    {
        public int Id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
    }
}
