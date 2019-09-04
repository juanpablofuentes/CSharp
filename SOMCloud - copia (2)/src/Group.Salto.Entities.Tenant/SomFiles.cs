using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class SomFiles : BaseEntity
    {
        public string Container { get; set; }
        public string Directory { get; set; }
        public string Name { get; set; }
        public string ContentMd5 { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<ExpensesTicketFile> ExpensesTicketFile { get; set; }
    }
}
