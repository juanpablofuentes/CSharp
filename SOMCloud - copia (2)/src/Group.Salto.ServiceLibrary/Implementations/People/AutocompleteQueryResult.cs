using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Enums;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class AutocompleteQueryResult : IAutocompleteQueryResult
    {
        private IPeopleRepository _peopleRepository;

        public AutocompleteQueryResult(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var people = _peopleRepository.GetAllByFilters(queryTypeParameters.Text, ActiveEnum.Active)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = $"{x.Name} {x.FisrtSurname}"
                }).ToList();
            return people;
        }
    }
}