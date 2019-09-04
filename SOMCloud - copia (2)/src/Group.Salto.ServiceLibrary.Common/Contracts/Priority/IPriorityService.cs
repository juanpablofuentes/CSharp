using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Priority
{
    public interface IPriorityService
    {
        IList<BaseNameIdDto<int>> GetBasePriorities();
    }
}