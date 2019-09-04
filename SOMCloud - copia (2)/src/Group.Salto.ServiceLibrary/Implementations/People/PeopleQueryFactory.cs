using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.People
{
    public class PeopleQueryFactory : IPeopleQueryFactory 
    {
        private IDictionary<QueryTypeEnum, Func<IQueryResult>> _servicesQuery;
        private readonly IServiceProvider _services;

        public PeopleQueryFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException($"{nameof(services)} is null ");
            _servicesQuery = new Dictionary<QueryTypeEnum, Func<IQueryResult>>();
            _servicesQuery.Add(QueryTypeEnum.Autocomplete, () => _services.GetService<IAutocompleteQueryResult>());
            _servicesQuery.Add(QueryTypeEnum.GetPeopleByCompany, () => _services.GetService<IPeopleByCompanyQueryResult>());
            _servicesQuery.Add(QueryTypeEnum.GetPeopleWithoutCompanyAndSubContractor, () => _services.GetService<IPeopleWithoutCompanyAndSubContractorQueryResult>());
            _servicesQuery.Add(QueryTypeEnum.GetPeopleTechnicians, () => _services.GetService<IPeopleTechnicians>());
        }

        public IQueryResult GetQuery(QueryTypeEnum queryType)
        {
            return _servicesQuery[queryType]() ?? throw new NotImplementedException();
        }
    }
}