using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PostconditionsTypes;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Postcondition
{
    public interface IPostconditionTypesService
    {
        IList<BaseNameIdDto<Guid>> GetAllKeyValuesByPostcondition(int id);
        List<PostconditionsTypesDto> GetAll();
        PostconditionsTypesDto GetPostconditionsTypeByName(string name);
        IList<BaseNameIdDto<Guid>> GetAllKeyValues();
    }
}