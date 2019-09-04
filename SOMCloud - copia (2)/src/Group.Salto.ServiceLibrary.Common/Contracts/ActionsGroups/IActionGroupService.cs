using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ActionsGroups
{
    public interface IActionGroupService
    {
        Dictionary<Guid, string> GetAllKeyValues();
    }
}