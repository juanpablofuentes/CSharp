using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderCategories
{
    public class WorkOrderCategoriesLiteralQueryResult : IWorkOrderCategoriesLiteralQueryResult
    {
        private IWorkOrderCategoriesService _workOrderCategoriesService;

        public WorkOrderCategoriesLiteralQueryResult(IWorkOrderCategoriesService workOrderCategoriesService)
        {
            _workOrderCategoriesService = workOrderCategoriesService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            WoCategoryQueryParameters wocQuery = (WoCategoryQueryParameters)filterQueryParameters;
            return _workOrderCategoriesService.GetAllKeyValuesByProject(wocQuery.ProjectsIdsToMatch, wocQuery.UserId);
        }
    }
}