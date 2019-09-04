using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class BillLine : BaseEntity
    {
        public int BillId { get; set; }
        public int ItemId { get; set; }
        public double Units { get; set; }
        public string SerialNumber { get; set; }

        public Bill Bill { get; set; }
        public Items Item { get; set; }
        public ItemsSerialNumber ItemsSerialNumber { get; set; }
        public DnAndMaterialsAnalysis DnAndMaterialsAnalysis { get; set; }
    }
}
