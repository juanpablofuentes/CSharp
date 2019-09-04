namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategoryKnowledge
    {
        public int WorkOrderCategoryId { get; set; }
        public int KnowledgeId { get; set; }
        public int? KnowledgeLevel { get; set; }

        public Knowledge Knowledge { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
    }
}
