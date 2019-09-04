using System.Collections.Generic;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderEquipmentDtoExtensions
    {
        public static WorkOrderEquipmentDto ToWorkOrderEquipmentDto(this WorkOrders dbModel)
        {
            var dto = new WorkOrderEquipmentDto
            {
                Id = dbModel.Id,
                TextRepair = dbModel.TextRepair,
                Observations = dbModel.Observations,
                ActionDate = dbModel.ActionDate,
                WoServices = dbModel.Services?.ToWoServiceDto()
            };
            return dto;
        }

        public static IEnumerable<WorkOrderEquipmentDto> ToWorkOrderEquipmentDto(this IEnumerable<WorkOrders> dbModelList)
        {
            var listDto = new List<WorkOrderEquipmentDto>();
            foreach (var dbModel in dbModelList)
            {
                listDto.Add(dbModel?.ToWorkOrderEquipmentDto());
            }
            return listDto;
        }
    }
}
