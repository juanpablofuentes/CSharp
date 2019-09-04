using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Implementations.Project
{
    public class ProjectRelatedInfoAdapter: BaseService, IProjectRelatedInfoAdapter
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkOrderCategoriesRepository _workOrderCategoriesRepository;
        private readonly IWorkOrderTypesRepository _workOrderTypesRepository;

        public ProjectRelatedInfoAdapter(ILoggingService logginingService,
            IProjectRepository projectRepository,
            IWorkOrderCategoriesRepository workOrderCategoriesRepository,
            IWorkOrderTypesRepository workOrderTypesRepository) : base( logginingService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(IProjectRepository));
            _workOrderCategoriesRepository = workOrderCategoriesRepository ?? throw new ArgumentNullException(nameof(IWorkOrderCategoriesRepository));
            _workOrderTypesRepository = workOrderTypesRepository ?? throw new ArgumentNullException(nameof(IWorkOrderTypesRepository));
        }

        public ResultDto<ProjectRelatedInfoDto> GetProjectRelatedInfo(int id)
        {
            Projects project = _projectRepository.GetById(id);
            Dictionary<int, string> workOrderCategories = _workOrderCategoriesRepository.GetAllKeyValuesByWorkOrderCategoriesCollectionId(project.WorkOrderCategoriesCollectionId);
            List<WorkOrderTypes> workOrderTypes = _workOrderTypesRepository.GetByCollectionsTypesWorkOrdersId(project.CollectionsTypesWorkOrdersId);
            return ProcessResult(project.ToRelatedDto(workOrderCategories, workOrderTypes));
        }
    }
}