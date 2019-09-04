using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class DnAndMaterialsAnalysis : BaseEntity
    {
        public int? WorkOrder { get; set; }
        public int? Bill { get; set; }
        public int? People { get; set; }
        public string ExternalSystemNumber { get; set; }
        public int? Status { get; set; }
        public int? ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemSerialNumber { get; set; }
        public decimal? Units { get; set; }
        public decimal? PVP { get; set; }
        public decimal? PurchaseCost { get; set; }
        public decimal? TotalDeliveryNoteCost { get; set; }
        public decimal? TotalDeliveryNoteSalePrice { get; set; }

        public BillLine BillLine { get; set; }
        public Bill BillEntity { get; set; }
        public People PeopleEntity { get; set; }
        public Items Item { get; set; }
        public WorkOrders WorkOrderEntity { get; set; }
    }
}
