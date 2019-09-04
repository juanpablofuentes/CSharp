using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionsTypes;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Trigger
{
    public interface IPreconditionTypesService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
        PreconditionsTypesDto GetById(Guid id);
        PreconditionsTypesDto GetByName(string name);
        List<PreconditionsTypesDto> GetAll();
        IList<BaseNameIdDto<Guid>> GetAllKeyValuesByPrecondition(int preconditionId);
    }
}