using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.TasksTypes
{
    public interface ITasksTypesService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
    }
}