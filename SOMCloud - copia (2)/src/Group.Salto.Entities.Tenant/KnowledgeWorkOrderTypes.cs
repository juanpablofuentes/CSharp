namespace Group.Salto.Entities.Tenant
{
    public class KnowledgeWorkOrderTypes
    {
        public int KnowledgeId { get; set; }
        public int WorkOrderTypeId { get; set; }

        public Knowledge Knowledge { get; set; }
        public WorkOrderTypes WorkOrderType { get; set; }
    }
}
