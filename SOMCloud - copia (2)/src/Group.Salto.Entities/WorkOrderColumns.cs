using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities
{
    public class WorkOrderColumns : BaseEntity
    {
        public string Name { get; set; }
        public bool CanSort { get; set; }
        public decimal Width { get; set; }
        public string Align { get; set; }
        public int? EditType { get; set; }

        public ICollection<WorkOrderDefaultColumns> WorkOrderDefaultColumns { get; set; }
    }
}