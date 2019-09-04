using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PostconditionCollections : BaseEntity
    {
        public int? TaskId { get; set; }

        public Tasks Task { get; set; }
        public ICollection<Postconditions> Postconditions { get; set; }
        public ICollection<Preconditions> Preconditions { get; set; }
    }
}
