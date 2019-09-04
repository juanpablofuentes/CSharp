using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class WorkForm : BaseEntity
    {
        public string Name { get; set; }
        public string Templates { get; set; }
        public int Type { get; set; }
    }
}