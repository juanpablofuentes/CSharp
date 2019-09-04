using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Contracts.AssetStatuses;
using Group.Salto.Infrastructure.Common.Dto;

namespace Group.Salto.ServiceLibrary.Implementations.AssetStatuses
{
    public class AssetStatusesAutocompleteQueryResult : IAssetStatusesAutocompleteQueryResult
    {
        private IAssetStatusesRepository _assetStatusesRepository;

        public AssetStatusesAutocompleteQueryResult(IAssetStatusesRepository assetStatusesRepository)
        {
            _assetStatusesRepository = assetStatusesRepository ?? throw new ArgumentNullException($"{nameof(IAssetStatusesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _assetStatusesRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}