using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderCategoryKnowledgeDtoExtensions
    {
        public static WorkOrderCategoryKnowledgeDto ToWorkOrderCategoryKnowledgeDto(this WorkOrderCategoryKnowledge source)
        {
            WorkOrderCategoryKnowledgeDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryKnowledgeDto()
                {
                    Name = source.Knowledge.Name,
                    Priority = source.KnowledgeLevel.HasValue ? source.KnowledgeLevel.Value: 0,
                };
            }
            return result;
        }

        public static IList<WorkOrderCategoryKnowledgeDto> ToWorkOrderCategoryKnowledgeDto(this IList<WorkOrderCategoryKnowledge> source)
        {
            return source?.MapList(kp => kp.ToWorkOrderCategoryKnowledgeDto());
        }
    }
}