using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus;
using Group.Salto.SOM.Web.Models.WorkOrderStatus;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderStatusFilterViewModelExtensions
    {
        public static WorkOrderStatusFilterDto ToDto(this WorkOrderStatusFilterViewModel source)
        {
            WorkOrderStatusFilterDto result = null;
            if (source != null)
            {
                result = new WorkOrderStatusFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    LanguageId = source.LanguageId,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new WorkOrderStatusFilterDto();
        }

        public static WorkOrderStatusListViewModel ToViewModel(this WorkOrderStatusListDto source)
        {
            WorkOrderStatusListViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderStatusListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return result;
        }

        public static IList<WorkOrderStatusListViewModel> ToViewModel(this IList<WorkOrderStatusListDto> source)
        {
            return source?.MapList(wc => wc.ToViewModel());
        }
    }
}