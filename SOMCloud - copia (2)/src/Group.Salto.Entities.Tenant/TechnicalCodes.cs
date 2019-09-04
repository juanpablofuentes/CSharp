using Group.Salto.Common;

namespace Group.Salto.Entities.Tenant
{
    public class TechnicalCodes : BaseEntity
    {
        public int? ProjectId { get; set; }
        public int PeopleTechnicId { get; set; }
        public string Code { get; set; }
        public int? WorkOrderCategoryId { get; set; }

        public People PeopleTechnic { get; set; }
        public Projects Project { get; set; }
        public WorkOrderCategories WorkOrderCategory { get; set; }
    }
}
