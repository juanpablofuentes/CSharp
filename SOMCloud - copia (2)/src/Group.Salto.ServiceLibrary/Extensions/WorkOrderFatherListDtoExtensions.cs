using System.Collections.Generic;
using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrderType;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderFatherListDtoExtensions
    {
        public static IEnumerable<WorkOrderFatherListDto> ToWorkOrderFatherListDto(this WorkOrderTypes dbModel, List<WorkOrderFatherListDto> list = null)
        {
            var listFatherTypes = new List<WorkOrderFatherListDto>();
            if (list != null)
            {
                listFatherTypes = list;
            }

            listFatherTypes.Insert(0, new WorkOrderFatherListDto
            {
                Name = dbModel.Name,
                Id = dbModel.Id,
                Description = dbModel.Description,
                Serie = dbModel.Serie
            });
            if (dbModel.WorkOrderTypesFather != null)
            {
                listFatherTypes = dbModel.WorkOrderTypesFather?.ToWorkOrderFatherListDto(listFatherTypes).ToList();
            }

            return listFatherTypes;
        }
    }
}
