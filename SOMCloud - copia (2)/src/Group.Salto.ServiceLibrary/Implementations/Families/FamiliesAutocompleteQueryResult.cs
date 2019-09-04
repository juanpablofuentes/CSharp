using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Families;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Families
{
    public class FamiliesAutocompleteQueryResult : IFamiliesAutocompleteQueryResult
    {
        private IFamiliesRepository _familiesRepository;

        public FamiliesAutocompleteQueryResult(IFamiliesRepository familiesRepository)
        {
            _familiesRepository = familiesRepository ?? throw new ArgumentNullException($"{nameof(IFamiliesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterQueryDto filterQuery = new FilterQueryDto() {Name = queryTypeParameters.Text, Active = ActiveEnum.Active };
            return _familiesRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}