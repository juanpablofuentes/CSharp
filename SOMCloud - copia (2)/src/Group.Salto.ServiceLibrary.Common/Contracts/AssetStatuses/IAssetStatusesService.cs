using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.AssetStatuses
{
    public interface IAssetStatusesService
    {
        ResultDto<AssetStatusesDto> GetById(int id);
        ResultDto<IList<AssetStatusesDto>> GetAllFiltered(AssetStatusesFilterDto filter);
        ResultDto<AssetStatusesDto> CreateAssetStatuses(AssetStatusesDto source);
        ResultDto<AssetStatusesDto> UpdateAssetStatuses(AssetStatusesDto source);
        ResultDto<bool> DeleteAssetStatuses(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
    }
}