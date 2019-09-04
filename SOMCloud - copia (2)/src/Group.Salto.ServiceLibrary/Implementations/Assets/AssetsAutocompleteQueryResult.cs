using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.ServiceLibrary.Common.Contracts.Assets;

namespace Group.Salto.ServiceLibrary.Implementations.AssetStatuses
{
    public class AssetsAutocompleteQueryResult : IAssetsAutocompleteQueryResult
    {
        private IAssetsRepository _assetsRepository;

        public AssetsAutocompleteQueryResult(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository ?? throw new ArgumentNullException($"{nameof(IAssetsRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterAdditionalQueryDto filterQuery = new FilterAdditionalQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active, Id = !string.IsNullOrEmpty(queryTypeParameters.Value) ? Convert.ToInt32(queryTypeParameters.Value) : 0 };
            return _assetsRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Key,
                    Name = x.Value ?? string.Empty
                }).ToList();
        }
    }
}