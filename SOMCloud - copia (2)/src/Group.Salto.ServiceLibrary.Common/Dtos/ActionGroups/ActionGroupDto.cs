using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;

namespace Group.Salto.ServiceLibrary.Common.Dtos.ActionGroups
{
    public class ActionGroupDto : BaseNameIdDto<Guid>
    {
        public string Description { get; set; }
    }
}