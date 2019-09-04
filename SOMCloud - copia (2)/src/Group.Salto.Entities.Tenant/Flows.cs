using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Flows : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Published { get; set; }
        
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<FlowsProjects> FlowsProjects {get; set;}
        public ICollection<FlowsWOCategories> FlowsWOCategories { get; set; }
    }
}
