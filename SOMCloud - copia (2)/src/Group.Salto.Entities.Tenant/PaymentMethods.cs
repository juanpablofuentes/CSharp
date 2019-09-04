using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class PaymentMethods : BaseEntity
    {
        public string Mode { get; set; }

        public ICollection<Expenses> Expenses { get; set; }
    }
}
