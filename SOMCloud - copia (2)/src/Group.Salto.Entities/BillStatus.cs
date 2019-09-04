using Group.Salto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.Entities
{
    public class BillStatus : BaseEntity
    {
        public string Name { get; set; }
        public int BillStatusId { get; set; }
    }
}
