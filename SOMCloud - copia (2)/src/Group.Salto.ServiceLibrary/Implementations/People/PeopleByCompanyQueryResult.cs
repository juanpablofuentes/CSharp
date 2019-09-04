using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleByCompanyQueryResult : IPeopleByCompanyQueryResult
    {
        private IPeopleRepository _peopleRepository;

        public PeopleByCompanyQueryResult(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            Int32.TryParse(queryTypeParameters.Value, out int companyId);
            var data = _peopleRepository.GetActiveByCompanyKeyValue(companyId);
            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}