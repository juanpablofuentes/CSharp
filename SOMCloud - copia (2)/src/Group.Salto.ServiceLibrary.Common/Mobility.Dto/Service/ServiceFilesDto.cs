using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.File;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class ServiceFilesDto
    {
        public IEnumerable<RequestFileDto> Files { get; set; }
        public int ExtraFieldId { get; set; }
    }
}
