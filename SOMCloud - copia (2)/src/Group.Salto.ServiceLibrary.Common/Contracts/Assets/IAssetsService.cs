using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Assets
{
    public interface IAssetsService
    {
        ResultDto<IList<AssetsListDto>> GetAllFiltered(AssetsFilterDto filter);
        ResultDto<AssetDetailsDto> GetById(int id);
        ResultDto<AssetForWorkOrderDetailsDto> GetForWorkOrderEditById(int id);
        ResultDto<AssetDetailsDto> Create(AssetDetailsDto model);
        ResultDto<AssetDetailsDto> Update(AssetDetailsDto model);
        ResultDto<bool> Transfer(AssetsTransferDto model);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<AssetDetailsDto> GetAssetPartialDetailBySiteIdWithFinalClient(int id);
        ResultDto<AssetsLocationsDto> GetLocationsAndUserSiteById(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        IList<BaseNameIdDto<int>> GetAllKeyValuesByLocationFinalClient(List<int> IdsToMatch);
    }
}