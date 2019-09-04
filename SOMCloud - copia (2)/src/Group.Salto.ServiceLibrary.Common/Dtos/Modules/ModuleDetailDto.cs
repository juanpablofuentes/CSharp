using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Modules
{
    public class ModuleDetailDto : ModuleDto
    {
        public IList<Guid> ActionGroupsSelected { get; set; }
    }
}