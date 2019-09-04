using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class StatesSla : BaseEntity
    {
        public int SlaId { get; set; }
        public int? MinutesToTheEnd { get; set; }
        public string RowColor { get; set; }

        public Sla Sla { get; set; }
    }
}
