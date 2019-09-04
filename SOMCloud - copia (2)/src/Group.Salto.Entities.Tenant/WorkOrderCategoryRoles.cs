namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategoryRoles
    {
        public int RoleId { get; set; }
        public int WorkOrderCategoryId { get; set; }

        public WorkOrderCategories WorkOrderCategory { get; set; }
    }
}