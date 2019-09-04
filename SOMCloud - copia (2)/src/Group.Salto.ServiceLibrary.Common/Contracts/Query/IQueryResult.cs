using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Query
{
    public interface IQueryResult
    {
        IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters);       
    }
}