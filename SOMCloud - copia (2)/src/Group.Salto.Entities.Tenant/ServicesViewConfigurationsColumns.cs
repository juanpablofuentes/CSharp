using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class ServicesViewConfigurationsColumns : BaseEntity
    {
        public int ColumnId { get; set; }
        public int ColumnOrder { get; set; }
        public string FilterValues { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }

        public ServicesViewConfigurations IdNavigation { get; set; }
    }
}
