using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Sla
{
    public class SlaBasicInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SlaStateBasicInfoDto> StatesSla { get; set; }
    }
}
