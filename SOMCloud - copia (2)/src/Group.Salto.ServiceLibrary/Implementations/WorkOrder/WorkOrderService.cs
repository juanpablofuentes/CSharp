using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Contracts.Tasks;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Tasks;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.WorkOrder;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.ServiceLibrary.Common.Dtos.ServiceAnalysis;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderAnalysis;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderService : BaseService, IWorkOrderService
    {
        private readonly IWorkOrdersRepository _workOrdersRepository;
        private readonly IPeopleRepository _peopleRepository;
        private readonly IWorkOrderColumnsAdapter _workOrderColumnsAdapter;
        private readonly IWorkOrderQueryAdapter _workOrderQueryAdapter;
        private readonly IWorkOrderToDto _workOrderToDto;
        private readonly IPeoplePermissionsRepository _peoplePermissionsRepository;
        private readonly IWorkOrderTypesRepository _workOrderTypesRepository;
        private readonly ICollectionTypeWorkOrdersService _collectionTypeWorkOrdersService;
        private readonly IWorkOrderCalculateSLADate _workOrderCalculateSLADate;
        private readonly ITasksService _tasksService;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkOrderAnalysisRepository _workOrderAnalysisRepository;
        private readonly IServicesAnalysisRepository _servicesAnalysisRepository;
        private readonly IWorkOrderViewConfigurationsServices _workOrderViewConfigurationsServices;

        public WorkOrderService(ILoggingService logginingService,
                                IWorkOrdersRepository workOrdersRepository,
                                IPeopleRepository peopleRepository,
                                IWorkOrderColumnsAdapter workOrderColumnsAdapter,
                                IWorkOrderQueryAdapter workOrderQueryAdapter,
                                IWorkOrderToDto workOrderToDto,
                                IWorkOrderTypesRepository workOrderTypesRepository,
                                ICollectionTypeWorkOrdersService collectionTypeWorkOrdersService,
                                IWorkOrderCalculateSLADate workOrderCalculateSLADate,
                                ITasksService tasksService,
                                IProjectRepository projectRepository,
                                IPeoplePermissionsRepository peoplePermissionsRepository,
                                IWorkOrderAnalysisRepository workOrderAnalysisRepository,
                                IServicesAnalysisRepository servicesAnalysisRepository,
                                IWorkOrderViewConfigurationsServices workOrderViewConfigurationsServices) : base(logginingService)
        {
            _workOrdersRepository = workOrdersRepository;
            _peopleRepository = peopleRepository;
            _workOrderColumnsAdapter = workOrderColumnsAdapter ?? throw new ArgumentNullException($"{nameof(IWorkOrderColumnsAdapter)} is null");
            _workOrderQueryAdapter = workOrderQueryAdapter ?? throw new ArgumentNullException($"{nameof(IWorkOrderQueryAdapter)} is null");
            _workOrderToDto = workOrderToDto ?? throw new ArgumentNullException($"{nameof(IWorkOrderToDto)} is null");
            _peoplePermissionsRepository = peoplePermissionsRepository ?? throw new ArgumentNullException($"{nameof(IPeoplePermissionsRepository)} is null");
            _workOrderTypesRepository = workOrderTypesRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderTypesRepository)} is null");
            _collectionTypeWorkOrdersService = collectionTypeWorkOrdersService ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersService)} is null");
            _workOrderCalculateSLADate = workOrderCalculateSLADate ?? throw new ArgumentNullException($"{nameof(IWorkOrderCalculateSLADate)} is null");
            _tasksService = tasksService ?? throw new ArgumentNullException($"{nameof(ITasksService)} is null");
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(IProjectRepository));
            _workOrderAnalysisRepository = workOrderAnalysisRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderAnalysisRepository)} is null");
            _servicesAnalysisRepository = servicesAnalysisRepository ?? throw new ArgumentNullException($"{nameof(IServicesAnalysisRepository)} is null");
            _workOrderViewConfigurationsServices = workOrderViewConfigurationsServices ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewConfigurationsServices)} is null");
        }

        public IEnumerable<WorkOrderBasicInfoDto> GetBasicByFilter(int peopleConfigId, WorkOrderSearchDto orderSearchDto)
        {
            var people = _peopleRepository.GetByConfigIdIncludePermissions(peopleConfigId);
            IEnumerable<WorkOrders> result = _workOrdersRepository.GetByPeopleAndDate(people, orderSearchDto.Date, orderSearchDto.GetAll);
            var listWorkOrders = result.ToBasicInfoDto().ToList();
            foreach (var wo in listWorkOrders)
            {
                var countResult = _workOrdersRepository.GetCountOpenOrdersSameLocation(wo.LocationId);
                wo.WoOpenedInSameLocation = countResult;
            }
            return listWorkOrders;
        }

        public WorkOrderFullInfoDto GetFullWorkOrderInfo(int id)
        {
            WorkOrders result = _workOrdersRepository.GetFullWorkOrderInfo(id);
            var resultDto = result?.ToFullInfoDto();
            return resultDto;
        }

        public WorkOrderResultDto GetConfiguredWorkOrdersList(GridDto gridConfig)
        {
            List<List<DataBaseResultDto>> dataWorkOrders = new List<List<DataBaseResultDto>>();
            gridConfig.PeopleId = _peopleRepository.GetPeopleIdByUserId(gridConfig.UserId);
            gridConfig.SubContracts = _peopleRepository.GetSubContracts(gridConfig.PeopleId);

            IList<WorkOrderColumnsDto> columns = null;
            int id = -1;
            if (gridConfig.ConfigurationViewId == 0)
            {
                id = _workOrderViewConfigurationsServices.GetDefaultConfigurationId(gridConfig.UserId);
            }
            else
            {
                id = gridConfig.ConfigurationViewId;
            }

            columns = _workOrderColumnsAdapter.GetConfiguredColumns(id, gridConfig);

            string sqlcount = _workOrderQueryAdapter.GenerateCountQuery(columns, gridConfig);
            int totalRegs = _workOrdersRepository.GetCountConfiguredWorkOrder(sqlcount, _workOrderQueryAdapter.ParameterList);

            if (totalRegs > 0)
            {
                string sql = _workOrderQueryAdapter.GenerateQuery(columns, gridConfig);
                dataWorkOrders = _workOrdersRepository.GetConfiguredWorkOrder(sql, _workOrderQueryAdapter.ParameterList);
            }
            List<GridDataDto> listGridData = _workOrderToDto.ToGridDataDtos(new GridDataParams() { Data = dataWorkOrders, Columns = columns, IsExcelMode = gridConfig.IsExcelMode });

            WorkOrderResultDto result = new WorkOrderResultDto()
            {
                TotalRegs = totalRegs,
                Columns = columns,
                Data = listGridData,
            };
            return result;
        }

        public FileContentDto ExportToExcel(IList<GridDataDto> listGridData, IList<WorkOrderColumnsDto> columns)
        {
            FileContentDto result = null;
            if (listGridData != null && listGridData.Count > 0)
            {
                byte[] excel = ExportToExcel(listGridData, columns.Select(x => x.TranslatedName).ToList());

                if (excel != null)
                {
                    result = new FileContentDto()
                    {
                        FileBytes = excel,
                        FileName = $"{DateTime.Now.ToString("yyyyMMdd")}_WorkOrderList.xlsx"
                    };
                }
            }
            return result;
        }

        public ResultDto<bool> GetPermissionToWorkOrder(int Id, int userId)
        {
            LogginingService.LogInfo($"Get GetPermissionToWorkOrder id {Id} user: {userId}");
            Entities.Tenant.People people = _peopleRepository.GetByConfigId(userId);
            int[] permissions = _peoplePermissionsRepository.GetPermissionsIdByPeopleId(people.Id);
            int[] subContracts = _peopleRepository.GetSubContracts(people.Id);

            bool access = _workOrdersRepository.GetPermissionToWorkOrder(new WorkOrderPermissionsDto() { Id = Id, PeopleId = people.Id, Permissions = permissions, SubContracts = subContracts });

            return ProcessResult(access);
        }

        public ResultDto<WorkOrderEditDto> GetById(int Id)
        {
            LogginingService.LogInfo($"Get WorkOrder by id {Id}");
            WorkOrderEditDto result = _workOrdersRepository.GetEditById(Id).ToEditDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkOrderEditDto> Create(WorkOrderEditDto workOrderEditDto)
        {
            LogginingService.LogInfo($"Create WorkOrder");
            List<ErrorDto> errors = new List<ErrorDto>();

            Entities.Tenant.People people = _peopleRepository.GetByConfigId(workOrderEditDto.UserConfigurationId);
            workOrderEditDto.PeopleIntroducedById = people.Id;
            LogginingService.LogInfo($"[CreateWorkOrder] WORKORDER CREATION STARTED");

            if (!ValidateWorkOrder(workOrderEditDto, out var validations))
            {
                errors.AddRange(validations);
                return ProcessResult(workOrderEditDto, errors);
            }
            else
            {
                _workOrderCalculateSLADate.CalculateSLADates(workOrderEditDto);
                WorkOrders localWorkOrder = workOrderEditDto.ToEntity();
                localWorkOrder.PeopleResponsible = people;
                localWorkOrder.Project = _projectRepository.GetByIdWithZoneProject(workOrderEditDto.ProjectId);

                ResultDto<bool> taskResult = ExecuteTask(workOrderEditDto, localWorkOrder);

                if (taskResult.Data)
                {
                    return ProcessResult(localWorkOrder.ToEditDto(), taskResult.Errors?.Errors);
                }
                else
                {
                    return ProcessResult(workOrderEditDto, taskResult.Errors?.Errors);
                }
            }
        }

        public ResultDto<WorkOrderEditDto> Update(WorkOrderEditDto workOrderEditDto)
        {
            LogginingService.LogInfo($"Update WorkOrder with id = {workOrderEditDto.Id}");
            ResultDto<WorkOrderEditDto> result = null;

            WorkOrders localWorkOrder = _workOrdersRepository.GetById(workOrderEditDto.Id);
            if (localWorkOrder != null)
            {
                localWorkOrder.UpdateWorkOrder(workOrderEditDto.ToEntity());
                SaveResult<WorkOrders> resultSave = _workOrdersRepository.UpdateWorkOrder(localWorkOrder);

                if (resultSave.IsOk)
                {
                    result = ProcessResult(resultSave.Entity.ToEditDto(), resultSave);
                }
                else
                {
                    result = ProcessResult(workOrderEditDto, resultSave);
                }
            }
            return result ?? new ResultDto<WorkOrderEditDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = workOrderEditDto,
            };
        }

        public ResultDto<WorkOrderDetailDto> GetDetailWO(int Id)
        {
            var entity = _workOrdersRepository.GetDetailWorkOrderInfo(Id);
            var data = entity.ToDetailDto();
            if (entity.WorkOrderTypesId.HasValue)
            {
                var workOrderTypes = _workOrderTypesRepository.GetByCollectionsTypesWorkOrdersId(entity.Project.CollectionsTypesWorkOrdersId).GetWorkOrderTypes(null);
                var nameWorkOrderTypes = _collectionTypeWorkOrdersService.GetWorkOrderTypes(entity.WorkOrderTypesId.Value, workOrderTypes);
                nameWorkOrderTypes.Reverse();
                data.WorkOrderTypes = nameWorkOrderTypes;
            }

            return ProcessResult(data, data != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<WorkOrdersSummaryDto> GetDetailSummaryWO(int Id)
        {
            var entity = _workOrdersRepository.GetSummaryWorkOrderInfo(Id).ToSummaryDto();
            return ProcessResult(entity, entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public IList<WorkOrdersSubWODto> GetDetailSubWO(int Id)
        {
            var entity = _workOrdersRepository.GetSubWOInfo(Id);
            return entity.ToList().ToListDto();
        }

        public ResultDto<IList<WorkOrderAssetsDto>> GetAllAssetsByWorkOrderId(int Id)
        {
            IList<WorkOrderAssetsDto> entity = _workOrdersRepository.GetAllByAssetId(Id).ToList().ToWorkOrderAssetsListDto();
            return ProcessResult(entity, entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<AssetsDetailWorkOrderServicesDto> GetAllServiceAndExtraFieldsById(int Id)
        {
            AssetsDetailWorkOrderServicesDto entity = _workOrdersRepository.GetAllServiceAndExtraFieldsById(Id).ToAssetsDetailWorkOrderServicesDto();
            return ProcessResult(entity, entity != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public WorkOrderOperationsDto GetWOAnalysisAndServiceAnalysis(int Id)
        {
            var technicians = _peopleRepository.GetAll();
            WorkOrderOperationsDto allOperations = null;
            WOAnalysisDto woAnalysis = _workOrderAnalysisRepository.GetWOAnalisys(Id).ToServiceListDto();
            if (woAnalysis != null)
            {
                IList<ServiceAnalysisDto> woService = _servicesAnalysisRepository.GetServiceAnalysisWOOperations(Id).ToList().ToServicesListDto(technicians);
                allOperations = new WorkOrderOperationsDto
                {
                    WOAnalysis = woAnalysis,
                    Services = woService.ToList(),
                    TotalService = woService.GetServiceAnalysisTotals(),
                };
            }
            return allOperations;
        }

        private ResultDto<bool> ExecuteTask(WorkOrderEditDto workOrderEditDto, WorkOrders localWorkOrder)
        {
            ResultDto<bool> taskResult = new ResultDto<bool>();
            TaskApiDto task = _tasksService.GetCreationTasks(workOrderEditDto.UserConfigurationId, localWorkOrder);
            if (task == null)
            {
                AddNoTaskError(taskResult);
            }
            else
            {
                try
                {
                    TaskExecuteDto taskExecute = new TaskExecuteDto
                    {
                        Id = task.Id,
                        WorkOrderId = localWorkOrder.Id,
                        Type = TaskTypeEnum.Creacio,
                        ResponsibleId = localWorkOrder.PeopleResponsible.Id
                    };
                    taskResult = _tasksService.TaskExecute(taskExecute, workOrderEditDto.UserConfigurationId, workOrderEditDto.CustomerId, localWorkOrder);
                }
                catch (Exception ex)
                {
                    AddExecutionError(taskResult, ex);
                }
            }
            return taskResult;
        }

        private void AddExecutionError(ResultDto<bool> taskResult, Exception ex)
        {
            LogginingService.LogException(ex);
            taskResult.Data = false;
            InstanceError(taskResult);
            taskResult.Errors.AddError(new ErrorDto
            {
                ErrorMessageKey = ex.ToString(),
                ErrorType = ErrorType.ValidationError
            });
        }

        private void AddNoTaskError(ResultDto<bool> taskResult)
        {
            taskResult.Data = false;
            InstanceError(taskResult);
            taskResult.Errors.AddError(new ErrorDto
            {
                ErrorMessageKey = WorkOrderConstants.WorkOrderNoTaskCreationToExecute,
                ErrorType = ErrorType.EntityNotExists
            });
        }

        private bool ValidateWorkOrder(WorkOrderEditDto workOrderEditDto, out List<ErrorDto> result)
        {
            result = new List<ErrorDto>();
            if (!workOrderEditDto.IsValid())
            {
                result.Add(new ErrorDto()
                {
                    ErrorType = ErrorType.ValidationError,
                });
            }

            return !result.Any();
        }
    }
}