using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.ClosingCode;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks
{
    public class PredefinedServiceTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TaskCollectionsServiceExtraFieldDto CollectionsExtraField { get; set; }
        public IEnumerable<ClosingCodeApiDto> ClosingCodes { get; set; }
        public bool ClosingCodeIsMandatory { get; set; }
    }
}
