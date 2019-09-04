using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class ItemsPointsRate : BaseEntity
    {
        public int ItemId { get; set; }
        public int PointsRateId { get; set; }
        public double Points { get; set; }

        public Items Item { get; set; }
        public PointsRate PointsRate { get; set; }
    }
}
