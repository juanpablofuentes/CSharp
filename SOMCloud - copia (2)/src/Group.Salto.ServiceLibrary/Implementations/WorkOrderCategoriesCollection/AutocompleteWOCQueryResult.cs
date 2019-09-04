using Group.Salto.Common.Enums;
using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderCategoriesCollection
{
    public class AutocompleteWOCQueryResult : IAutocompleteWOCQueryResult
    {
        private IWorkOrderCategoriesRepository _workOrderCategoriesRepository;

        public AutocompleteWOCQueryResult(IWorkOrderCategoriesRepository workOrderCategoriesRepository)
        {
            _workOrderCategoriesRepository = workOrderCategoriesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesRepository)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetFiltered(QueryTypeParametersDto queryTypeParameters)
        {
            var categories = _workOrderCategoriesRepository.GetAllByName(queryTypeParameters.Text)
                .Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = $"{x.Name}"
                }).ToList();
            return categories;
        }
    }
}