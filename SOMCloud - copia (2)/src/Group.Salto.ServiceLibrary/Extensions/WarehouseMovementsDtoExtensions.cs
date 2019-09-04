using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WarehouseMovementsDtoExtensions
    {
        public static WarehouseMovementsDto ToDto(this WarehouseMovements source)
        {
            WarehouseMovementsDto result = null;
            if (source != null)
            {
                result = new WarehouseMovementsDto
                {
                    Id = source.Id,
                    ItemsId = source.ItemsId,
                    Item = source.Items?.Name,
                    Quantity = source.Quantity,
                    RegistryDate = source.UpdateDate,
                    SerialNumber = source.SerialNumber,
                    ServicesId = source.ServicesId,
                    WorkOrdersId = source.WorkOrdersId,
                    MovementFrom = source.EndpointsFrom.ToDto(),
                    MovementTo = source.EndpointsTo.ToDto()
                };
                result.WarehouseMovementType = new KeyValuePair<System.Guid, string>(source.WarehouseMovementTypeId, string.Empty);
            }
            return result;
        }

        public static IList<WarehouseMovementsDto> ToListDto(this IQueryable<WarehouseMovements> source)
        {
            return source?.MapList(x => x.ToDto());            
        }

        public static WarehouseMovements ToEntity(this WarehouseMovementsDto source)
        {
           WarehouseMovements result = null;
            if (source != null)
            {
                result = new WarehouseMovements
                {
                    Id = source.Id,
                    ItemsId = source.ItemsId,
                    Quantity = source.Quantity,
                    UpdateDate = source.RegistryDate,
                    SerialNumber = source.SerialNumber,
                    ServicesId = source.ServicesId,
                    WorkOrdersId = source.WorkOrdersId,
                    EndpointsFrom = source.MovementFrom.ToEntity(),
                    EndpointsTo = source.MovementTo.ToEntity(),
                    WarehouseMovementTypeId = source.WarehouseMovementType.Key
                };
            }
            return result;
        }
    }
}