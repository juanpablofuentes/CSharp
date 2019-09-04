using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Brands;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Brands
{
    public class BrandsAutocompleteQueryResult : IBrandsAutocompleteQueryResult
    {
        private IBrandsRepository _brandsRepository;

        public BrandsAutocompleteQueryResult(IBrandsRepository brandsRepository)
        {
            _brandsRepository = brandsRepository ?? throw new ArgumentNullException($"{nameof(IBrandsRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _brandsRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}