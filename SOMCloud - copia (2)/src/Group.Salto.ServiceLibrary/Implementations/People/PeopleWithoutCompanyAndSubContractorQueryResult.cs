using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Enums;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleWithoutCompanyAndSubContractorQueryResult : IPeopleWithoutCompanyAndSubContractorQueryResult
    {
        private readonly IPeopleRepository _peopleRepository;
        public PeopleWithoutCompanyAndSubContractorQueryResult(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var people = _peopleRepository.GetAllByFilters(queryTypeParameters.Text, ActiveEnum.Active)
                .Where(x=>x.CompanyId == null 
                          && (x.SubcontractId == null || x.SubcontractId.ToString() == queryTypeParameters.Value))
                .Select(x=>new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = $"{x.Name} {x.FisrtSurname}"
                }).ToList();
            return people;
        }
    }
}