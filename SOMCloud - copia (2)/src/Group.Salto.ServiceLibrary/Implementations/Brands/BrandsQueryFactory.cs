using Group.Salto.ServiceLibrary.Common.Contracts.Brands;
using Group.Salto.ServiceLibrary.Common.Contracts.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Brands
{
    public class BrandsQueryFactory : IBrandsQueryFactory
    { 
        private IDictionary<QueryTypeEnum, Func<IQueryResult>> _servicesQuery;
        private readonly IServiceProvider _services;

        public BrandsQueryFactory(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException($"{nameof(services)} is null ");
            _servicesQuery = new Dictionary<QueryTypeEnum, Func<IQueryResult>>();
            _servicesQuery.Add(QueryTypeEnum.Autocomplete, () => _services.GetService<IBrandsAutocompleteQueryResult>());
        }

        public IQueryResult GetQuery(QueryTypeEnum queryType)
        {
            return _servicesQuery[queryType]() ?? throw new NotImplementedException();
        }
    }
}