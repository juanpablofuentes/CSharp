using System;

namespace Group.Salto.Entities.Tenant
{
    public class MainWoViewConfigurationsColumns
    {
        public int UserMainWoviewConfigurationId { get; set; }
        public int ColumnId { get; set; }
        public int ColumnOrder { get; set; }
        public string FilterValues { get; set; }
        public DateTime? FilterStartDate { get; set; }
        public DateTime? FilterEndDate { get; set; }

        public UsersMainWoviewConfigurations UserMainWoviewConfiguration { get; set; }
    }
}
