using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ProjectsRelatedDtoExtensions
    {
        public static ProjectRelatedInfoDto ToRelatedDto(this Projects source, Dictionary<int, string> workOrderCategories, List<WorkOrderTypes> workOrderTypes)
        {
            ProjectRelatedInfoDto result = null;
            if (source != null)
            {
                result = new ProjectRelatedInfoDto()
                {
                    Id = source.Id,
                    QueueId = source.QueuetId.HasValue ? source.QueuetId.Value : 0,
                    WorkOrderStatusId = source.WorkOrderStatusesId.HasValue ? source.WorkOrderStatusesId.Value : 0,
                    WorkOrderCategories = workOrderCategories.ToBaseNameId(),
                    WorkOrderTypes = workOrderTypes.GetWorkOrderTypes(null),
                };
            }
            return result;
        }
    }
}