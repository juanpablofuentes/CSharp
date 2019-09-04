using Group.Salto.Common;
using Group.Salto.Common.Constants.FormElements;
using Group.Salto.Common.Enums;
using Group.Salto.DataAccess.Tenant.Repositories;
using Group.Salto.Entities.Tenant;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Idantity;
using Group.Salto.ServiceLibrary.Common.Contracts.Permisions;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.TechnicalCodes;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderCategories
{
    public class WorkOrderCategoriesService : BaseFilterService, IWorkOrderCategoriesService
    {
        private readonly IWorkOrderCategoriesRepository _workOrderCategoriesRepository;
        private readonly IPermissionsService _permissionsService;
        private readonly IPermissionsRepository _permissionsRepository;
        private readonly IWorkOrderCategoryPermissionsRepository _workOrderCategoryPermissionsRepository;
        private readonly IIdentityService _identityService;
        private readonly IWorkOrderCategoryRolesRepository _workOrderCategoryRolesRepository;
        private readonly IKnowledgeRepository _knowledgeRepository;
        private readonly IMainWoViewConfigurationsColumnsRepository _mainWoViewConfigurationsColumnsRepository;
        private readonly IFormElementRepository _formElementsRepository;
        private readonly ISlaRepository _slaRepository;
        private readonly IProjectRepository _projectRepository;

        public WorkOrderCategoriesService(ILoggingService logginingService,
                                          IWorkOrderCategoriesRepository workOrderCategoriesRepository,
                                          IPermissionsService permissionsService,
                                          IPermissionsRepository permissionsRepository,
                                          IWorkOrderCategoryPermissionsRepository workOrderCategoryPermissionsRepository,
                                          IIdentityService identityService,
                                          IKnowledgeRepository knowledgeRepository,
                                          IWorkOrderCategoryRolesRepository workOrderCategoryRolesRepository,
                                          IWorkOrderCategoryQueryFactory workOrderCategoryQueryFactory,
                                          IMainWoViewConfigurationsColumnsRepository mainWoViewConfigurationsColumnsRepository,
                                          IFormElementRepository formElementRepository,
                                          IProjectRepository projectRepository,
                                          ISlaRepository slaRepository) : base(workOrderCategoryQueryFactory, logginingService)
        {
            _workOrderCategoriesRepository = workOrderCategoriesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesRepository)} is null");
            _permissionsService = permissionsService ?? throw new ArgumentNullException($"{nameof(IPermissionsService)} is null");
            _workOrderCategoryPermissionsRepository = workOrderCategoryPermissionsRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoryPermissionsRepository)} is null");
            _permissionsRepository = permissionsRepository ?? throw new ArgumentNullException($"{nameof(IPermissionsRepository)} is null");
            _identityService = identityService ?? throw new ArgumentNullException($"{nameof(IIdentityService)} is null");
            _workOrderCategoryRolesRepository = workOrderCategoryRolesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoryRolesRepository)} is null");
            _knowledgeRepository = knowledgeRepository ?? throw new ArgumentNullException($"{nameof(IKnowledgeRepository)} is null ");
            _mainWoViewConfigurationsColumnsRepository = mainWoViewConfigurationsColumnsRepository ?? throw new ArgumentNullException($"{nameof(IMainWoViewConfigurationsColumnsRepository)} is null ");
            _formElementsRepository = formElementRepository ?? throw new ArgumentNullException($"{nameof(IFormElementRepository)} is null ");
            _slaRepository = slaRepository ?? throw new ArgumentNullException($"{nameof(ISlaRepository)} is null ");
            _projectRepository = projectRepository ?? throw new ArgumentNullException($"{nameof(IProjectRepository)} is null ");
        }

        public ResultDto<WorkOrderCategoryDetailsDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get WorkOrderCategory by id {Id}");
            WorkOrderCategoryDetailsDto result = _workOrderCategoriesRepository.GetById(Id)?.ToDetailDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public Entities.Tenant.WorkOrderCategories GetByIdWithSLA(int Id)
        {
            LogginingService.LogInfo($"Get WorkOrderCategory with SLA by id {Id}");
            return _workOrderCategoriesRepository.GetByIdWithSLA(Id);
        }

        public ResultDto<WorkOrderCategoryDetailsDto> Create(WorkOrderCategoryDetailsDto workOrderCategoryDetailDto)
        {
            LogginingService.LogInfo($"Create WorkOrderCategory");
            var workOrderCategories = workOrderCategoryDetailDto.ToEntity();
            workOrderCategories = AssignTechnicalCodes(workOrderCategories, workOrderCategoryDetailDto.TechnicalCodesSelected);
            workOrderCategories = AssignKnowledges(workOrderCategories, workOrderCategoryDetailDto.KnowledgeSelected);

            var data = workOrderCategoryDetailDto.Permissions.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
            workOrderCategories = AssignPermissions(workOrderCategories, data);

            var datarol = workOrderCategoryDetailDto.Roles.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
            workOrderCategories = AssignRoles(workOrderCategories, datarol);

            var resultSave = _workOrderCategoriesRepository.CreateWorkOrderCategories(workOrderCategories);
            return ProcessResult(resultSave.Entity.ToDetailDto(), resultSave);
        }

        public ResultDto<WorkOrderCategoryDetailsDto> Update(WorkOrderCategoryDetailsDto workOrderCategoryDetailDto)
        {
            LogginingService.LogInfo($"Update WorkOrderCategory with id = {workOrderCategoryDetailDto.Id}");
            ResultDto<WorkOrderCategoryDetailsDto> result = null;
            var localWorkOrderCategory = _workOrderCategoriesRepository.GetById(workOrderCategoryDetailDto.Id);
            if (localWorkOrderCategory != null)
            {
                localWorkOrderCategory.UpdateWorkOrderCategory(workOrderCategoryDetailDto.ToEntity());
                localWorkOrderCategory = AssignTechnicalCodes(localWorkOrderCategory, workOrderCategoryDetailDto.TechnicalCodesSelected);
                localWorkOrderCategory = AssignKnowledges(localWorkOrderCategory, workOrderCategoryDetailDto.KnowledgeSelected);

                var data = workOrderCategoryDetailDto.Permissions.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
                localWorkOrderCategory = AssignPermissions(localWorkOrderCategory, data);

                var datarol = workOrderCategoryDetailDto.Roles.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
                localWorkOrderCategory = AssignRoles(localWorkOrderCategory, datarol);

                var resultSave = _workOrderCategoriesRepository.UpdateWorkOrderCategories(localWorkOrderCategory);
                result = ProcessResult(_workOrderCategoriesRepository.GetById(workOrderCategoryDetailDto.Id)?.ToDetailDto(), resultSave);
            }
            return result ?? new ResultDto<WorkOrderCategoryDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = workOrderCategoryDetailDto,
            };
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete WorkOrderCategory by id {id}");
            ResultDto<bool> result = null;
            var localWorkCenter = _workOrderCategoriesRepository.GetById(id);
            if (localWorkCenter != null)
            {
                var resultSave = _workOrderCategoriesRepository.DeleteWorkOrderCategories(localWorkCenter);
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

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };
            var workOrderCategory = _workOrderCategoriesRepository.GetWorkOrderCategoryRelationshipsById(id);

            if (workOrderCategory != null)
            {
                if (workOrderCategory.ExternalServicesConfiguration?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveExternalServicesConfiguration";
                }
                else if (workOrderCategory.ExternalServicesConfigurationProjectCategories?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveExternalServicesConfigurationProjectCategories";
                }
                else if (workOrderCategory.PreconditionsLiteralValues?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHavePreconditionsLiteralValues";
                }
                else if (workOrderCategory.TechnicalCodes?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveTechnicalCodes";
                }
                else if (workOrderCategory.WorkOrderCategoryKnowledge?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveWorkOrderCategoryKnowledge";
                }
                else if (workOrderCategory.WorkOrderCategoryPermission?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveWorkOrderCategoryPermission";
                }
                else if (workOrderCategory.WorkOrderCategoryRoles?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveWorkOrderCategoryRoles";
                }
                else if (workOrderCategory.WorkOrders?.Any() == true)
                {
                    result.ErrorMessageKey = "WOCategoryCanDeleteHaveWorkOrder";
                }
                else
                {
                    var mainWoViewConfigurationsColumns = _mainWoViewConfigurationsColumnsRepository.GetFilterValuesByColumnId((int)WorkOrderColumnsEnum.WorkOrderCategory);
                    foreach (var item in mainWoViewConfigurationsColumns)
                    {
                        if (item.FilterValues.Contains(id.ToString()))
                        {
                            result.ErrorMessageKey = "WOCategoryCanDeleteHaveMainWoViewConfigurationsColumn";
                            break;
                        }

                    }
                    var formElements = _formElementsRepository.GetValueByName(FormElementsConstants.CategoriesList);
                    foreach (var elem in formElements)
                    {
                        if (elem.Value.Contains(id.ToString()))
                        {
                            result.ErrorMessageKey = "WOCategoryCanDeleteHaveFormElements";
                            break;
                        }
                    }
                }
            }

            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get all Key Values WO Categories");
            var query = _workOrderCategoriesRepository.GetAllKeyValues();
            var data = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
            return data;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValuesByProject(List<int> projectsIds, int userId)
        {
            LogginingService.LogInfo($"Get all Key Values WO Categories By Project");
            IList<BaseNameIdDto<int>> data;
            if (projectsIds.Count() == 0)
            {
                var query = _workOrderCategoriesRepository.GetAll();
                data = Filter(new QueryRequestDto() { QueryType = QueryTypeEnum.Autocomplete, QueryTypeParameters = new QueryTypeParametersDto() { Value = userId.ToString() } });
            }
            else
            {
                var query = _workOrderCategoriesRepository.GetAllWOCategoriesByProjectIds(projectsIds);
                data = query.Select(x => new BaseNameIdDto<int>()
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            }
            
            return data;
        }

        public ResultDto<IList<WorkOrderCategoriesListDto>> GetAllFiltered(WorkOrderCategoriesFilterDto filter)
        {
            var query = _workOrderCategoriesRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.Where(au => !au.IsDeleted);
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToListDto());
        }

        public ResultDto<List<MultiSelectItemDto>> GetPermissionList(int? workOrderCategoryId)
        {
            LogginingService.LogInfo($"GetPermisionList for workOrderCategoryId {workOrderCategoryId}");
            IEnumerable<PermissionsDto> permisions = _permissionsService.GetAllKeyValues();
            IList<WorkOrderCategoryPermissions> workOrderCategoryPermisions = new List<WorkOrderCategoryPermissions>();

            if (workOrderCategoryId.HasValue)
                workOrderCategoryPermisions = _workOrderCategoryPermissionsRepository.GetByWorkOrderCategoryId(workOrderCategoryId.Value);

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (PermissionsDto permision in permisions)
            {
                bool isCheck = workOrderCategoryPermisions.Any(x => x.PermissionId == permision.Id);

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

        public async Task<ResultDto<List<MultiSelectItemDto>>> GetPermissionRoleList(int? workOrderCategoryId)
        {
            LogginingService.LogInfo($"GetPermissionRoleList workOrderCategoryId {workOrderCategoryId}");
            var rolesData = await _identityService.GetAllRolesKeyValues();
            IEnumerable<PermissionsDto> roles = rolesData.ToMultiSelectListDto();
            IList<WorkOrderCategoryRoles> workOrderCategoryRoles = new List<WorkOrderCategoryRoles>();

            if (workOrderCategoryId.HasValue)
                workOrderCategoryRoles = _workOrderCategoryRolesRepository.GetByWorkOrderCategoryId(workOrderCategoryId.Value);

            List<MultiSelectItemDto> multiSelectItemDto = new List<MultiSelectItemDto>();
            foreach (PermissionsDto rol in roles)
            {
                bool isCheck = workOrderCategoryRoles.Any(x => x.RoleId == rol.Id);

                multiSelectItemDto.Add(new MultiSelectItemDto()
                {
                    LabelName = rol.Name,
                    Value = rol.Id.ToString(),
                    IsChecked = isCheck
                });
            }

            return ProcessResult(multiSelectItemDto, multiSelectItemDto != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<List<bool>> HasSLADates(int id)
        {
            List<bool> result = new List<bool>();
            LogginingService.LogInfo($"Get HasSLADates by id {id}");
            Entities.Tenant.WorkOrderCategories workOrderCategory = _workOrderCategoriesRepository.GetById(id);

            if (workOrderCategory != null)
            {
                Sla sla = _slaRepository.GetById(workOrderCategory.SlaId);
                if (sla != null)
                {
                    result.Add(sla.TimeResolutionActive.HasValue && sla.TimeResolutionActive.Value && sla.MinutesResponseOtDefined.HasValue && sla.MinutesResponseOtDefined.Value);
                    result.Add(sla.TimeResolutionActive.HasValue && sla.TimeResolutionActive.Value && sla.MinutesResolutionOtDefined.HasValue && sla.MinutesResolutionOtDefined.Value);
                    result.Add(sla.TimePenaltyWithoutResponseActive.HasValue && sla.TimePenaltyWithoutResponseActive.Value && sla.MinutesPenaltyWithoutResponseOtDefined.HasValue && sla.MinutesPenaltyWithoutResponseOtDefined.Value);
                    result.Add(sla.TimePenaltyWhithoutResolutionActive.HasValue && sla.TimePenaltyWhithoutResolutionActive.Value && sla.MinutesPenaltyWithoutResolutionOtDefined.HasValue && sla.MinutesPenaltyWithoutResolutionOtDefined.Value);
                }
            }

            return ProcessResult(result);
        }

        public IList<BaseNameIdDto<int>> FilterByProject(QueryCascadeDto queryRequest)
        {
            int? id = queryRequest.Selected.FirstOrDefault();
            int? projectWoCat = null;
            if (id.HasValue)
            {
                var project = _projectRepository.GetById(id.Value);
                projectWoCat = project?.WorkOrderCategoriesCollectionId;
            }
            var query = _workOrderCategoriesRepository.FilterByProject(queryRequest.Text, projectWoCat);
            var result = query.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }

        public ResultDto<List<MultiSelectItemDto>> GeWorkOrderCategoryMultiSelect(List<int> selectItems, int userId)
        {
            LogginingService.LogInfo($"GeWorkOrderCategoryMultiSelect");
            IList<BaseNameIdDto<int>> categories = Filter(new QueryRequestDto() { QueryType = QueryTypeEnum.Autocomplete, QueryTypeParameters = new QueryTypeParametersDto() { Value = userId.ToString() } });
            return GetMultiSelect(categories, selectItems);
        }

        public string GetNamesComaSeparated(List<int> ids)
        {
            List<string> names = _workOrderCategoriesRepository.GetByIds(ids).Select(x => x.Name).ToList();
            return names.Aggregate((i, j) => $"{i}, {j}");
        }

        private IQueryable<Entities.Tenant.WorkOrderCategories> OrderBy(IQueryable<Entities.Tenant.WorkOrderCategories> data, WorkOrderCategoriesFilterDto filter)
        {
            var query = data.AsQueryable();
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            return query;
        }

        private Entities.Tenant.WorkOrderCategories AssignTechnicalCodes(Entities.Tenant.WorkOrderCategories entity, IList<TechnicalCodesDto> technicalCodes)
        {
            entity.TechnicalCodes?.Clear();
            if (technicalCodes != null && technicalCodes.Any())
            {
                entity.TechnicalCodes = entity.TechnicalCodes ?? new List<TechnicalCodes>();
                IList<TechnicalCodes> localTechnicalCodes = technicalCodes.ToEntity();
                foreach (TechnicalCodes localTechnicalCode in localTechnicalCodes)
                {
                    entity.TechnicalCodes.Add(localTechnicalCode);
                }
            }
            return entity;
        }

        private Entities.Tenant.WorkOrderCategories AssignPermissions(Entities.Tenant.WorkOrderCategories entity, IEnumerable<int> PermissionIds)
        {
            entity.WorkOrderCategoryPermission?.Clear();
            if (PermissionIds != null && PermissionIds.Any())
            {
                entity.WorkOrderCategoryPermission = entity.WorkOrderCategoryPermission ?? new List<WorkOrderCategoryPermissions>();
                var elements = _permissionsRepository.GetAllById(PermissionIds);
                foreach (var element in elements)
                {
                    entity.WorkOrderCategoryPermission.Add(new WorkOrderCategoryPermissions()
                    {
                        Permission = element,
                    });
                }
            }

            return entity;
        }

        private Entities.Tenant.WorkOrderCategories AssignRoles(Entities.Tenant.WorkOrderCategories entity, IEnumerable<int> rolesIds)
        {
            entity.WorkOrderCategoryRoles?.Clear();
            if (rolesIds != null && rolesIds.Any())
            {
                entity.WorkOrderCategoryRoles = entity.WorkOrderCategoryRoles ?? new List<WorkOrderCategoryRoles>();
                foreach (var rol in rolesIds)
                {
                    entity.WorkOrderCategoryRoles.Add(new WorkOrderCategoryRoles()
                    {
                        RoleId = rol
                    });
                }
            }

            return entity;
        }

        private Entities.Tenant.WorkOrderCategories AssignKnowledges(Entities.Tenant.WorkOrderCategories entity, IList<WorkOrderCategoryKnowledgeDto> knowledges)
        {
            entity.WorkOrderCategoryKnowledge?.Clear();
            if (knowledges != null && knowledges.Any())
            {
                entity.WorkOrderCategoryKnowledge = entity.WorkOrderCategoryKnowledge ?? new List<Entities.Tenant.WorkOrderCategoryKnowledge>();
                List<Entities.Tenant.Knowledge> localKnowledges = _knowledgeRepository.FilterById(knowledges.Select(x => x.Id)).ToList();
                foreach (Entities.Tenant.Knowledge localKnowledge in localKnowledges)
                {
                    entity.WorkOrderCategoryKnowledge.Add(new Entities.Tenant.WorkOrderCategoryKnowledge()
                    {
                        Knowledge = localKnowledge,
                        KnowledgeLevel = knowledges.SingleOrDefault(x => x.Id == localKnowledge.Id)?.Priority ?? 0,
                    });
                }
            }
            return entity;
        }
    }
}