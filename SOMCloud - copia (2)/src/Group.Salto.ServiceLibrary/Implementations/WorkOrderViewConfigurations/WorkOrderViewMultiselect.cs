using Group.Salto.Common.Enums;
using Group.Salto.Log;
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
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderViewConfigurations
{
    public class WorkOrderViewMultiselect : BaseService, IWorkOrderViewMultiselect
    {
        private readonly IDictionary<WorkOrderColumnsEnum, Func<MultiSelectConfigurationViewDto, List<int>, ResultDto<List<MultiSelectItemDto>>>> multiselect = null;

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

        public WorkOrderViewMultiselect(ILoggingService logginingService,
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
                                        IExternalWorkOrderStatusService externalWorkOrderStatusService) : base(logginingService)
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

            multiselect = new Dictionary<WorkOrderColumnsEnum, Func<MultiSelectConfigurationViewDto, List<int>, ResultDto<List<MultiSelectItemDto>>>>()
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

        public ResultDto<List<MultiSelectItemDto>> GetMultiSelect(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto)
        {
            ResultDto<List<MultiSelectItemDto>> result = null;
            List<int> filters = new List<int>();
            if (!string.IsNullOrEmpty(multiSelectConfigurationViewDto.SelectedFilter))
            {
                filters = multiSelectConfigurationViewDto.SelectedFilter.Split(",").Select(x => Convert.ToInt32(x)).ToList();
            }
            Func<MultiSelectConfigurationViewDto, List<int>, ResultDto<List<MultiSelectItemDto>>> action;
            if (multiselect.TryGetValue((WorkOrderColumnsEnum)multiSelectConfigurationViewDto.ColumnId, out action))
            {
                result = action.Invoke(multiSelectConfigurationViewDto, filters);
            }

            return result;
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectWorkOrderStatus(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _workOrderStatusService.GetWorkOrderStatusMultiSelect(multiSelectConfigurationViewDto.LanguageId, selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectInsertedBy(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _peopleService.GetActivePeopleMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectWorkOrderType(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _collectionTypeWorkOrdersService.GetWorkOrderTypesMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectSaltoClient(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _clientService.GetClientMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectEndClient(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _finalClientsServices.GetFinalClientMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectZone(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _zonesService.GetZoneMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectResponsiblePeople(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _peopleService.GetActivePeopleMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectState(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _stateService.GetStatesMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectManufacturer(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _peopleService.GetActivePeopleMultiSelect(selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectWorkOrderCategory(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _workOrderCategoriesService.GeWorkOrderCategoryMultiSelect(selectedItems, multiSelectConfigurationViewDto.UserId);
        }
 
        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectProject(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _projectsService.GetProjectMultiSelect(selectedItems, multiSelectConfigurationViewDto.UserId);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectQueue(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _queueService.GetQueueMultiSelect(multiSelectConfigurationViewDto, selectedItems);
        }

        private ResultDto<List<MultiSelectItemDto>> GetMultiSelectExternalWorkOrderStatus(MultiSelectConfigurationViewDto multiSelectConfigurationViewDto, List<int> selectedItems)
        {
            return _externalWorkOrderStatusService.GetExternalWorkOrderStatusMultiSelect(multiSelectConfigurationViewDto.LanguageId, selectedItems);
        }
    }
}