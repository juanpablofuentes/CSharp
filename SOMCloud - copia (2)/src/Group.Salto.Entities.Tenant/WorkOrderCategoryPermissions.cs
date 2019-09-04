namespace Group.Salto.Entities.Tenant
{
    public class WorkOrderCategoryPermissions
    {
        public int PermissionId { get; set; }
        public int WorkOrderCategoryId { get; set; }

        public WorkOrderCategories WorkOrderCategory { get; set; }
        public Permissions Permission { get; set; }
    }
}
