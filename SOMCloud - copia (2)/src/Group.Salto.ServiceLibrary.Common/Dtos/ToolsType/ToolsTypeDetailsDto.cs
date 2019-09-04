using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ToolsType
{
    public class ToolsTypeDetailsDto :ToolsTypeDto
    {
        public IList<ToolsTypeKnowledgeDto> KnowledgeSelected { get; set; }
    }
}
