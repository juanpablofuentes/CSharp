using System;

namespace Group.Salto.SOM.Web.Models.WarehouseMovements
{
    public class WarehouseMovementsLineViewModel
    {
        public int Id { get; set; }
        public DateTime RegistryDate { get; set; }
        public string ItemName { get; set; }
        public string TypeName { get; set; }
        public int? WorkOrderId { get; set; }
        public int? ServiceId { get; set; }
        public string SerialNumber { get; set; }
        public int Quantity { get; set; }
        public string MovementFromName { get; set; }
        public string MovementFromType { get; set; }
        public string MovementToName { get; set; }
        public string MovementToType { get; set; }        
    }
}