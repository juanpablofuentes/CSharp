using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class FormElements : BaseEntity
    {
        public int? FormConfigsId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public FormConfigs FormConfigs { get; set; }
    }
}
