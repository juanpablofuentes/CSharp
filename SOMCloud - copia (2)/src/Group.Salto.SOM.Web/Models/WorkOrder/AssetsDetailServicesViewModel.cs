using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class AssetsDetailServicesViewModel
    {
        public string ServiceId { get; set; }
        public string Status { get; set; }
        public string FormState { get; set; }
        public string DeliveryNumber { get; set; }
        public string ResponsibleName { get; set; }
        public IList<WorkOrderExtraFieldsValuesDto> ExtraFieldValues { get; set; }
    }
}