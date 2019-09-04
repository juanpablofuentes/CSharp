using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderAnalysis;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderOperationsViewModelExtensions
    {
        public static WorkOrderOperationsViewModel ToOperationsViewModel(this WorkOrderOperationsDto source)
        {
            WorkOrderOperationsViewModel result = new WorkOrderOperationsViewModel();
            if (source != null)
            {
                result.ResponseTime = source.WOAnalysis.ResponseTime;
                result.ResolutionTime = source.WOAnalysis.ResolutionTime;
                result.Visits = source.WOAnalysis.Visits.HasValue ? source.WOAnalysis.Visits.Value : 0;
                result.Interventions = source.WOAnalysis.Interventions.HasValue ? source.WOAnalysis.Interventions.Value : 0;
                result.SLAResponseDate = source.WOAnalysis.SLAResponseDate;
                result.SLAResolutionDate = source.WOAnalysis.SLAResolutionDate;
                result.SlaResponsePenaltyDate = source.WOAnalysis.SlaResponsePenaltyDate;
                result.SlaResolutionPenaltyDate = source.WOAnalysis.SlaResolutionPenaltyDate;
                result.MeetResolutionSLA = source.WOAnalysis.MeetResolutionSLA.HasValue? source.WOAnalysis.MeetResolutionSLA.Value? LocalizationsConstants.Yes: LocalizationsConstants.No : "-";
                result.MeetResolutionPenaltySla = source.WOAnalysis.MeetResolutionPenaltySla.HasValue? source.WOAnalysis.MeetResolutionPenaltySla.Value? LocalizationsConstants.Yes : LocalizationsConstants.No : "-";
                result.MeetResponseSLA = source.WOAnalysis.MeetResponseSLA.HasValue? source.WOAnalysis.MeetResponseSLA.Value? LocalizationsConstants.Yes : LocalizationsConstants.No : "-";
                result.MeetResponsePenaltySla = source.WOAnalysis.MeetResponsePenaltySla.HasValue? source.WOAnalysis.MeetResponsePenaltySla.Value? LocalizationsConstants.Yes : LocalizationsConstants.No : "-";
                result.Services = source.Services.ToListViewModel();
                result.Services.Add(source.TotalService.ToServicesViewModel());
            }
            return result;
        }

        public static IList<WorkOrderOperationsViewModel> ToListViewModel(this IList<WorkOrderOperationsDto> source)
        {
            return source?.MapList(x => x.ToOperationsViewModel());
        }
    }
}