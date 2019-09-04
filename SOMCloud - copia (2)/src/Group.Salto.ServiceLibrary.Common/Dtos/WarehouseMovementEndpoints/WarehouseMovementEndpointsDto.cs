using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovementEndpoints
{
    public class WarehouseMovementEndpointsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public KeyValuePair<int?,string> Warehouse { get; set; }
        public KeyValuePair<int?,string> Asset { get; set; }
    }
}