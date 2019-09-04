using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class ZoneProjectPostalCode : BaseEntity
    {
        public int ZoneProjectId { get; set; }
        public string PostalCode { get; set; }

        public ZoneProject ZoneProject { get; set; }
    }
}
