using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Sites;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Sites
{
    public class SitesAutocompleteQueryResult : ISitesAutocompleteQueryResult
    {
        private ISitesRepository _sitesRepository;

        public SitesAutocompleteQueryResult(ISitesRepository sitesRepository)
        {
            _sitesRepository = sitesRepository ?? throw new ArgumentNullException($"{nameof(ISitesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            FilterAdditionalQueryDto filterQuery = new FilterAdditionalQueryDto() { Name = queryTypeParameters.Text, Active = ActiveEnum.Active, Id = !string.IsNullOrEmpty(queryTypeParameters.Value) ? Convert.ToInt32(queryTypeParameters.Value) : 0 };
            return _sitesRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = $"{x.Code} - {x.Name}"
                }).ToList();
        }
    }
}