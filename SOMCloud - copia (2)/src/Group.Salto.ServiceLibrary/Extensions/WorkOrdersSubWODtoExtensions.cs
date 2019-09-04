using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrdersSubWODtoExtensions
    {
        public static WorkOrdersSubWODto ToListDto(this WorkOrders source)
        {
            WorkOrdersSubWODto result = null;
            if (source != null)
            {
                result = new WorkOrdersSubWODto()
                {
                    Id = source.Id,
                    ResolutionDateSla = source.ResolutionDateSla?.ToString(),
                    TimingCreation = source.AssignmentTime?.ToString(),
                    InternalIdentifier = source.InternalIdentifier,
                    WorkOrderStatus = source.WorkOrderStatus?.Name,
                    ActionDate = source.ActionDate?.ToString(),
                    PeopleResponsible = source.PeopleResponsible?.Name,
                    WorkOrderCategory = source.WorkOrderCategory?.Name,
                    Project = source.Project?.Name,
                    Queue = source.Queue?.Name,
                    TimingAssigned = source.AssignmentTime?.ToString(),
                };
            }
            return result;
        }

        public static IList<WorkOrdersSubWODto> ToListDto(this IList<WorkOrders> source)
        {
            return source?.MapList(x => x.ToListDto());
        }
    }
}