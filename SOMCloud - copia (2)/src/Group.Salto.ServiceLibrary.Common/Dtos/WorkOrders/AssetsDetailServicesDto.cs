using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders
{
    public class AssetsDetailServicesDto
    {
        public string ServiceId { get; set; }
        public string Status { get; set; }
        public string FormState { get; set; }
        public string DeliveryNumber { get; set; }
        public string ResponsibleName { get; set; }
        public IList<WorkOrderExtraFieldsValuesDto> ExtraFieldsValues { get; set; }
    }
}