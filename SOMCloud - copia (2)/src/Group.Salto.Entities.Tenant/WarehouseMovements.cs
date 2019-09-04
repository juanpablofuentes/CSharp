using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class WarehouseMovements : BaseEntity
    {
        public int ItemsId { get; set; }
        public Guid WarehouseMovementTypeId { get; set; }
        public int? WorkOrdersId { get; set; }
        public int? ServicesId { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public int EndpointsFromId { get; set; }
        public int EndpointsToId { get; set; }

        public Items Items { get; set; }
        public ItemsSerialNumber ItemsSerialNumber { get; set; }
        public WorkOrders WorkOrders { get; set; }
        public Services Services { get; set; }
        public WarehouseMovementEndpoints EndpointsFrom { get; set; }
        public WarehouseMovementEndpoints EndpointsTo { get; set; }
    }
}