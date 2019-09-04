using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderSummaryDtoExtensions
    {
        public static WorkOrdersSummaryDto ToSummaryDto(this WorkOrders source)
        {
            WorkOrdersSummaryDto result = null;
            if(source != null)
            {
                result = new WorkOrdersSummaryDto
                {
                    ProjectName = source.Project?.Name,
                    ProjectObservations = source.Project?.Observations,
                    ContractName = source.Project?.Contract?.Object,
                    ContractObservations = source.Project?.Contract?.Observations,
                    FinalClientName = source.FinalClient?.Name,
                    FinalClientNIF = source.FinalClient?.Nif,
                    FinalClientTel1 = source.FinalClient?.Phone1,
                    FinalClientTel2 = source.FinalClient?.Phone2,
                    FinalClientTel3 = source.FinalClient?.Phone3,
                    FinalClientFax = source.FinalClient?.Fax,
                    FinalClientObservations = source.FinalClient?.Observations,
                    LocationName = source.Location?.Name,
                    LocationZone = source.Location?.Zone,
                    LocationSubZone = source.Location?.Subzone,
                    LocationCity = source.Location?.City,
                    LocationPostalCode = source.Location?.PostalCode,
                    LocationProvince = source.Location?.Province,
                    AssignationQueue = source.Queue?.Name,
                    AssignationTechnician = source.PeopleResponsible?.FullName,
                    WorkOrderFatherID = source.WorkOrdersFatherId,
                    WorkOrderType = source.WorkOrderTypes?.Name,
                    WorkOrderCategory = source.WorkOrderCategory?.Name,
                    WorkOrderClient = source.InternalIdentifier,
                    WorkOrderClientSite = source.ExternalIdentifier,
                    WorkOrderStatus = source.WorkOrderStatus.Name,
                    WorkOrderExternalStatus = source.ExternalWorOrderStatus?.Name,
                    WorkOrderRepair = source.TextRepair,
                    TimingCreation = source.CreationDate.ToString(),
                    TimingReception = source.PickUpTime.ToString(),
                    TimingAssigned = source.AssignmentTime.ToString(),
                    TimingPerformance = source.ActionDate.ToString(),
                    TimingFinalPerformance = source.ActuationEndDate.ToString(),
                    TimingCloseClient = source.FinalClientClosingTime.ToString(),
                    TimingCloseClientSalto = source.InternalClosingTime.ToString(),
                    TimingSLAResponse = source.ResponseDateSla.ToString(),
                    TimingFinalSLA = source.ResolutionDateSla.ToString(),
                    TimingResponsePenalty = source.DateUnansweredPenaltySla.ToString(),
                    TimingBreachPenalty = source.DatePenaltyWithoutResolutionSla.ToString(),
                    TimingStopSLA = source.DateStopTimerSla.ToString(),
                };
            }
            return result;
        }
    }
}