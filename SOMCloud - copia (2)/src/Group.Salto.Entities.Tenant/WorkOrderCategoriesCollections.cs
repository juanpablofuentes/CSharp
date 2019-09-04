using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategoriesCollections : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public ICollection<Projects> Projects { get; set; }
        public ICollection<WorkOrderCategories> WorkOrderCategories { get; set; }
        public ICollection<WorkOrderCategoriesCollectionCalendar> WorkOrderCategoriesCollectionCalendar { get; set; }
    }
}