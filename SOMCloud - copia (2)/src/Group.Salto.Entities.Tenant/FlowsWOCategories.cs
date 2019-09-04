namespace Group.Salto.Entities.Tenant
{
    public class FlowsWOCategories 
    {
        public int WOCategoryId { get; set; }
        public int FlowId { get; set; }

        public WorkOrderCategories WorkOrderCategories { get; set; }
        public Flows Flows { get; set; }
    }
}