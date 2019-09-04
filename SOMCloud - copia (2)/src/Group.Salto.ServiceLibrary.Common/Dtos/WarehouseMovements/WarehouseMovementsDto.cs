using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovementEndpoints;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements
{
    public class WarehouseMovementsDto
    {
        public int Id { get; set; }
        public DateTime RegistryDate { get; set; }
        public string Item { get; set; }
        public int ItemsId { get; set; }
        public KeyValuePair<Guid,string> WarehouseMovementType { get; set; }
        public int? WorkOrdersId { get; set; }
        public int? ServicesId { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public WarehouseMovementEndpointsDto MovementFrom { get; set; }
        public WarehouseMovementEndpointsDto MovementTo { get; set; }
    }
}