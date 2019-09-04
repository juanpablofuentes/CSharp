using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Linq;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.ServiceLibrary.Implementations.State
{
    public class StateAutocompleteQueryResult : IStateAutocompleteQueryResult
    {
        private readonly IStatesRepository _stateRepository;
        private readonly IWorkOrdersRepository _workOrdersRepository;

        public StateAutocompleteQueryResult(IStatesRepository stateRepository, IWorkOrdersRepository workOrdersRepository)
        {
            _stateRepository = stateRepository ?? throw new ArgumentNullException($"{nameof(IStatesRepository)} is null ");
            _workOrdersRepository = workOrdersRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrdersRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            IEnumerable<int> states = _workOrdersRepository.GetIdsForStatesInWorkOrders();
            StateFilterRepositoryDto filterQuery = new StateFilterRepositoryDto() { Name = queryTypeParameters.Text, Ids = states };
            return _stateRepository.GetAllByFilters(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}