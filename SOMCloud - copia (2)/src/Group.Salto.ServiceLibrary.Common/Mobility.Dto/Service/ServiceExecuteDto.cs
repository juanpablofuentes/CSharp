using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class ServiceExecuteDto
    {
        public int PredefinedServiceId { get; set; }
        public string Observations { get; set; }
        public int ClosingCodeId { get; set; }
        public IEnumerable<ExtraFieldValueAddDto> ExtraFieldsValues { get; set; }
    }
}
