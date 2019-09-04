using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using Group.Salto.SOM.Web.Models.WorkOrderType;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderTypeViewModelExtensions
    {
        public static WorkOrderTypeDetailViewModel ToViewModel(this WorkOrderTypeDto source)
        {
            WorkOrderTypeDetailViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderTypeDetailViewModel()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Serie = source.Serie,
                    Childs = source.Childs.ToViewModel()
                };
            }

            return result;
        }

        public static IList<WorkOrderTypeDetailViewModel> ToViewModel(this IList<WorkOrderTypeDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }


        public static WorkOrderTypeDto ToDto(this WorkOrderTypeDetailViewModel source)
        {
            WorkOrderTypeDto result = null;
            if (source != null)
            {
                result = new WorkOrderTypeDto()
                {
                    Name = source.Name,
                    Id = source.IdClonedItem == 0 ? 0 : source.Id,
                    Description = source.Description,
                    Serie = source.Serie == LocalizationsConstants.Undefined ? null : source.Serie,
                    Childs = source.Childs.ToDto()
                };
            }

            return result;
        }

        public static IList<WorkOrderTypeDto> ToDto(this IList<WorkOrderTypeDetailViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}