using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class InfrastructureConfiguration : BaseEntity
    {
        public int TenantId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
