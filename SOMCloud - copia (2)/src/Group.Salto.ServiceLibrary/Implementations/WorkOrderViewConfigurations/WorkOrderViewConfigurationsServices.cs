using Group.Salto.Common;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using System;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Extensions;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using System.Linq;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderViewConfigurations
{
    public class WorkOrderViewConfigurationsServices : BaseService, IWorkOrderViewConfigurationsServices
    {
        private readonly IWorkOrderViewConfigurationsRepository _workOrderViewConfigurationsRepository;
        private readonly IWorkOrderColumnsAdapter _workOrderColumnsAdapter;
        private readonly IWorkOrderViewToolTip _workOrderViewToolTip;

        public WorkOrderViewConfigurationsServices(ILoggingService logginingService,
            IWorkOrderViewConfigurationsRepository workOrderViewConfigurationsRepository,
            IWorkOrderColumnsAdapter workOrderColumnsAdapter,
            IWorkOrderViewToolTip workOrderViewToolTip) : base(logginingService)
        {
            _workOrderViewConfigurationsRepository = workOrderViewConfigurationsRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewConfigurationsRepository)} is null");
            _workOrderColumnsAdapter = workOrderColumnsAdapter ?? throw new ArgumentNullException($"{nameof(IWorkOrderColumnsAdapter)} is null");
            _workOrderViewToolTip = workOrderViewToolTip ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewToolTip)} is null");
        }

        public int GetDefaultConfigurationId(int userId)
        {
            int? id = _workOrderViewConfigurationsRepository.GetDefaultConfigByUserId(userId)?.Id;
            if (!id.HasValue)
                id = -1;
            return id.Value;
        }

        public ResultDto<IList<WorkOrderViewConfigurationsDto>> GetAllViewsByUserId(int userId)
        {
            LogginingService.LogInfo($"Get GetAllByUserId by userId {userId}");

            IList<WorkOrderViewConfigurationsDto> result = _workOrderViewConfigurationsRepository.GetAllViewsByUserId(userId).ToListDto();

            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ConfigurationViewDto> GetConfiguredViewById(int id, int languageId)
        {
            LogginingService.LogInfo($"Get GetConfiguredView by id {id}");

            IList<WorkOrderColumnsDto> columns = _workOrderColumnsAdapter.GetAllColumns(languageId);

            ConfigurationViewDto result = _workOrderViewConfigurationsRepository.GetViewColumnsByViewId(id).ToViewDto(columns.OrderBy(x => x.Name).ToList());
            SetToolTips(result, languageId);
            return ProcessResult(result, result != null ? null : new ErrorDto()
            {
                ErrorType = ErrorType.EntityNotExists,
            });
        }

        public ResultDto<ConfigurationViewDto> Create(ConfigurationViewDto configurationViewDto)
        {
            LogginingService.LogInfo($"Create WorkorderFilter");
            SaveResult<UsersMainWoviewConfigurations> resultSave = null;

            bool defaultSaveResult = SetDefaultConfiguration(configurationViewDto);
            if (defaultSaveResult)
            {
                UsersMainWoviewConfigurations localConfigurationView = configurationViewDto.ToEntity();
                localConfigurationView = AssingEntities(configurationViewDto, localConfigurationView);

                resultSave = _workOrderViewConfigurationsRepository.CreateViewConfiguration(localConfigurationView);
                if (resultSave.IsOk)
                {
                    IList<WorkOrderColumnsDto> columns = _workOrderColumnsAdapter.GetAllColumns(configurationViewDto.LanguageId);
                    return ProcessResult(resultSave.Entity.ToViewDto(columns.OrderBy(x => x.Name).ToList()), resultSave);
                }
                else
                {
                    return ProcessResult(configurationViewDto, resultSave);
                }
            }
            else
            {
                return ProcessResult(configurationViewDto, new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.SaveChangesException } });
            }
        }

        public ResultDto<ConfigurationViewDto> Update(ConfigurationViewDto configurationViewDto)
        {
            LogginingService.LogInfo($"Update WorkOrderFilter with id = {configurationViewDto.Id}");
            ResultDto<ConfigurationViewDto> result = null;
            bool defaultSaveResult = SetDefaultConfiguration(configurationViewDto);

            if (defaultSaveResult)
            {
                UsersMainWoviewConfigurations localConfigurationView = _workOrderViewConfigurationsRepository.GetViewColumnsByViewId(configurationViewDto.Id);
                if (localConfigurationView != null)
                {
                    localConfigurationView.UpdateConfigurationView(configurationViewDto.ToEntity());
                    localConfigurationView = AssingEntities(configurationViewDto, localConfigurationView);

                    SaveResult<UsersMainWoviewConfigurations> resultSave = _workOrderViewConfigurationsRepository.UpdateViewConfiguration(localConfigurationView);

                    if (resultSave.IsOk)
                    {
                        IList<WorkOrderColumnsDto> columns = _workOrderColumnsAdapter.GetAllColumns(configurationViewDto.LanguageId);
                        result = ProcessResult(resultSave.Entity.ToViewDto(columns.OrderBy(x => x.Name).ToList()), resultSave);
                    }
                    else
                    {
                        result = ProcessResult(configurationViewDto, resultSave);
                    }
                }
            }
            return result ?? new ResultDto<ConfigurationViewDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = configurationViewDto,
            };
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete Configuration View by id {id}");
            ResultDto<bool> result = null;
            var localConfiguationView = _workOrderViewConfigurationsRepository.GetViewColumnsByViewId(id);
            if (localConfiguationView != null)
            {
                localConfiguationView.MainWoViewConfigurationsColumns.Clear();
                localConfiguationView.MainWoViewConfigurationsPeople.Clear();
                localConfiguationView.MainWoviewConfigurationsGroups.Clear();
                var resultSave = _workOrderViewConfigurationsRepository.DeleteViewConfiguration(localConfiguationView);
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

        private bool SetDefaultConfiguration(ConfigurationViewDto configurationViewDto)
        {
            SaveResult<UsersMainWoviewConfigurations> defaultSaveResult = null;
            UsersMainWoviewConfigurations defaultConfiguration = _workOrderViewConfigurationsRepository.GetDefaultConfigByUserId(configurationViewDto.UserConfigurationId);
            if (defaultConfiguration != null && configurationViewDto.Id != defaultConfiguration.Id)
            {
                defaultConfiguration.IsDefault = false;
                defaultSaveResult = _workOrderViewConfigurationsRepository.UpdateViewConfiguration(defaultConfiguration);
                return defaultSaveResult.IsOk;
            }
            return true;
        }

        private void SetToolTips(ConfigurationViewDto data, int languageId)
        {
            foreach(var column in data.SelectedColumns.Where(x => x.EditType == Salto.Common.Enums.EditTypeEnum.MultiSelect))
            {
                string tooltip = _workOrderViewToolTip.GetToolTipMultiSelect(new MultiSelectConfigurationViewDto() {ColumnId = column.ColumnId, LanguageId = languageId, SelectedFilter = column.FilterValues } );
                column.ToolTip = tooltip ?? string.Empty;
            }

            foreach (var column in data.SelectedColumns.Where(x => x.EditType == Salto.Common.Enums.EditTypeEnum.Date))
            {
                string tooltip = _workOrderViewToolTip.GetToolTipDates(new MultiSelectConfigurationViewDto() { ColumnId = column.ColumnId, LanguageId = languageId, StartDate = column.FilterStartDate.HasValue ? column.FilterStartDate.Value.ToString() : string.Empty, EndDate = column.FilterEndDate.HasValue ? column.FilterEndDate.Value.ToString() : string.Empty });
                column.ToolTip = tooltip ?? string.Empty;
            }
        }
        
        private UsersMainWoviewConfigurations AssingEntities(ConfigurationViewDto configurationViewDto, UsersMainWoviewConfigurations localConfigurationView)
        {
            IEnumerable<int> technicianId = configurationViewDto.Technician.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));
            IEnumerable<int> GroupsId = configurationViewDto.Groups.Where(x => x.IsChecked).Select(x => Convert.ToInt32(x.Value));

            localConfigurationView = localConfigurationView.AssignColumns(configurationViewDto.SelectedColumns);
            localConfigurationView = localConfigurationView.AssignTechnician(technicianId);
            localConfigurationView = localConfigurationView.AssignGroup(GroupsId);
            return localConfigurationView;
        }
    }
}