using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderFormsViewModel : FormState
    {
        public int Id { get; set; }
        public string CreationDate { get; set; }
        public string PrefinedService { get; set; }
        public string Status { get; set; }
        public string DeliveryNote { get; set; }
        public string TechnicianName { get; set; }
        public string TechnicianSurname { get; set; }
        public string Observations { get; set; }
        public bool IsFather { get; set; }
        public bool IsEditable { get; set; }
        public IList<WorkOrderExtraFieldsValuesViewModel> ExtraFieldValues { get; set; }
    }
}