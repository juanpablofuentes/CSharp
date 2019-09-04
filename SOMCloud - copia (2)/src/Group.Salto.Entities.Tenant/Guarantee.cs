using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Guarantee : BaseEntity
    {
        public int? IdExternal { get; set; }
        public string Standard { get; set; }
        public DateTime? StdStartDate { get; set; }
        public DateTime? StdEndDate { get; set; }
        public string Armored { get; set; }
        public DateTime? BlnStartDate { get; set; }
        public DateTime? BlnEndDate { get; set; }
        public string Provider { get; set; }
        public DateTime? ProStartDate { get; set; }
        public DateTime? ProEndDate { get; set; }

        public ICollection<Assets> Assets { get; set; }
    }
}
