using Group.Salto.Entities.Tenant;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.TemplateProcessor
{
    public class TemplateProcessorValuesDto
    {
        public string Template { get; set; }
        public WorkOrders WorkOrder { get; set; }
        public Services Service { get; set; }
        public string NullStringValue { get; set; }
        public bool NcalcEncapsulate { get; set; }
    }
}
