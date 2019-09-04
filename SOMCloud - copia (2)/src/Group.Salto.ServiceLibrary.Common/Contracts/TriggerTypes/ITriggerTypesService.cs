using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Trigger;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.TriggerTypes
{
    public interface ITriggerTypesService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
        TriggerTypeDto GetTriggerTypeById(Guid id);
        TriggerTypeDto GetTriggerTypeByName(string name);
    }
}