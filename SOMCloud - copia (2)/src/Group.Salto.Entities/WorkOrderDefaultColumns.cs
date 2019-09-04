using Group.Salto.Common;

namespace Group.Salto.Entities
{
    public class WorkOrderDefaultColumns : BaseEntity
    {
        public int WorkOrderColumnId { get; set; }
        public int Position { get; set; }
        public WorkOrderColumns WorkOrderColumns { get; set; }
    }
}