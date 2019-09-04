using Group.Salto.Common.Constants.WorkOrderFilter;
using Group.Salto.Common.Enums;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Contracts.Clients;
using Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Contracts.ExternalWorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.FinalClients;
using Group.Salto.ServiceLibrary.Common.Contracts.People;
using Group.Salto.ServiceLibrary.Common.Contracts.Project;
using Group.Salto.ServiceLibrary.Common.Contracts.Queue;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderStatus;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Contracts.Zones;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderViewConfigurations
{
    public class WorkOrderViewToolTip : BaseService, IWorkOrderViewToolTip
    {
        private readonly IDictionary<WorkOrderColumnsEnum, Func<int, List<int>, string>> toolTips = null;

        private readonly IPeopleService _peopleService;
        private readonly IWorkOrderStatusService _workOrderStatusService;
        private readonly ICollectionTypeWorkOrdersService _collectionTypeWorkOrdersService;
        private readonly IProjectsService _projectsService;
        private readonly IQueueService _queueService;
        private readonly IZonesService _zonesService;
        private readonly IStateService _stateService;
        private readonly IWorkOrderCategoriesService _workOrderCategoriesService;
        private readonly IClientService _clientService;
        private readonly IFinalClientsServices _finalClientsServices;
        private readonly IExternalWorkOrderStatusService _externalWorkOrderStatusService;
        private readonly ITranslationService _translationService;

        public WorkOrderViewToolTip(ILoggingService logginingService,
                                    IPeopleService peopleService,
                                    IWorkOrderStatusService workOrderStatusService,
                                    ICollectionTypeWorkOrdersService collectionTypeWorkOrdersService,
                                    IProjectsService projectsService,
                                    IQueueService queueService,
                                    IZonesService zonesService,
                                    IStateService stateService,
                                    IClientService clientService,
                                    IFinalClientsServices finalClientsServices,
                                    IWorkOrderCategoriesService workOrderCategoriesService,
                                    IExternalWorkOrderStatusService externalWorkOrderStatusService,
                                    ITranslationService translationService) : base(logginingService)
        {
            _peopleService = peopleService ?? throw new ArgumentNullException($"{nameof(IPeopleService)} is null");
            _workOrderStatusService = workOrderStatusService ?? throw new ArgumentNullException($"{nameof(IWorkOrderStatusService)} is null");
            _collectionTypeWorkOrdersService = collectionTypeWorkOrdersService ?? throw new ArgumentNullException($"{nameof(ICollectionTypeWorkOrdersService)} is null");
            _projectsService = projectsService ?? throw new ArgumentNullException($"{nameof(IProjectsService)} is null");
            _queueService = queueService ?? throw new ArgumentNullException($"{nameof(IQueueService)} is null");
            _zonesService = zonesService ?? throw new ArgumentNullException($"{nameof(IZonesService)} is null");
            _stateService = stateService ?? throw new ArgumentNullException($"{nameof(IStateService)} is null");
            _clientService = clientService ?? throw new ArgumentNullException($"{nameof(IStateService)} is null");
            _finalClientsServices = finalClientsServices ?? throw new ArgumentNullException($"{nameof(IFinalClientsServices)} is null");
            _workOrderCategoriesService = workOrderCategoriesService ?? throw new ArgumentNullException($"{nameof(IWorkOrderCategoriesService)} is null");
            _externalWorkOrderStatusService = externalWorkOrderStatusService ?? throw new ArgumentNullException($"{nameof(IExternalWorkOrderStatusService)} is null");
            _translationService = translationService ?? throw new ArgumentNullException($"{nameof(ITranslationService)} is null ");

            toolTips = new Dictionary<WorkOrderColumnsEnum, Func<int, List<int>, string>>()
            {
                { WorkOrderColumnsEnum.WorkOrderStatusId, GetMultiSelectWorkOrderStatus},
                { WorkOrderColumnsEnum.InsertedBy, GetMultiSelectInsertedBy },
                { WorkOrderColumnsEnum.WorkOrderType, GetMultiSelectWorkOrderType },
                { WorkOrderColumnsEnum.Project, GetMultiSelectProject },
                { WorkOrderColumnsEnum.SaltoClient, GetMultiSelectSaltoClient },
                { WorkOrderColumnsEnum.EndClient, GetMultiSelectEndClient },
                { WorkOrderColumnsEnum.Zone, GetMultiSelectZone },
                { WorkOrderColumnsEnum.ResponsiblePersonName, GetMultiSelectResponsiblePeople },
                { WorkOrderColumnsEnum.Queue, GetMultiSelectQueue },
                { WorkOrderColumnsEnum.Province, GetMultiSelectState },
                { WorkOrderColumnsEnum.Manufacturer, GetMultiSelectManufacturer },
                { WorkOrderColumnsEnum.WorkOrderCategory, GetMultiSelectWorkOrderCategory },
                { WorkOrderColumnsEnum.ExternalWorOrderStatus, GetMultiSelectExternalWorkOrderStatus }
            };
        }

        public string GetToolTipMultiSelect(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto)
        {
            string result = null;
            List<int> filters = new List<int>();
            if (!string.IsNullOrEmpty(multiSelectConfigurationViewDto.SelectedFilter))
            {
                filters = multiSelectConfigurationViewDto.SelectedFilter.Split(",").Select(x => Convert.ToInt32(x)).ToList();
                Func<int, List<int>, string> action;
                if (toolTips.TryGetValue((WorkOrderColumnsEnum)multiSelectConfigurationViewDto.ColumnId, out action))
                {
                    result = action.Invoke(multiSelectConfigurationViewDto.LanguageId, filters);
                }
            }
            return result;
        }

        public string GetToolTipDates(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto)
        {
            string text = _translationService.GetTranslationText(WorkOrderFilterConstants.WorkOrderFilterDateToolTip).Replace("{InitialDate}", multiSelectConfigurationViewDto.StartDate).Replace("{EndDate}", multiSelectConfigurationViewDto.EndDate);
            return text;
        }

        private string GetMultiSelectWorkOrderStatus(int languageId, List<int> selectedItems)
        {
            return _workOrderStatusService.GetNamesComaSeparated(languageId, selectedItems);
        }

        private string GetMultiSelectInsertedBy(int languageId, List<int> selectedItems)
        {
            return _peopleService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectWorkOrderType(int languageId, List<int> selectedItems)
        {
            return _collectionTypeWorkOrdersService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectSaltoClient(int languageId, List<int> selectedItems)
        {
            return _clientService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectEndClient(int languageId, List<int> selectedItems)
        {
            return _finalClientsServices.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectZone(int languageId, List<int> selectedItems)
        {
            return _zonesService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectResponsiblePeople(int languageId, List<int> selectedItems)
        {
            return _peopleService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectState(int languageId, List<int> selectedItems)
        {
            return _stateService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectManufacturer(int languageId, List<int> selectedItems)
        {
            return _peopleService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectWorkOrderCategory(int languageId, List<int> selectedItems)
        {
            return _workOrderCategoriesService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectProject(int languageId, List<int> selectedItems)
        {
            return _projectsService.GetNamesComaSeparated(selectedItems);
        }

        private string GetMultiSelectQueue(int languageId, List<int> selectedItems)
        {
            return _queueService.GetNamesComaSeparated(languageId, selectedItems);
        }

        private string GetMultiSelectExternalWorkOrderStatus(int languageId, List<int> selectedItems)
        {
            return _externalWorkOrderStatusService.GetNamesComaSeparated(languageId, selectedItems);
        }
    }
}