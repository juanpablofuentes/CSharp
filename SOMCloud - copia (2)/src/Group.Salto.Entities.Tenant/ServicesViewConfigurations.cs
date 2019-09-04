using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ServicesViewConfigurations : BaseEntity
    {
        public bool? IsDefault { get; set; }
        public string Name { get; set; }
        public int UserConfigurationId { get; set; }
        public UserConfiguration UserConfiguration { get; set; }
        public ICollection<ServicesViewConfigurationsColumns> ServicesViewConfigurationsColumns { get; set; }
    }
}
