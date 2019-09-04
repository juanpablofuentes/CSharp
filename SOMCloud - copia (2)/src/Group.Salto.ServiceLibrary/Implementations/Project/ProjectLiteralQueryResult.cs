using Group.Salto.ServiceLibrary.Common.Contracts.LiteralPreconditions;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.LiteralPreconditions;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.Project
{
    public class ProjectLiteralQueryResult : IProjectLiteralQueryResult
    {
        private IProjectsService _projectsService;

        public ProjectLiteralQueryResult(IProjectsService projectsService)
        {
            _projectsService = projectsService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null ");
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues(IFilterQueryParameters filterQueryParameters)
        {
            return _projectsService.GetProjectFilteredUserId(((FilterQueryParametersBase)filterQueryParameters).UserId);
        }
    }
}