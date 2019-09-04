using Group.Salto.Common.Constants.WorkOrder;
using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Contracts.States;
using Group.Salto.ServiceLibrary.Common.Contracts.WorkOrder;
using Group.Salto.ServiceLibrary.Common.Dtos.Grid;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderColumns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.WorkOrder
{
    public class WorkOrderQuerySelect : IWorkOrderQuerySelect
    {
        private readonly IDictionary<WorkOrderColumnsEnum, string> selectFields = null;
        private readonly IStateService _stateService;
        private string tempTables = string.Empty;

        public WorkOrderQuerySelect(IStateService stateService)
        {
            _stateService = stateService ?? throw new ArgumentNullException($"{nameof(IStateService)} is null ");
            selectFields = new Dictionary<WorkOrderColumnsEnum, string>()
            {
                { WorkOrderColumnsEnum.Id, $"wo.Id as Id, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.InternalIdentifier, $"wo.InternalIdentifier as InternalIdentifier, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ExternalIdentifier, $"wo.ExternalIdentifier as ExternalIdentifier, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.CreationDate, $"wo.CreationDate as CreationDate, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.AssignmentTime, $"wo.AssignmentTime as AssignmentTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.PickUpTime, $"wo.PickUpTime as PickUpTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.FinalClientClosingTime, $"wo.FinalClientClosingTime as FinalClientClosingTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.InternalClosingTime, $"wo.InternalClosingTime as InternalClosingTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.WorkOrderStatusId, $"ISNULL(wost.NameText, wos.[Name]) As WorkOrderStatusId_Name, wos.Color as WorkOrderStatusId_Color, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.TextRepair, $"left(wo.TextRepair, 100) as TextRepair, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Observations, $"left(wo.Observations, 100) as Observations, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.InsertedBy, $"pinserted.Name + ' ' + pinserted.FisrtSurname  + ' ' + pinserted.SecondSurname as InsertedBy, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.WorkOrderType, $"wot.Name as WorkOrderType, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Project, $"project.Name as Project, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.SaltoClient, $"client.ComercialName as SaltoClient, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.EndClient, $"finalClient.Name as EndClient, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ResolutionDateSla, $"wo.ResolutionDateSla as ResolutionDateSla, woc.SlaId as {WorkOrderConstants.SlaId}, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Phone, $"loc.Code as Phone, {Environment.NewLine}" }, 
                { WorkOrderColumnsEnum.Area, $"loc.Area as Area, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Zone, $"loc.Zone as Zone, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Subzona, $"loc.Subzone as Subzona, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.SiteName, $"loc.Name as SiteName, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.SitePhone, $"'' as SitePhone, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Address, $"loc.StreetType as Address_StreetType, loc.Street as Address_Street, loc.Number as Address_Number, loc.Escala as Address_Escala, loc.GateNumber as Address_GateNumber, loc.MunicipalityId as Address_MunicipalityId, loc.PostalCodeId as Address_PostalCodeId {Environment.NewLine}," },
                { WorkOrderColumnsEnum.ClosingCode, $"'' as ClosingCode, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ActionDate, $"wo.ActionDate as ActionDate, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ResponsiblePersonName, $"responsible.Name + ' ' + responsible.FisrtSurname  + ' ' + responsible.SecondSurname as ResponsiblePersonName, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Queue, $"q.Name as Queue, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.City, $"loc.MunicipalityId as City, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Province, $"s.[Name] as Province, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Manufacturer, $"manipulator.Name + ' ' + manipulator.FisrtSurname  + ' ' + manipulator.SecondSurname as Manufacturer, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.WorkOrderCategory, $"woc.Name as WorkOrderCategory, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ParentWOId, $"wo.WorkOrdersFatherId as ParentWOId, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ActuationEndDate, $"wo.ActuationEndDate as ActuationEndDate, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.NumWOsInSite, $"(select count(woLoc.id) from dbo.WorkOrders as woLoc with (nolock) full join dbo.WorkOrderStatuses as wosLoc with (nolock) on woLoc.WorkOrderStatusId = wosLoc.Id where woLoc.LocationId = wo.LocationId and wosLoc.IsWoclosed = 0) as NumWOsInSite, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.NumSubWOs, $"'' as NumSubWOs, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.SiteCode, $"loc.Code as SiteCode, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.AssetSerialNumber, $"assets.SerialNumber as AssetSerialNumber, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.AssetNumber, $"assets.AssetNumber as AssetNumber, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Maintenance, $"guarantee.StdStartDate as Maintenance_StdStartDate, guarantee.StdEndDate as Maintenance_StdEndDate, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.StandardWarranty, $"guarantee.BlnStartDate as StandardWarranty_BlnStartDate, guarantee.BlnEndDate as StandardWarranty_BlnEndDate, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ManufacturerWarranty, $"guarantee.ProStartDate as ManufacturerWarranty_ProStartDate, guarantee.ProEndDate as ManufacturerWarranty_ProEndDate, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ExternalWorOrderStatus, $"ewos.Name As ExternalWorOrderStatus_Name, ewos.Color As ExternalWorOrderStatus_Color, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.TotalWorkedTime, $"woa.TotalWorkedTime As TotalWorkedTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.OnSiteTime, $"woa.OnSiteTime As OnSiteTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.TravelTime, $"woa.TravelTime As TravelTime, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.Kilometers, $"woa.Kilometers As Kilometers, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.NumberOfVisitsToClient, $"woa.NumberOfVisitsToClient As NumberOfVisitsToClient, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.NumberOfIntervention, $"woa.NumberOfIntervention As NumberOfIntervention, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.MeetResolutionSLA, $"woa.MeetResolutionSLA As MeetResolutionSLA, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.MeetResponseSLA, $"woa.MeetResponseSLA As MeetResponseSLA, {Environment.NewLine}" },
                { WorkOrderColumnsEnum.ClosingWODate, $"woa.ClosingWODate As ClosingWODate, {Environment.NewLine}" },
            };
        }

        public string CreateCountSelect(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            string states = GetSelectStates(columns);
            StringBuilder select = new StringBuilder($"{states}{GetTempTables()} SET NOCOUNT ON; Select Count(wo.Id) ");

            return select.ToString();
        }

        public string CreateSelect(IList<WorkOrderColumnsDto> columns, GridDto gridConfig)
        {
            string states = GetSelectStates(columns);
            StringBuilder select = new StringBuilder($"{states}{GetTempTables()} SET NOCOUNT ON; SELECT ");
            foreach (WorkOrderColumnsDto col in columns)
            {
                string action;
                if (selectFields.TryGetValue((WorkOrderColumnsEnum)col.Id, out action))
                {
                    select.Append(action);
                }
            }
            select.Append($"wo.Id As {WorkOrderConstants.WorkOrderId} {Environment.NewLine}");

            return select.ToString();
        }

        private string GetSelectStates(IList<WorkOrderColumnsDto> columns)
        {
            WorkOrderColumnsDto existStates = columns.Where(x => x.Id == (int)WorkOrderColumnsEnum.Province).FirstOrDefault();
            string states = string.Empty;
            if (existStates != null)
            {
                states = GetProvinceTable();
            }

            return states;
        }

        private string GetProvinceTable()
        {
            string data = _stateService.GetStatesForInsert();
            string stateTable = $@"DECLARE @StateTable TABLE(StateId INT primary key, [Name] varchar(100) not null) 
INSERT INTO @StateTable values {data}{Environment.NewLine}";

            return stateTable;
        }

        private string GetTempTables()
        {
            string temptables = $@"
DECLARE @PermisionTable TABLE(PermisionId INT primary key)
INSERT INTO @PermisionTable
SELECT pp.PermissionId from dbo.PeoplePermissions as pp where pp.PeopleId = @peopleid
IF EXISTS (select 1 from @PermisionTable)
DECLARE @QueueTable TABLE(QueueId INT primary key)
INSERT INTO @QueueTable
SELECT distinct pq.QueueId FROM PermissionsQueues As pq WHERE pq.PermissionId IN (select p.PermisionId from @PermisionTable as p)
DECLARE @ProjectTable TABLE(projectId INT primary key)
INSERT INTO @ProjectTable
SELECT distinct pp.ProjectId FROM ProjectsPermissions As pp with(nolock) WHERE pp.PermissionId IN (select p.PermisionId from @PermisionTable as p)
DECLARE @WOCTable TABLE(WorkOrderCategoryId INT primary key)
INSERT INTO @WOCTable
SELECT distinct wocp.WorkOrderCategoryId FROM WorkOrderCategoryPermissions As wocp with(nolock) WHERE wocp.PermissionId IN (select p.PermisionId from @PermisionTable as p)

";
            return temptables;
        }
    }
}