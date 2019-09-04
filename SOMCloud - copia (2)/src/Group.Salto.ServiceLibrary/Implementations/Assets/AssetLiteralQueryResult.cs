using Group.Salto.ServiceLibrary.Common.Contracts.Assets;
using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Assets
{
    public class AssetLiteralQueryResult : IAssetLiteralQueryResult
    {
        private IAssetsService _assetsService;

        public AssetLiteralQueryResult(IAssetsService assetsService)
        {
            _assetsService = assetsService ?? throw new ArgumentNullException($"{nameof(IAssetsService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _assetsService.GetAllKeyValuesByLocationFinalClient(((AssetQueryParameters)filterQueryParameters).LocationFinalClientIdsToMatch);
        }
    }
}