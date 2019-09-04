using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderCategories
{
    public class WorkOrderCategoryAutocompleteQueryResult : IWorkOrderCategoryAutocompleteQueryResult
    {
        private readonly IWorkOrderCategoriesRepository _workOrderCategoriesRepository;
        private readonly IPeoplePermissionsRepository _peoplePermissionsRepository;
        private readonly IPeopleRepository _peopleRepository;

        public WorkOrderCategoryAutocompleteQueryResult(IWorkOrderCategoriesRepository workOrderCategoriesRepository, IPeoplePermissionsRepository peoplePermissionsRepository, IPeopleRepository peopleRepository)
        {
            _workOrderCategoriesRepository = workOrderCategoriesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesRepository)} is null ");
            _peoplePermissionsRepository = peoplePermissionsRepository ?? throw new ArgumentNullException($"{nameof(IPeoplePermissionsRepository)} is null ");
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var people = _peopleRepository.GetByConfigId(Convert.ToInt32(queryTypeParameters.Value));
            int[] permisions = _peoplePermissionsRepository.GetPermissionsIdByPeopleId(people.Id);
            PermisionsFilterQueryDto filterQuery = new PermisionsFilterQueryDto() { Name = queryTypeParameters.Text, Persmisions = permisions };

            return _workOrderCategoriesRepository.GetAllByFiltersWithPermisions(filterQuery)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
        }
    }
}