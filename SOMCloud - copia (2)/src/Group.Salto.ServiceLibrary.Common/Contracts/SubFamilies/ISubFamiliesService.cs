using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.SubFamilies
{
    public interface ISubFamiliesService
    {
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<int>> FilterByClientSite(QueryCascadeDto queryCascadeDto);
    }
}