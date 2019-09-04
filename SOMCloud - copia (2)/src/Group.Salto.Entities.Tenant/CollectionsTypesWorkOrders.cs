using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class CollectionsTypesWorkOrders : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Projects> Projects { get; set; }
        public ICollection<WorkOrderTypes> WorkOrderTypes { get; set; }
    }
}