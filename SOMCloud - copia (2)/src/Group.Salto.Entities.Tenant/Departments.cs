using System.Collections.Generic;
using Group.Salto.Common.Entities;

namespace Group.Salto.Entities.Tenant
{
    public class Departments : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public int CompanyId { get; set; }

        public Companies Company { get; set; }
        public ICollection<People> People { get; set; }
    }
}
