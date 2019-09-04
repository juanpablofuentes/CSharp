using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.AdvancedSearch;

namespace Group.Salto.ServiceLibrary.Implementations.Project
{
    public class ProjectService : BaseFilterService, IProjectsService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IPeopleProjectsRepository _peopleProjectsRepository;
        private readonly IPermissionsService _permissionsService;
        private readonly IProjectsPermissionsRepository _projectsPermissionsRepository;
        private readonly IPredefinedServiceRepository _predefinedServiceRepository;
        private readonly IPeopleRepository _peopleRepository;

        public ProjectService(ILoggingService logginingService,
                              IProjectRepository projectRepository,
                              IPeopleProjectsRepository peopleProjectsRepository,
                              IPermissionsService permissionsService,
                              IPredefinedServiceRepository predefinedServiceRepository,
                              IProjectsPermissionsRepository projectsPermissionsRepository,
                              IProjectQueryFactory projectQueryFactory,
                              IPeopleRepository peopleRepository) : base(projectQueryFactory, logginingService)
        {
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(IProjectRepository));
            _peopleProjectsRepository = peopleProjectsRepository ?? throw new ArgumentNullException(nameof(IPeopleProjectsRepository));
            _permissionsService = permissionsService ?? throw new ArgumentNullException(nameof(IPeopleProjectsRepository));
            _projectsPermissionsRepository = projectsPermissionsRepository ?? throw new ArgumentNullException(nameof(IPeopleProjectsRepository));
            _predefinedServiceRepository = predefinedServiceRepository ?? throw new ArgumentNullException(nameof(IPredefinedServiceRepository));
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(IPeopleRepository));
        }

        public ResultDto<ProjectsDetailsDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get Project by id {Id}");

            Projects project = _projectRepository.GetWithPeopleProjectById(Id);
            ProjectsDetailsDto result = GetProjectManager(project).ToDetailDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<IList<AdvancedSearchDto>> GetAdvancedSearch(QueryTypeParametersDto queryTypeParameters)
        {
            LogginingService.LogInfo($"Get GetAdvancedSearch ");

            var projects = _projectRepository.GetProjectForAdvancedSearch(new Infrastructure.Common.Dto.FilterQueryDto() { Name = queryTypeParameters.Value });
            IList<AdvancedSearchDto> result = ToAdvancedSearchDto(projects);

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ProjectsDetailsDto> Create(ProjectsDetailsDto projectsDetailsDto)
        {
            LogginingService.LogInfo($"Create project");
            var localProject = projectsDetailsDto.ToEntity();

            localProject = projectsDetailsDto.AddPeopleProjects(localProject);
            IEnumerable<int> permissionsId = projectsDetailsDto.Permissions.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
            localProject = localProject.AssignPermissions(permissionsId);
            localProject = localProject.AssignProjectsContacts(projectsDetailsDto.ContactsSelected);
            localProject = localProject.AssignTechnicalCodes(projectsDetailsDto.TechnicalCodesSelected);
            localProject = localProject.AssignToAddPredefinedServices(projectsDetailsDto);

            var resultSave = _projectRepository.CreateProjects(localProject);
            if (resultSave.IsOk)
            {
                return ProcessResult(GetProjectManager(resultSave.Entity).ToDetailDto(), resultSave);
            }
            else
            {
                return ProcessResult(projectsDetailsDto, resultSave);
            }
        }

        public ResultDto<ProjectsDetailsDto> Update(ProjectsDetailsDto projectsDetailsDto)
        {
            LogginingService.LogInfo($"Update Project with id = {projectsDetailsDto.Id}");
            ResultDto<ProjectsDetailsDto> result = null;
            var localProject = _projectRepository.GetWithPeopleProjectById(projectsDetailsDto.Id);
            if (localProject != null)
            {
                localProject.UpdateProject(projectsDetailsDto.ToEntity());
                localProject = UpdatePeopleProject(projectsDetailsDto, localProject);

                IEnumerable<int> permissionsId = projectsDetailsDto.Permissions.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
                localProject = localProject.AssignPermissions(permissionsId);
                localProject = localProject.AssignProjectsContacts(projectsDetailsDto.ContactsSelected);
                localProject = localProject.AssignTechnicalCodes(projectsDetailsDto.TechnicalCodesSelected);
                localProject = localProject.AssignToAddPredefinedServices(projectsDetailsDto);
                localProject = localProject.AssignToUpdatePredefinedServices(projectsDetailsDto);
                localProject = DeletePredefinedServices(projectsDetailsDto, localProject);

                var resultSave = _projectRepository.UpdateProjects(localProject);
                if (resultSave.IsOk)
                {
                    return ProcessResult(GetProjectManager(resultSave.Entity).ToDetailDto(), resultSave);
                }
                else
                {
                    result = ProcessResult(projectsDetailsDto, resultSave);
                }
            }
            return result ?? new ResultDto<ProjectsDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = projectsDetailsDto,
            };
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Project by id {id}");
            ResultDto<bool> result = null;
            var localProject = _projectRepository.GetByIdWithIncludesPermissions(id);
            if (localProject != null)
            {
                if (localProject.ProjectPermission?.Any() == true)
                {
                    localProject.ProjectPermission.Clear();
                }
                var resultSave = _projectRepository.DeleteProjects(localProject);
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

        public ResultDto<IList<ProjectsDto>> GetAllFiltered(ProjectsFilterDto filter)
        {
            var query = _projectRepository.GetAllWithPeopleProjectsAndPeople();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Serie, au => au.Serie.Contains(filter.Serie));

            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Project Key Value");
            var data = _projectRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }

        public ResultDto<List<MultiSelectItemDto>> GetPermissionList(int? projectId)
        {
            LogginingService.LogInfo($"GetPermisionList for people {projectId}");
            IEnumerable<PermissionsDto> permisions = _permissionsService.GetAllKeyValues();
            IList<ProjectsPermissions> projectPermisions = new List<ProjectsPermissions>();

            if (projectId.HasValue)
                projectPermisions = _projectsPermissionsRepository.GetByProjectId(projectId.Value);

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

        public ResultDto<List<MultiSelectItemDto>> GetProjectMultiSelect(List<int> selectItems, int userId)
        {
            LogginingService.LogInfo($"GetProjectMultiSelect");
            IList<BaseNameIdDto<int>> projects = Filter(new QueryRequestDto() { QueryType = QueryTypeEnum.Autocomplete, QueryTypeParameters = new QueryTypeParametersDto() { Value = userId.ToString() } });
            return GetMultiSelect(projects, selectItems);
        }

        public IList<BaseNameIdDto<int>> GetProjectFilteredUserId(int userId) {
            return Filter(new QueryRequestDto() { QueryType = QueryTypeEnum.Autocomplete, QueryTypeParameters = new QueryTypeParametersDto() { Value = userId.ToString() } });
        }

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            var project = _projectRepository.GetByIdWithIncludesToDelete(id);
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (project?.ProjectsContacts?.Any() == true)
            {
                result.ErrorMessageKey = "ProjectCanDeleteHaveProjectsContact";
            }
            else if (project?.TechnicalCodes?.Any() == true)
            {
                result.ErrorMessageKey = "ProjectCanDeleteHaveTechnicalCodes";
            }
            else if (project?.ZoneProject?.Any() == true == true)
            {
                result.ErrorMessageKey = "ProjectCanDeleteHaveZoneProject";
            }
            else if (project?.WorkOrders?.Any() == true
                || project?.WorkOrdersDeritative?.Any() == true
                || project?.ProjectsCalendars?.Any() == true
                || project?.ProjectsContacts?.Any() == true
                || project?.PredefinedServices?.Any() == true
                || project?.PreconditionsLiteralValues?.Any() == true)
            {
                result.ErrorMessageKey = "ProjectCanDeleteHaveRelations";
            }

            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> FilterByClient(QueryCascadeDto queryRequest)
        {
            var query = _projectRepository.FilterByClient(queryRequest.Text, queryRequest.Selected);
            var result = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _projectRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private Projects UpdatePeopleProject(ProjectsDetailsDto projectsDetailsDto, Projects localProject)
        {
            var peopleProjects = localProject.PeopleProjects.Where(x => x.IsManager).FirstOrDefault();
            if (peopleProjects != null)
            {
                _peopleProjectsRepository.DeleteOnContext(peopleProjects);
                localProject.PeopleProjects.Remove(peopleProjects);
            }
            localProject = projectsDetailsDto.AddPeopleProjects(localProject);
            return localProject;
        }

        private Projects DeletePredefinedServices(ProjectsDetailsDto projectsDetailsDto, Projects localProject)
        {
            IList<PredefinedServicesDto> forDelete = projectsDetailsDto.PredefinedServicesSelected.Where(x => x.State == "D").ToList();
            if (forDelete != null && forDelete.Count > 0)
            {
                foreach (PredefinedServicesDto predefinedService in forDelete)
                {
                    PredefinedServices entityToDelete = localProject.PredefinedServices.Where(x => x.Id == predefinedService.Id).FirstOrDefault();
                    if (entityToDelete != null)
                    {
                        _predefinedServiceRepository.DeleteOnContext(entityToDelete);
                        localProject.PredefinedServices.Remove(entityToDelete);
                    }
                }
            }
            return localProject;
        }

        private IQueryable<Projects> OrderBy(IQueryable<Projects> data, ProjectsFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Serie);
            return query;
        }

        private Projects GetProjectManager(Projects project)
        {
            project.PeopleProjects = project?.PeopleProjects.Where(x => x.IsManager).ToList();
            return project;
        }

        private IList<AdvancedSearchDto> ToAdvancedSearchDto(List<Projects> projects)
        {
            if (projects != null)
            {
                for (int i = 0; i < projects.Count; i++)
                {
                    Projects project = projects[i];
                    project = GetProjectManager(project);
                }
            }
            return projects.ToAdvancedSearchListDto();
        }
    }
}