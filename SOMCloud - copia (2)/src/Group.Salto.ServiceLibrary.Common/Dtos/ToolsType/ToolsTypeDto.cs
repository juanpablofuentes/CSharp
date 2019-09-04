using System;
using System.Collections.Generic;

using System.Text;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ToolsType
{
    public class ToolsTypeDto: BaseNameIdDto<int>
    {
        public string Description { get; set; }
        public string Observations { get; set; }
    }
}