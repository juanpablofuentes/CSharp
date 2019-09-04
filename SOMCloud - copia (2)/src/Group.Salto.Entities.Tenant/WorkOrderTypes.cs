using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderTypes : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public int? CollectionsTypesWorkOrdersId { get; set; }
        public int? WorkOrderTypesFatherId { get; set; }
        public int? SlaId { get; set; }
        public int? EstimatedDuration { get; set; }
        public string Serie { get; set; }

        public CollectionsTypesWorkOrders CollectionsTypesWorkOrders { get; set; }
        public Sla Sla { get; set; }
        public WorkOrderTypes WorkOrderTypesFather { get; set; }
        public ICollection<WorkOrderTypes> InverseWorkOrderTypesFather { get; set; }
        public ICollection<KnowledgeWorkOrderTypes> KnowledgeWorkOrderTypes { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesWorkOrderTypesN1 { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesWorkOrderTypesN2 { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesWorkOrderTypesN3 { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesWorkOrderTypesN4 { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValuesWorkOrderTypesN5 { get; set; }
        public ICollection<ToolsTypeWorkOrderTypes> ToolsTypeWorkOrderTypes { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
    }
}
