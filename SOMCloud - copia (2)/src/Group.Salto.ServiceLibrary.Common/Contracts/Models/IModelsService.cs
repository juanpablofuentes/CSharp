using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Models
{
    public interface IModelsService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<int>> FilterByBrand(QueryCascadeDto queryCascadeDto);
    }
}