using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Project
{
    public class ProjectAutocompleteQueryResult : IProjectAutocompleteQueryResult
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPeoplePermissionsRepository _peoplePermissionsRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ProjectAutocompleteQueryResult(IProjectRepository projectRepository, IPeoplePermissionsRepository peoplePermissionsRepository, IPeopleRepository peopleRepository)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException($"{nameof(IProjectRepository)} is null ");
            _peoplePermissionsRepository = peoplePermissionsRepository ?? throw new ArgumentNullException($"{nameof(IPeoplePermissionsRepository)} is null ");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var people = _peopleRepository.GetByConfigId(Convert.ToInt32(queryTypeParameters.Value));
            int[] permisions = _peoplePermissionsRepository.GetPermissionsIdByPeopleId(people.Id);
            PermisionsFilterQueryDto filterQuery = new PermisionsFilterQueryDto() { Name = queryTypeParameters.Text, Persmisions = permisions };

            return _projectRepository.GetAllByFiltersWithPermisions(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}