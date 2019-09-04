using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderDefaultColumns;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderColumns
{
    public class WorkOrderColumnsAdapter : BaseService, IWorkOrderColumnsAdapter
    {
        private readonly IWorkOrderColumnsRepository _workOrderColumnsRepository;
        private readonly IWorkOrderViewConfigurationsRepository _workOrderViewConfigurationsRepository;
        private readonly IWorkOrderDefaultColumnsService _workOrderDefaultColumnsService;
        private readonly IWorkOrderViewFilterValues _workOrderViewFilterValues;
        private readonly ITranslationService _translationService;
        private readonly ICache _cacheService;

        public WorkOrderColumnsAdapter(ILoggingService logginingService,
                                       IWorkOrderColumnsRepository workOrderColumnsRepository,
                                       IWorkOrderViewConfigurationsRepository workOrderViewConfigurationsRepository,
                                       IWorkOrderDefaultColumnsService workOrderDefaultColumnsService,
                                       IWorkOrderViewFilterValues workOrderViewFilterValues,
                                       ITranslationService translationService,
                                       ICache cacheService) : base(logginingService)
        {
            _workOrderColumnsRepository = workOrderColumnsRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderColumnsRepository)} is null");
            _workOrderViewConfigurationsRepository = workOrderViewConfigurationsRepository ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewConfigurationsRepository)} is null");
            _workOrderDefaultColumnsService = workOrderDefaultColumnsService ?? throw new ArgumentNullException($"{nameof(IWorkOrderDefaultColumnsService)} is null");
            _workOrderViewFilterValues = workOrderViewFilterValues ?? throw new ArgumentNullException($"{nameof(IWorkOrderViewFilterValues)} is null");
            _translationService = translationService ?? throw new ArgumentNullException($"{nameof(ITranslationService)} is null ");
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(ICache)} is null");
        }

        public IList<WorkOrderColumnsDto> GetConfiguredColumns(int id, GridDto gridConfig)
        {
            IList<WorkOrderColumnsDto> columnsDtos = null;
            IList<WorkOrderColumnsDto> columns = GetAllColumns(gridConfig.LanguageId);

            if (id == -1 )
            {
                IList<Entities.WorkOrderDefaultColumns> data = _workOrderDefaultColumnsService.GetAll().ToList();
                columnsDtos = data.ToListDto(columns, gridConfig.IsExcelMode);
            }
            else
            {
                UsersMainWoviewConfigurations data = _workOrderViewConfigurationsRepository.GetViewColumnsByViewId(id);
                columnsDtos = data.MainWoViewConfigurationsColumns.OrderBy(x => x.ColumnOrder).ToList().ToListDto(columns, gridConfig.IsExcelMode);

                if (!gridConfig.IsQuickFilter)
                {
                    gridConfig.WorkOrderFilters = _workOrderViewFilterValues.GetFilterValues(data);
                    if (data.MainWoViewConfigurationsPeople != null && data.MainWoViewConfigurationsPeople.Any())
                    {
                        gridConfig.WorkOrderFilters.ResponsiblesIds = data.MainWoViewConfigurationsPeople?.Select(x => x.PeopleId.ToString())?.ToList()?.Aggregate((i, j) => $"{i}, {j}");
                    }

                    if (data.MainWoviewConfigurationsGroups != null && data.MainWoviewConfigurationsGroups.Any())
                    {
                        gridConfig.WorkOrderFilters.ResponsiblesIds += data.MainWoviewConfigurationsGroups.SelectMany(x => x.PeopleCollection.PeopleCollectionsPeople.Select(s => s.PeopleId.ToString()))?.ToList()?.Aggregate((i, j) => $"{i}, {j}");
                    }
                }
            }
            return columnsDtos;
        }

        public IList<WorkOrderColumnsDto> GetAllColumns(int languageId)
        {
            LogginingService.LogInfo($"Get all Columns");
            IList<WorkOrderColumnsDto> res = (IList<WorkOrderColumnsDto>)_cacheService.GetData(AppCache.Column + languageId, AppCache.ColumnKey + languageId);

            if (res is null)
            {
                List<Entities.WorkOrderColumns> result = _workOrderColumnsRepository.GetAllColumns();
                res = new List<WorkOrderColumnsDto>();
                foreach (Entities.WorkOrderColumns column in result)
                {
                    string translationName = _translationService.GetTranslationText(column.Id + column.Name);
                    res.Add(column.ToDto(translationName));
                }
                _cacheService.SetData(AppCache.Column + languageId, AppCache.ColumnKey + languageId, res);
            }

            return res;
        }
    }
}