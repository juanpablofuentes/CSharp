using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Contracts.Analysis;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Analysis;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Location;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Sla;

namespace Group.Salto.ServiceLibrary.Implementations.Analysis
{
    public class WoAnalysisService : IWoAnalysisService
    {
        public ResultDto<bool> AddWoAnalysis(WorkOrders currentWorkOrder)
        {
            var result = new ResultDto<bool> { Data = true };
            try
            {
                var woAnalysisValues = GetWoAnalysisValues(currentWorkOrder);
                var locationAnalysisValues = GetLocationAnalysisValues(currentWorkOrder);
                var slaAnalysisValues = GetSlaAnalysisValues(currentWorkOrder);

                var newAnalysis = new WorkOrderAnalysis
                {
                    WorkOrderCode = currentWorkOrder.Id,
                    WorkOrderClientCode = currentWorkOrder.InternalIdentifier,
                    WorkOrderCampainCode = currentWorkOrder.ExternalIdentifier,
                    InternalAssetCode = currentWorkOrder.AssetId,
                    ProjectCode = currentWorkOrder.ProjectId,
                    AssignedTechnicianCode = currentWorkOrder.PeopleResponsibleId ?? 0,
                    ClientCreationDate = currentWorkOrder.CreationDate,
                    InternalCreationDate = currentWorkOrder.InternalCreationDate ?? currentWorkOrder.PickUpTime ?? currentWorkOrder.CreationDate,
                    ActuationDate = currentWorkOrder.ActionDate,
                    ClosingClientDate = currentWorkOrder.ClientClosingDate,
                    InternalSystemTimeWhenOtclosed = currentWorkOrder.SystemDateWhenOtclosed,
                    AccountingClosingDate = currentWorkOrder.AccountingClosingDate,
                    SlaresolutionDate = currentWorkOrder.ResolutionDateSla,
                    SlaResolutionPenaltyDate = currentWorkOrder.DatePenaltyWithoutResolutionSla,
                    SlaresponseDate = currentWorkOrder.ResponseDateSla,
                    SlaResponsePenaltyDate = currentWorkOrder.DateUnansweredPenaltySla,
                    MeetResponseSla = slaAnalysisValues.MeetSlaResponse,
                    MeetResolutionSla = slaAnalysisValues.MeetSlaResolution,
                    ResolutionTime = slaAnalysisValues.ResolTime,
                    MeetResponsePenaltySla = slaAnalysisValues.MeetSlaResponsePenalty,
                    MeetResolutionPenaltySla = slaAnalysisValues.MeetSlaResolutionPenalty,
                    TotalWorkedTime = woAnalysisValues.WorkedTime,
                    ExpectedTimeWorked = woAnalysisValues.EstimatedTime,
                    OnSiteTime = woAnalysisValues.OnsiteTime,
                    TravelTime = woAnalysisValues.TravelTime,
                    WaitTime = woAnalysisValues.WaitTime,
                    Kilometers = woAnalysisValues.Kilometers,
                    Tolls = woAnalysisValues.Tolls,
                    Parking = woAnalysisValues.Parking,
                    Expenses = woAnalysisValues.Mexpenses,
                    OtherCosts = woAnalysisValues.Oexpenses,
                    WorkOrderCategory = currentWorkOrder.WorkOrderCategoryId,
                    NumberOfIntervention = woAnalysisValues.NumberOfIntervention,
                    NumberOfVisitsToClient = woAnalysisValues.NumberOfVisitsToClient,
                    Otstatus = currentWorkOrder.WorkOrderStatusId,
                    ExternalOtstatus = currentWorkOrder.ExternalWorOrderStatusId,
                    FinalClientCode = currentWorkOrder.FinalClientId,
                    FinalClientName = currentWorkOrder.FinalClient.Name,
                    LocationCode = locationAnalysisValues.Code,
                    LocationClientCode = locationAnalysisValues.ClientCode,
                    LocationName = locationAnalysisValues.Name,
                    LocationAddress = locationAnalysisValues.Address,
                    LocationCity = locationAnalysisValues.City,
                    LocationTown = locationAnalysisValues.Town,
                    LocationState = locationAnalysisValues.State,
                    LocationRegion = locationAnalysisValues.Region,
                    LocationCountry = locationAnalysisValues.Country,
                    LocationPostalCode = locationAnalysisValues.PostalCode?.ToString(),
                    LocationObservation = locationAnalysisValues.Description,
                    ClosingClientTime = currentWorkOrder.ClientClosingDate ?? currentWorkOrder.ClosingOtdate,
                    ClosingSystemDate = currentWorkOrder.SystemDateWhenOtclosed ?? currentWorkOrder.ClosingOtdate,
                    ClosingWodate = currentWorkOrder.ClosingOtdate ?? DateTime.UtcNow,
                    TotalWosalesAmount = woAnalysisValues.SalesAmount,
                    TotalWoproductionCost = woAnalysisValues.ProductionCost,
                    TotalWosubcontractorCost = woAnalysisValues.SubcontractorCost,
                    TotalWotravelTimeCost = woAnalysisValues.TravelCost,
                    TotalWomaterialsCost = woAnalysisValues.MaterialCost,
                    TotalWoexpensesCost = woAnalysisValues.ExpenseCost,
                    GrossMargin = woAnalysisValues.Margin,
                    ExpectedvsWorkedTime = woAnalysisValues.PercentWorked,
                    TotalTime = woAnalysisValues.TravelTime + woAnalysisValues.WaitTime + woAnalysisValues.OnsiteTime,
                    WorkOrders = currentWorkOrder
                };

                currentWorkOrder.WorkOrderAnalysis = newAnalysis;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Errors = new ErrorsDto { Errors = new List<ErrorDto> { new ErrorDto { ErrorType = ErrorType.ValidationError, ErrorMessageKey = e.ToString() } } };
            }

            return result;
        }

        public ResultDto<bool> UpdateWoAnalysis(WorkOrders currentWorkOrder)
        {
            var result = new ResultDto<bool>{Data = true};
            try
            {
                var woAnalysisValues = GetWoAnalysisValues(currentWorkOrder);

                currentWorkOrder.WorkOrderAnalysis.AssignedTechnicianCode = currentWorkOrder.PeopleResponsible.Id;
                currentWorkOrder.WorkOrderAnalysis.ActuationDate = currentWorkOrder.ActionDate;
                currentWorkOrder.WorkOrderAnalysis.ClosingClientDate = currentWorkOrder.ClientClosingDate;
                currentWorkOrder.WorkOrderAnalysis.InternalSystemTimeWhenOtclosed = currentWorkOrder.SystemDateWhenOtclosed;
                currentWorkOrder.WorkOrderAnalysis.AccountingClosingDate = currentWorkOrder.AccountingClosingDate;
                currentWorkOrder.WorkOrderAnalysis.TotalWorkedTime = woAnalysisValues.WorkedTime;
                currentWorkOrder.WorkOrderAnalysis.ExpectedTimeWorked = woAnalysisValues.EstimatedTime;
                currentWorkOrder.WorkOrderAnalysis.OnSiteTime = woAnalysisValues.OnsiteTime;
                currentWorkOrder.WorkOrderAnalysis.TravelTime = woAnalysisValues.TravelTime;
                currentWorkOrder.WorkOrderAnalysis.WaitTime = woAnalysisValues.WaitTime;
                currentWorkOrder.WorkOrderAnalysis.Kilometers = woAnalysisValues.Kilometers;
                currentWorkOrder.WorkOrderAnalysis.Tolls = woAnalysisValues.Tolls;
                currentWorkOrder.WorkOrderAnalysis.Parking = woAnalysisValues.Parking;
                currentWorkOrder.WorkOrderAnalysis.Expenses = woAnalysisValues.Mexpenses;
                currentWorkOrder.WorkOrderAnalysis.OtherCosts = woAnalysisValues.Oexpenses;
                currentWorkOrder.WorkOrderAnalysis.NumberOfIntervention = woAnalysisValues.NumberOfIntervention;
                currentWorkOrder.WorkOrderAnalysis.NumberOfVisitsToClient = woAnalysisValues.NumberOfVisitsToClient;
                currentWorkOrder.WorkOrderAnalysis.Otstatus = currentWorkOrder.WorkOrderStatusId;
                currentWorkOrder.WorkOrderAnalysis.ExternalOtstatus = currentWorkOrder.ExternalWorOrderStatusId;
                currentWorkOrder.WorkOrderAnalysis.ClosingClientTime = currentWorkOrder.ClientClosingDate ?? currentWorkOrder.ClosingOtdate;
                currentWorkOrder.WorkOrderAnalysis.ClosingSystemDate = currentWorkOrder.SystemDateWhenOtclosed ?? currentWorkOrder.ClosingOtdate;
                currentWorkOrder.WorkOrderAnalysis.ClosingWodate = currentWorkOrder.ClosingOtdate ?? DateTime.UtcNow;
                currentWorkOrder.WorkOrderAnalysis.TotalWosalesAmount = woAnalysisValues.SalesAmount;
                currentWorkOrder.WorkOrderAnalysis.TotalWoproductionCost = woAnalysisValues.ProductionCost;
                currentWorkOrder.WorkOrderAnalysis.TotalWosubcontractorCost = woAnalysisValues.SubcontractorCost;
                currentWorkOrder.WorkOrderAnalysis.TotalWotravelTimeCost = woAnalysisValues.TravelCost;
                currentWorkOrder.WorkOrderAnalysis.TotalWomaterialsCost = woAnalysisValues.MaterialCost;
                currentWorkOrder.WorkOrderAnalysis.TotalWoexpensesCost = woAnalysisValues.ExpenseCost;
                currentWorkOrder.WorkOrderAnalysis.GrossMargin = woAnalysisValues.Margin;
                currentWorkOrder.WorkOrderAnalysis.ExpectedvsWorkedTime = woAnalysisValues.PercentWorked;
                currentWorkOrder.WorkOrderAnalysis.TotalTime = woAnalysisValues.TravelTime + woAnalysisValues.WaitTime + woAnalysisValues.OnsiteTime;
            }
            catch (Exception e)
            {
                result.Data = false;
                result.Errors = new ErrorsDto{Errors = new List<ErrorDto> { new ErrorDto{ErrorType = ErrorType.ValidationError, ErrorMessageKey = e.ToString()}}};
            }

            return result;
        }

        private SlaAnalysisValuesDto GetSlaAnalysisValues(WorkOrders currentWorkOrder)
        {
            var slaAnalysisValues = new SlaAnalysisValuesDto();

            if (currentWorkOrder.ResponseDateSla.HasValue)
            {
                slaAnalysisValues.MeetSlaResponse = currentWorkOrder.ResponseDateSla > currentWorkOrder.ClosingOtdate;
            }

            if (currentWorkOrder.ResolutionDateSla.HasValue)
            {
                slaAnalysisValues.MeetSlaResolution = currentWorkOrder.ResolutionDateSla > currentWorkOrder.ClosingOtdate;
                slaAnalysisValues.ResolTime = (currentWorkOrder.ResolutionDateSla - currentWorkOrder.ClosingOtdate)?.Minutes;
            }

            if (currentWorkOrder.DateUnansweredPenaltySla.HasValue)
            {
                slaAnalysisValues.MeetSlaResponsePenalty = currentWorkOrder.DateUnansweredPenaltySla > currentWorkOrder.ClosingOtdate;
            }

            if (currentWorkOrder.DatePenaltyWithoutResolutionSla.HasValue)
            {
                slaAnalysisValues.MeetSlaResolutionPenalty = currentWorkOrder.DatePenaltyWithoutResolutionSla > currentWorkOrder.ClosingOtdate;
            }

            return slaAnalysisValues;
        }

        private LocationAnalysisValuesDto GetLocationAnalysisValues(WorkOrders currentWorkOrder)
        {
            var locationAnalysisValues = new LocationAnalysisValuesDto();

            locationAnalysisValues.Code = currentWorkOrder.LocationId;
            locationAnalysisValues.Name = currentWorkOrder.Location.Name;
            locationAnalysisValues.Address = $"{currentWorkOrder.Location.StreetType} {currentWorkOrder.Location.Street} {currentWorkOrder.Location.Number} {currentWorkOrder.Location.Escala} {currentWorkOrder.Location.GateNumber}";
            locationAnalysisValues.City = currentWorkOrder.Location.City;
            locationAnalysisValues.State = currentWorkOrder.Location.Province;
            locationAnalysisValues.Region = currentWorkOrder.Location.Zone;
            locationAnalysisValues.PostalCode = currentWorkOrder.Location.PostalCode;
            locationAnalysisValues.Country = currentWorkOrder.Location.Country;
            locationAnalysisValues.Town = currentWorkOrder.Location.City;
            locationAnalysisValues.Description = currentWorkOrder.Location.Observations;
            locationAnalysisValues.ClientCode = currentWorkOrder.Location.LocationsFinalClients.FirstOrDefault()?.CompositeCode;

            return locationAnalysisValues;
        }

        private AnalysisWoValuesDto GetWoAnalysisValues(WorkOrders currentWorkOrder)
        {
            var woAnalysisValues = new AnalysisWoValuesDto();

            var materialAnalysis = currentWorkOrder.Bill.SelectMany(b => b.BillLine).Select(b => b.DnAndMaterialsAnalysis).Where(dn => dn != null);

            woAnalysisValues.WorkedTime = currentWorkOrder.ServicesAnalysis.Sum(s => s.WorkedTime) ?? 0;
            woAnalysisValues.EstimatedTime = (int?)currentWorkOrder.WorkOrderCategory.EstimatedDuration;
            woAnalysisValues.OnsiteTime = currentWorkOrder.ServicesAnalysis.Sum(s => s.OnSiteTime) ?? 0;
            woAnalysisValues.TravelTime = currentWorkOrder.ServicesAnalysis.Sum(s => s.TravelTime) ?? 0;
            woAnalysisValues.WaitTime = currentWorkOrder.ServicesAnalysis.Sum(s => s.WaitTime) ?? 0;
            woAnalysisValues.Kilometers = currentWorkOrder.ServicesAnalysis.Sum(s => s.Kilometers) ?? 0;
            woAnalysisValues.SalesAmount = materialAnalysis.Sum(s => s.TotalDeliveryNoteSalePrice) ?? 0;
            woAnalysisValues.ProductionCost = currentWorkOrder.ServicesAnalysis.Sum(s => s.ProductionCost) ?? 0;
            woAnalysisValues.SubcontractCost = currentWorkOrder.ServicesAnalysis.Sum(s => s.SubcontractorCost) ?? 0;
            woAnalysisValues.TravelCost = currentWorkOrder.ServicesAnalysis.Sum(s => s.TravelTimeCost) ?? 0;
            woAnalysisValues.MaterialCost = materialAnalysis.Sum(s => s.TotalDeliveryNoteCost) ?? 0;
            woAnalysisValues.WaitCost = currentWorkOrder.ServicesAnalysis.Sum(s => s.WaitTime * s.StandardPersonCost) / 60;
            woAnalysisValues.KmCost = currentWorkOrder.ServicesAnalysis.Sum(s => s.Kilometers * s.KmCost);
            woAnalysisValues.Margin = woAnalysisValues.SalesAmount -
                                      (woAnalysisValues.ProductionCost + woAnalysisValues.SubcontractCost +
                                       woAnalysisValues.TravelCost + woAnalysisValues.MaterialCost +
                                       woAnalysisValues.ExpenseCost + woAnalysisValues.WaitCost +
                                       woAnalysisValues.KmCost);
            woAnalysisValues.PercentWorked = woAnalysisValues.EstimatedTime > 0 ? woAnalysisValues.WorkedTime > woAnalysisValues.EstimatedTime ?
                    ((woAnalysisValues.WorkedTime - woAnalysisValues.EstimatedTime) / woAnalysisValues.EstimatedTime * 100) : 0 : -1;

            woAnalysisValues.NumberOfIntervention = currentWorkOrder.ServicesAnalysis.Count;
            woAnalysisValues.NumberOfVisitsToClient = currentWorkOrder.ServicesAnalysis.Count(s => s.Kilometers != null);

            //expenses
            woAnalysisValues.ExpenseCost = currentWorkOrder.ExpensesTickets?.SelectMany(et => et.Expenses).Sum(e => e.Amount * (decimal)e.Factor)?? 0;

            woAnalysisValues.Tolls = currentWorkOrder.ExpensesTickets.SelectMany(et => et.Expenses).Where(e => e.ExpenseTypeId == (int) ExpenseTypeEnum.Freeway).Sum(e => e.Amount);
            woAnalysisValues.Parking = currentWorkOrder.ExpensesTickets.SelectMany(et => et.Expenses).Where(e => e.ExpenseTypeId == (int) ExpenseTypeEnum.Parking).Sum(e => e.Amount);
            woAnalysisValues.Mexpenses = currentWorkOrder.ExpensesTickets.SelectMany(et => et.Expenses).Where(e =>
                e.ExpenseTypeId == (int) ExpenseTypeEnum.Transport ||
                e.ExpenseTypeId == (int) ExpenseTypeEnum.Housing || 
                e.ExpenseTypeId == (int) ExpenseTypeEnum.Fuel ||
                e.ExpenseTypeId == (int) ExpenseTypeEnum.Diets)
                .Sum(e => e.Amount);
            woAnalysisValues.Oexpenses = currentWorkOrder.ExpensesTickets.SelectMany(et => et.Expenses).Where(e => e.ExpenseTypeId == (int) ExpenseTypeEnum.Freeway).Sum(e => e.Amount);

            return woAnalysisValues;
        }
    }
}
