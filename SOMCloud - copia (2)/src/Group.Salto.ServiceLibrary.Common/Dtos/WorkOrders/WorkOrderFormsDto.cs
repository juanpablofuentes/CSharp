using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class WorkOrderFormsDto
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
        public string PredefinedService { get; set; }
        public string Status { get; set; }
        public string DeliveryNote { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianSurname { get; set; }
        public string Observations { get; set; }
        public IList<WorkOrderExtraFieldsValuesDto> ExtraFieldsValues { get; set; }
        public DateTime DateForOrder { get; set; }
        public bool IsFather { get; set; }
        public bool IsEditable { get; set; } = true;
        public bool IsWOClosed { get; set; }
        public bool WOBillable { get; set; }
        public bool HasBill { get; set; }
        public Mobility.Dto.Enums.BillStatus? BillState { get; set; }
        public bool HasWOAnalisis { get; set; }
        public int? TaskId { get; set; }
        public int? WorkOrderId { get; set; }
        public bool IsSystemForm { get; set; }

        public bool IsClosedOrderAccounting()
        {
            return (IsWOClosed && WOBillable);
        }

        public bool IsWorkOrderOpen()
        {
            return (!IsClosedOrderAccounting() && !HasWOAnalisis);
        }

        public bool IsClosedOrder()
        {
            return (!IsClosedOrderAccounting() && HasWOAnalisis);
        }
    }
}