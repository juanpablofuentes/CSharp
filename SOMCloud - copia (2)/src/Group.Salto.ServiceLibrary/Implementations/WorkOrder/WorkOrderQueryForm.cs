using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderQueryForm: IWorkOrderQueryForm
    {
        private IDictionary<WorkOrderColumnsEnum, List<string>> formJoins = null;
        private StringBuilder form;

        public WorkOrderQueryForm()
        {
            form = new StringBuilder($@"FROM dbo.WorkOrders as wo with (nolock){Environment.NewLine}");

            formJoins = new Dictionary<WorkOrderColumnsEnum, List<string>>()
            {
                { WorkOrderColumnsEnum.Project, new List<string>(){$"full join dbo.Projects as project with (nolock) on project.Id = wo.ProjectId and project.IsDeleted = 0 {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Queue, new List<string>(){$"full join dbo.Queues as q with (nolock) on q.Id = wo.QueueId and q.IsDeleted = 0 {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.WorkOrderCategory, new List<string>(){$"full join dbo.WorkOrderCategories as woc with (nolock) on woc.Id = wo.WorkOrderCategoryId and woc.IsDeleted = 0 {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.WorkOrderStatusId, new List<string>(){ $"full join dbo.WorkOrderStatuses as wos with (nolock) on wos.Id = wo.WorkOrderStatusId {Environment.NewLine}", $"full join WorkOrderStatusesTranslations as wost with (nolock) on wost.WorkOrderStatusesId = wos.Id and LanguageId = [languageId] {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.InsertedBy, new List<string>(){$"full join dbo.People as pinserted with (nolock) on pinserted.Id = wo.PeopleResponsibleId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.WorkOrderType, new List<string>(){$"full join dbo.WorkOrderTypes as wot with (nolock) on wot.Id = wo.WorkOrderTypesId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.SaltoClient, new List<string>(){ $"full join dbo.Projects as project with (nolock) on project.Id = wo.ProjectId and project.IsDeleted = 0 {Environment.NewLine}", $"full join dbo.Contracts as [contract] with (nolock) on [contract].Id = project.ContractId {Environment.NewLine}", $"full join dbo.Clients as client with (nolock) on client.id = [contract].ClientId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.EndClient, new List<string>(){$"full join dbo.FinalClients as finalClient with (nolock) on finalClient.Id = wo.FinalClientId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.ExternalWorOrderStatus, new List<string>(){$"full join dbo.ExternalWorOrderStatuses as ewos with (nolock) on ewos.Id = wo.ExternalWorOrderStatusId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Phone, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}", $"full join dbo.LocationsFinalClients as locfinalclient with (nolock) on locfinalclient.FinalClientId = (select top 1 FinalClientId from dbo.LocationsFinalClients where FinalClientId = loc.Id) {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Area, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Zone, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Subzona, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.SiteName, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.SitePhone, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}", $"full join dbo.LocationsFinalClients as locfinalclient with (nolock) on locfinalclient.FinalClientId = (select top 1 FinalClientId from dbo.LocationsFinalClients where FinalClientId = loc.Id) {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Address, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.ResponsiblePersonName, new List<string>(){$"full join dbo.People as responsible with (nolock) on responsible.Id = wo.PeopleResponsibleId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.City, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Province, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}", $"full join @StateTable as s on s.StateId = loc.StateId{Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Manufacturer, new List<string>(){$"full join dbo.People as manipulator with (nolock) on manipulator.Id = wo.PeopleManipulatorId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.SiteCode, new List<string>(){$"full join dbo.Locations as loc with (nolock) on loc.Id = wo.LocationId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.AssetSerialNumber, new List<string>(){$"full join dbo.Assets as assets with (nolock) on assets.Id = wo.AssetId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.AssetNumber, new List<string>(){$"full join dbo.Assets as assets with (nolock) on assets.Id = wo.AssetId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Maintenance, new List<string>(){$"full join dbo.Assets as assets with (nolock) on assets.Id = wo.AssetId {Environment.NewLine}", $"full join dbo.Guarantee as guarantee with (nolock) on guarantee.Id = assets.GuaranteeId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.StandardWarranty, new List<string>(){$"full join dbo.Assets as assets with (nolock) on assets.Id = wo.AssetId {Environment.NewLine}", $"full join dbo.Guarantee as guarantee with (nolock) on guarantee.Id = assets.GuaranteeId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.ManufacturerWarranty, new List<string>(){$"full join dbo.Assets as assets with (nolock) on assets.Id = wo.AssetId {Environment.NewLine}", $"full join dbo.Guarantee as guarantee with (nolock) on guarantee.Id = assets.GuaranteeId {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.TotalWorkedTime, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.OnSiteTime, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.TravelTime, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.Kilometers, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.NumberOfVisitsToClient, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.NumberOfIntervention, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.MeetResolutionSLA, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.MeetResponseSLA, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }},
                { WorkOrderColumnsEnum.ClosingWODate, new List<string>(){$"left join dbo.WorkOrderAnalysis as woa on woa.WorkOrderCode = wo.Id {Environment.NewLine}" }}
            };
        }

        public string CreateForm(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            foreach (WorkOrderColumnsDto col in columns)
            {
                ExecuteAction(form, (WorkOrderColumnsEnum)col.Id);
            }
            return CreateCountForm(columns, gridConfig);
        }

        public string CreateCountForm(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            CreateFromWhere(form, gridConfig);
            form.Replace("[languageId]", gridConfig.LanguageId.ToString());
            return form.ToString();
        }

        private void ExecuteAction(StringBuilder form, WorkOrderColumnsEnum colId)
        {
            List<string> actionList;
            if (formJoins.TryGetValue(colId, out actionList))
            {
                foreach (string action in actionList)
                {
                    if (!form.ToString().Contains(action))
                    {
                        form.Append(action);
                    }
                }
            }
        }

        private string CreateFromWhere(StringBuilder form, GridDto gridConfig)
        {
            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.SerialNumber))
            {
                ExecuteAction(form, WorkOrderColumnsEnum.AssetSerialNumber);
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.LocationCode))
            {
                ExecuteAction(form, WorkOrderColumnsEnum.SiteCode);
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.StateIds))
            {
                ExecuteAction(form, WorkOrderColumnsEnum.Province);
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.SaltoClient))
            {
                ExecuteAction(form, WorkOrderColumnsEnum.SaltoClient);
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.EndClient))
            {
                ExecuteAction(form, WorkOrderColumnsEnum.EndClient);
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.Zone))
            {
                ExecuteAction(form, WorkOrderColumnsEnum.Zone);
            }

            if (!string.IsNullOrEmpty(gridConfig.WorkOrderFilters.WorkOrderSearch?.SearchString))
            {
                switch(gridConfig.WorkOrderFilters.WorkOrderSearch.SearchType)
                {
                    case WorkOrderSearchEnum.Location:
                        ExecuteAction(form, WorkOrderColumnsEnum.SiteCode);
                        break;

                    case WorkOrderSearchEnum.Active:
                        ExecuteAction(form, WorkOrderColumnsEnum.AssetSerialNumber);
                        break;
                    default:
                        break;
                }
            }

            return form.ToString();
        }
    }
}