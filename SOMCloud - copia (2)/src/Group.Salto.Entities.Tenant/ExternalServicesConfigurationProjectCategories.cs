using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public partial class ExternalServicesConfigurationProjectCategories : BaseEntity
    {
        public int? ConfigurationId { get; set; }
        public int? ProjectId { get; set; }
        public int? WoCategoryId { get; set; }
        public string AssetSerialNumberProperty { get; set; }
        public bool? AssetMailAlert { get; set; }

        public ExternalServicesConfiguration Configuration { get; set; }
        public Projects Project { get; set; }
        public WorkOrderCategories WoCategory { get; set; }
        public ICollection<ExternalServicesConfigurationProjectCategoriesProperties> ExternalServicesConfigurationProjectCategoriesProperties { get; set; }
    }
}
