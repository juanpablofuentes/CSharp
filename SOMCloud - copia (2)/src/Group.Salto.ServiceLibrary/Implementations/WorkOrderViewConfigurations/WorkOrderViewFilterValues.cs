using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderViewConfigurations;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrderViewConfigurations
{
    public class WorkOrderViewFilterValues : BaseService, IWorkOrderViewFilterValues
    {
        private readonly IDictionary<WorkOrderColumnsEnum, Action<MainWoViewConfigurationsColumns, WorkOrderFiltersDto>> filter = null;

        public WorkOrderViewFilterValues(ILoggingService logginingService) : base(logginingService)
        {
            filter = new Dictionary<WorkOrderColumnsEnum, Action<MainWoViewConfigurationsColumns, WorkOrderFiltersDto>>()
            {
                { WorkOrderColumnsEnum.WorkOrderStatusId, GetFilterValueWorkOrderStatus},
                { WorkOrderColumnsEnum.InsertedBy, GetFilterValueInsertedBy },
                { WorkOrderColumnsEnum.WorkOrderType, GetFilterValueWorkOrderType },
                { WorkOrderColumnsEnum.Project, GetFilterValueProject },
                { WorkOrderColumnsEnum.SaltoClient, GetFilterValueSaltoClient },
                { WorkOrderColumnsEnum.EndClient, GetFilterValueEndClient },
                { WorkOrderColumnsEnum.Zone, GetFilterValueZone },
                { WorkOrderColumnsEnum.ResponsiblePersonName, GetFilterValueResponsiblePeople },
                { WorkOrderColumnsEnum.Queue, GetFilterValueQueue },
                { WorkOrderColumnsEnum.Province, GetFilterValueState },
                { WorkOrderColumnsEnum.Manufacturer, GetFilterValueManufacturer },
                { WorkOrderColumnsEnum.WorkOrderCategory, GetFilterValueWorkOrderCategory },
                { WorkOrderColumnsEnum.ExternalWorOrderStatus, GetFilterValueExternalWorkOrderStatus },
                { WorkOrderColumnsEnum.CreationDate, GetFilterValueCreationDate },
                { WorkOrderColumnsEnum.ActionDate, GetFilterValueActionDate },
                { WorkOrderColumnsEnum.AssignmentTime, GetFilterValueAssignmentTime },
                { WorkOrderColumnsEnum.PickUpTime, GetFilterValuePickUpTime },
                { WorkOrderColumnsEnum.FinalClientClosingTime, GetFilterValueFinalClientClosingTime },
                { WorkOrderColumnsEnum.InternalClosingTime, GetFilterValueInternalClosingTime }
            };
        }

        public WorkOrderFiltersDto GetFilterValues(UsersMainWoviewConfigurations data)
        {
            WorkOrderFiltersDto valueFilters = new WorkOrderFiltersDto();
            foreach (MainWoViewConfigurationsColumns column in data.MainWoViewConfigurationsColumns)
            {
                Action<MainWoViewConfigurationsColumns, WorkOrderFiltersDto> action;
                if (filter.TryGetValue((WorkOrderColumnsEnum)column.ColumnId, out action))
                {
                    action.Invoke(column, valueFilters);
                }
            }
            return valueFilters;
        }

        private void GetFilterValueWorkOrderStatus(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.WorkOrderStatusIds = data.FilterValues;
        }

        private void GetFilterValueInsertedBy(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.InsertedBy = data.FilterValues;
        }

        private void GetFilterValueWorkOrderType(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.WorkOrderType = data.FilterValues;
        }

        private void GetFilterValueProject(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.ProjectIds = data.FilterValues;
        }

        private void GetFilterValueSaltoClient(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.SaltoClient = data.FilterValues;
        }

        private void GetFilterValueEndClient(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.EndClient = data.FilterValues;
        }

        private void GetFilterValueZone(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.Zone = data.FilterValues;
        }

        private void GetFilterValueResponsiblePeople(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.ResponsiblesIds = data.FilterValues;
        }

        private void GetFilterValueState(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.StateIds = data.FilterValues;
        }

        private void GetFilterValueManufacturer(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.Manipulator = data.FilterValues;
        }

        private void GetFilterValueWorkOrderCategory(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.WorkOrderCategoryIds = data.FilterValues;
        }

        private void GetFilterValueQueue(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.WorkOrderQueueIds = data.FilterValues;
        }

        private void GetFilterValueExternalWorkOrderStatus(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.ExternalWorOrderStatus = data.FilterValues;
        }

        private void GetFilterValueCreationDate(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.CreationStartDate = data.FilterStartDate;
            valueFilter.CreationEndDate = data.FilterEndDate;
        }

        private void GetFilterValueActionDate(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.ActionDateStartDate = data.FilterStartDate;
            valueFilter.ActionDateEndDate = data.FilterEndDate;
        }

        private void GetFilterValueAssignmentTime(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.AssignmentTimeStartDate = data.FilterStartDate;
            valueFilter.AssignmentTimeEndDate = data.FilterEndDate;
        }

        private void GetFilterValuePickUpTime(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.PickUpTimeStartDate = data.FilterStartDate;
            valueFilter.PickUpTimeEndDate = data.FilterEndDate;
        }

        private void GetFilterValueFinalClientClosingTime(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.FinalClientClosingTimeStartDate = data.FilterStartDate;
            valueFilter.FinalClientClosingTimeEndDate = data.FilterEndDate;
        }

        private void GetFilterValueInternalClosingTime(MainWoViewConfigurationsColumns data, WorkOrderFiltersDto valueFilter)
        {
            valueFilter.InternalClosingTimeStartDate = data.FilterStartDate;
            valueFilter.InternalClosingTimeEndDate = data.FilterEndDate;
        }
    }
}