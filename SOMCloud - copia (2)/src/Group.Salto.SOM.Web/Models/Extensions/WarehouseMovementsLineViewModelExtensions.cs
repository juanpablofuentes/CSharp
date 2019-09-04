using Group.Salto.Common.Constants.Warehouses;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovementEndpoints;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements;
using Group.Salto.SOM.Web.Models.WarehouseMovements;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WarehouseMovementsLineViewModelExtensions
    {
        public static IList<WarehouseMovementsLineViewModel> ToViewModel(this IList<WarehouseMovementsDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        } 

        public static WarehouseMovementsLineViewModel ToViewModel(this WarehouseMovementsDto source)
        {
            WarehouseMovementsLineViewModel result = null;
            if (source != null)
            {
                result = new WarehouseMovementsLineViewModel
                {
                    Id = source.Id,
                    ItemName = source.Item,
                    Quantity = source.Quantity,
                    RegistryDate = source.RegistryDate,
                    ServiceId = source.ServicesId,
                    SerialNumber = source.SerialNumber,
                    WorkOrderId = source.WorkOrdersId,
                    MovementFromName = source.MovementFrom?.Name,
                    MovementToName = source.MovementTo?.Name,
                    MovementFromType = GetMovementType(source.MovementFrom),
                    MovementToType = GetMovementType(source.MovementTo)
                };
            }
            return result;
        }

        public static string GetMovementType(WarehouseMovementEndpointsDto endpoint) 
        {
            return (endpoint.Warehouse.Value == null) ? WarehousesConstants.WarehousesMovementEndpointTypeWarehouse
                : (endpoint.Asset.Value == null) ? WarehousesConstants.WarehousesMovementEndpointTypeAsset
                : WarehousesConstants.WarehousesMovementEndpointTypeOther;
        }
    }
}