using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderTypeDtoExtensions
    {
        public static List<WorkOrderTypeDto> GetClosingCodesDtoTree(this List<WorkOrderTypes> list, int? parent)
        {
            return list?.Where(x => x.WorkOrderTypesFatherId == parent).Select(x => new WorkOrderTypeDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Serie = x.Serie,
                Childs = GetClosingCodesDtoTree(list, x.Id)
            }).ToList();
        }

        public static List<WorkOrderTypeFatherDto> GetWorkOrderTypes(this List<WorkOrderTypes> list, int? parent)
        {
            var data = list?.Where(x => x.WorkOrderTypesFatherId == parent).Select(x => new WorkOrderTypeFatherDto
            {
                Id = x.Id,
                Name = x.Name,
                FatherId = x.WorkOrderTypesFatherId.HasValue ? x.WorkOrderTypesFatherId.Value : (int?) null,
                Childs = GetWorkOrderTypes(list, x.Id)
            }).ToList();

            return data;
        }

        public static WorkOrderTypes ToEntity(this WorkOrderTypeDto source, CollectionsTypesWorkOrders parent)
        {
            WorkOrderTypes result = null;
            if (source != null)
            {
                result = new WorkOrderTypes()
                {
                    Name = source.Name,
                    Description = source.Description,
                    InverseWorkOrderTypesFather = source.Childs.ToEntity(parent),
                    CollectionsTypesWorkOrders = parent,
                    Serie = source.Serie,
                };
            }

            return result;
        }

        public static IList<WorkOrderTypes> ToEntity(this IList<WorkOrderTypeDto> source, CollectionsTypesWorkOrders parent)
        {
            return source?.MapList(x => x.ToEntity(parent));
        }
    }
}