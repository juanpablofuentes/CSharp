using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovementEndpoints;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WarehouseMovementEndpointsDtoExtensions
    {
        public static WarehouseMovementEndpointsDto ToDto(this WarehouseMovementEndpoints source)
        {
            WarehouseMovementEndpointsDto result = null;
            if (source != null)
            {
                result = new WarehouseMovementEndpointsDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Asset = new KeyValuePair<int?, string>(source.AssetId, source.Asset?.Name),
                    Warehouse = new KeyValuePair<int?, string>(source.WarehouseId, source.Warehouse.Name)
                };     
            }
            return result;
        }

        public static WarehouseMovementEndpoints ToEntity(this WarehouseMovementEndpointsDto source)
        {
            WarehouseMovementEndpoints result = null;
            if (source != null)
            {
                result = new WarehouseMovementEndpoints
                {
                    Id = source.Id,
                    Name = source.Name,
                    AssetId = source.Asset.Key,
                    WarehouseId = source.Warehouse.Key
                };
            }
            return result;
        }
    }
}