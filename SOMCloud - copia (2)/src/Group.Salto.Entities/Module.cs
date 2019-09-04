using System;
using System.Collections.Generic;
using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class Module : BaseEntity<Guid>
    {
        public string Key { get; set; }
        public string Description { get; set; }
        public ICollection<CustomerModule> CustomersAssigned { get; set; }
        public ICollection<ModuleActionGroups> ModuleActionGroups { get; set; }
    }
}