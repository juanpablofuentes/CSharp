namespace Group.Salto.Entities.Tenant
{
    public class AssetsWorkOrders
    {
        public int WorkOrderId { get; set; }
        public int AssetId { get; set; }

        public Assets Assets { get; set; }
        public WorkOrders WorkOrder { get; set; }
    }
}
