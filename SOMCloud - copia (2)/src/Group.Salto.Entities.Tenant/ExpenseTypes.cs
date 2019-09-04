using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ExpenseTypes : BaseEntity
    {
        public string Type { get; set; }
        public string Unit { get; set; }

        public ICollection<Expenses> Expenses { get; set; }
    }
}
