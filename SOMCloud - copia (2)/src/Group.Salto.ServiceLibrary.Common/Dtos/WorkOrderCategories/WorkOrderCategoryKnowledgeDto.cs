using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories
{
    public class WorkOrderCategoryKnowledgeDto : BaseNameIdDto<int>
    {
        public int Priority { get; set; }
    }
}