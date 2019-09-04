using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Queue
{
    public class QueueAutocompleteQueryResult : IQueueAutocompleteQueryResult
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IPeoplePermissionsRepository _peoplePermissionsRepository;
        private readonly IPeopleRepository _peopleRepository;

        public QueueAutocompleteQueryResult(IQueueRepository queueRepository, IPeoplePermissionsRepository peoplePermissionsRepository, IPeopleRepository peopleRepository)
        {
            _queueRepository = queueRepository ?? throw new ArgumentNullException($"{nameof(IQueueRepository)} is null ");
            _peoplePermissionsRepository = peoplePermissionsRepository ?? throw new ArgumentNullException($"{nameof(IPeoplePermissionsRepository)} is null ");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var people = _peopleRepository.GetByConfigId(Convert.ToInt32(queryTypeParameters.Value));
            int[] permisions = _peoplePermissionsRepository.GetPermissionsIdByPeopleId(people.Id);
            PermisionsFilterQueryDto filterQuery = new PermisionsFilterQueryDto() { Name = queryTypeParameters.Text, Persmisions = permisions };

            return _queueRepository.GetAllByFiltersWithPermisions(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = !x.QueuesTranslations.Any(t => t.LanguageId == queryTypeParameters.LanguageId) ? x.Name : x.QueuesTranslations.FirstOrDefault(t => t.LanguageId == queryTypeParameters.LanguageId).NameText,
                }).ToList();
        }
    }
}