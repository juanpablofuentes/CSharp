using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.Entities.Tenant;

namespace Group.Salto.ServiceLibrary.Implementations.Permisions
{
    public class PermissionsService : BaseService, IPermissionsService
    {
        private readonly IQueueRepository _queueRepository;
        private readonly IWorkOrderCategoriesRepository _wordkOrderCategoriesRepository;
        private readonly IPredefinedServiceRepository _predefinedServiceRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPermissionsRepository _permissionsRepository;

        public PermissionsService(ILoggingService logginingService,
                                    IQueueRepository queueRepository,
                                    IWorkOrderCategoriesRepository wordkOrderCategoriesRepository,
                                    IPredefinedServiceRepository predefinedServiceRepository,
                                    IPeopleRepository peopleRepository,
                                    ITaskRepository taskRepository,
                                    IProjectRepository projectRepository,
                                    IPermissionsRepository permissionsRepository) : base(logginingService)
        {
            _queueRepository = queueRepository ?? throw new ArgumentNullException($"{nameof(IQueueRepository)} is null "); 
            _wordkOrderCategoriesRepository = wordkOrderCategoriesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesRepository)} is null ");;
            _predefinedServiceRepository = predefinedServiceRepository ?? throw new ArgumentNullException($"{nameof(IPredefinedServiceRepository)} is null "); 
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException($"{nameof(IPeopleRepository)} is null "); 
            _taskRepository = taskRepository ?? throw new ArgumentNullException($"{nameof(ITaskRepository)} is null "); 
            _projectRepository = projectRepository ?? throw new ArgumentNullException($"{nameof(IProjectRepository)} is null "); 
            _permissionsRepository = permissionsRepository ?? throw new ArgumentNullException($"{nameof(IPermissionsRepository)} is null ");
        }

        public ResultDto<IList<PermissionsDto>> GetAllFiltered(PermissionsFilterDto filter)
        {
            var query = _permissionsRepository.GetAllNotDeleted();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = query.WhereIfNotDefault(filter.Observations, au => au.Observations.Contains(filter.Description));

            var data = query.MapList(x => x.ToDto());
            data = OrderBy(data.AsQueryable(), filter).ToList();
            return ProcessResult(data);
        }

        public IEnumerable<PermissionsDto> GetAllKeyValues()
        {
            LogginingService.LogInfo($"PermissionsService->GetAllForPeople");
            IEnumerable<PermissionsDto> result = _permissionsRepository.GetAllKeyValues().ToMultiSelectListDto();

            return result;
        }

        public ResultDto<PermissionDetailDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Permission Id");
            var data = _permissionsRepository.GetByIdNotDeleted(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<PermissionDetailDto> Create(PermissionDetailDto model)
        {
            var entity = model.ToEntity();
            entity = AssignWorkOrdersCategories(entity, model.WorkOrdersCategories);
            entity = AssignProjects(entity, model.Projects);
            entity = AssignQueues(entity, model.Queues);
            entity = AssignPeople(entity, model.People);
            entity = AssignTasks(entity, model.Tasks);
            var resultRepository = _permissionsRepository.CreatePermissions(entity);
            return ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
        }

        public ResultDto<PermissionDetailDto> Update(PermissionDetailDto model)
        {
            ResultDto<PermissionDetailDto> result = null;
            var entity = _permissionsRepository.GetByIdNotDeleted(model.Id);
            if (entity != null)
            {
                entity.Update(model);
                entity = AssignWorkOrdersCategories(entity, model.WorkOrdersCategories);
                entity = AssignProjects(entity, model.Projects);
                entity = AssignQueues(entity, model.Queues);
                entity = AssignPeople(entity, model.People);
                entity = AssignTasks(entity, model.Tasks);
                entity = AssignPredefinedServices(entity, model.PredefinedServices);
                var resultRepository = _permissionsRepository.UpdatePermissions(entity);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }
            return result ?? ProcessResult(model, new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Permission by id {id}");
            ResultDto<bool> result = null;
            var entity = _permissionsRepository.GetByIdNotDeleted(id);
            if (entity != null)
            {
                entity.Clear();
                var resultSave = _permissionsRepository.DeletePermissions(entity);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        public ResultDto<List<MultiSelectItemDto>> GetPermissionList()
        {
            LogginingService.LogInfo($"GetPermisionList");
            IEnumerable<PermissionsDto> permisions = GetAllKeyValues();
            IList<ProjectsPermissions> projectPermisions = new List<ProjectsPermissions>();

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (PermissionsDto permision in permisions)
            {
                bool isCheck = projectPermisions.Any(x => x.PermissionId == permision.Id);

                multiSelectItemDto.Add(new MultiSelectItemDto()
                {
                    LabelName = permision.Name,
                    Value = permision.Id.ToString(),
                    IsChecked = isCheck
                });
            }

            return ProcessResult(multiSelectItemDto, multiSelectItemDto != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        private IQueryable<PermissionsDto> OrderBy(IQueryable<PermissionsDto> query, PermissionsFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Observations);
            return query;
        }

        private Permissions AssignPredefinedServices(Permissions entity, IList<int> predefinedServicesId)
        {
            entity.PredefinedServicesPermission?.Clear();
            if (predefinedServicesId != null && predefinedServicesId.Any())
            {
                entity.PredefinedServicesPermission =
                    entity.PredefinedServicesPermission ?? new List<PredefinedServicesPermission>();
                var predefinedServicesValues = _predefinedServiceRepository.GetAllById(predefinedServicesId);
                foreach (var element in predefinedServicesValues)
                {
                    entity.PredefinedServicesPermission.Add(new PredefinedServicesPermission()
                    {
                        PredefinedService = element,
                    });
                }
            }

            return entity;
        }

        private Permissions AssignTasks(Permissions entity, IList<int> tasksIds)
        {
            entity.PermissionTask?.Clear();
            if (tasksIds != null && tasksIds.Any())
            {
                entity.PermissionTask =
                    entity.PermissionTask ?? new List<PermissionsTasks>();
                var elements = _taskRepository.GetAllById(tasksIds);
                foreach (var element in elements)
                {
                    entity.PermissionTask.Add(new PermissionsTasks()
                    {
                        Task = element,
                    });
                }
            }

            return entity;
        }

        private Permissions AssignPeople(Permissions entity, IList<int> peopleIds)
        {
            entity.PeoplePermission?.Clear();
            if (peopleIds != null && peopleIds.Any())
            {
                entity.PeoplePermission =
                    entity.PeoplePermission ?? new List<Entities.Tenant.PeoplePermissions>();
                var elements = _peopleRepository.GetByPeopleIds(peopleIds);
                foreach (var element in elements)
                {
                    entity.PeoplePermission.Add(new Entities.Tenant.PeoplePermissions()
                    {
                        People = element,
                    });
                }
            }

            return entity;
        }

        private Permissions AssignQueues(Permissions entity, IList<int> queuesIds)
        {
            entity.PermissionQueue?.Clear();
            if (queuesIds != null && queuesIds.Any())
            {
                entity.PermissionQueue =
                    entity.PermissionQueue ?? new List<PermissionsQueues>();
                var elements = _queueRepository.GetAllById(queuesIds);
                foreach (var element in elements)
                {
                    entity.PermissionQueue.Add(new PermissionsQueues()
                    {
                        Queue = element,
                    });
                }
            }

            return entity;
        }

        private Permissions AssignProjects(Permissions entity, IList<int> projectsIds)
        {
            entity.ProjectPermission?.Clear();
            if (projectsIds != null && projectsIds.Any())
            {
                entity.ProjectPermission = entity.ProjectPermission ?? new List<ProjectsPermissions>();
                var elements = _projectRepository.GetAllById(projectsIds);
                foreach (var element in elements)
                {
                    entity.ProjectPermission.Add(new ProjectsPermissions()
                    {
                        Project = element,
                    });
                }
            }

            return entity;
        }

        private Permissions AssignWorkOrdersCategories(Permissions entity, IList<int> workOrderCategoriesIds)
        {
            entity.WorkOrderCategoryPermission?.Clear();
            if (workOrderCategoriesIds != null && workOrderCategoriesIds.Any())
            {
                entity.WorkOrderCategoryPermission =
                    entity.WorkOrderCategoryPermission ?? new List<WorkOrderCategoryPermissions>();
                var elements = _wordkOrderCategoriesRepository.GetAllByIds(workOrderCategoriesIds);
                foreach (var element in elements)
                {
                    entity.WorkOrderCategoryPermission.Add(new WorkOrderCategoryPermissions()
                    {
                        WorkOrderCategory = element,
                    });
                }
            }

            return entity;
        }
    }
}