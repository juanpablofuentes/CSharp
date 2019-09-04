using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Clients;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Clients
{
    public class ClientAutocompleteQueryResult : IClientsAutocompleteQueryResult
    {
        private IClientRepository _clientRepository;

        public ClientAutocompleteQueryResult(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException($"{nameof(IClientRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
           PermisionsFilterQueryDto filterQuery = new PermisionsFilterQueryDto() { Name = queryTypeParameters.Text};

            return _clientRepository.GetAllByFiltersWithPermisions(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.CorporateName
                }).ToList();
        }
    }
}