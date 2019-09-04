using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Tools
{
    public class ToolsDetailsDto :ToolsDto
    {
        public IList<BaseNameIdDto<int>> Types { get; set; }
    }
}
