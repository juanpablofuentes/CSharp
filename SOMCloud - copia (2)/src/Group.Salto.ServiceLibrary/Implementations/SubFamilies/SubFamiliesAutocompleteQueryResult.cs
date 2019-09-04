using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.SubFamilies;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.SubFamilies
{
    public class SubFamiliesAutocompleteQueryResult : ISubFamiliesAutocompleteQueryResult
    {
        private ISubFamiliesRepository _subFamiliesRepository;

        public SubFamiliesAutocompleteQueryResult(ISubFamiliesRepository subFamiliesRepository)
        {
            _subFamiliesRepository = subFamiliesRepository ?? throw new ArgumentNullException($"{nameof(ISubFamiliesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() {Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _subFamiliesRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Nom
                }).ToList();
        }
    }
}