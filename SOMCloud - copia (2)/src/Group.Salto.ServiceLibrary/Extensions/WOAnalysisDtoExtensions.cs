using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderAnalysis;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WOAnalysisDtoExtensions
    {
        public static WOAnalysisDto ToServiceListDto(this WorkOrderAnalysis source)
        {
            WOAnalysisDto result = null;
            if (source != null)
            {
                result = new WOAnalysisDto()
                {
                    Id = source.WorkOrderCode,
                    ResponseTime = source.ResponseTime.ToString(),
                    ResolutionTime = source.ResolutionTime.ToString(),
                    Visits = source.NumberOfVisitsToClient,
                    Interventions = source.NumberOfIntervention,
                    SLAResponseDate = source.SlaresponseDate.ToString(),
                    SLAResolutionDate = source.SlaresolutionDate.ToString(),
                    SlaResponsePenaltyDate = source.SlaResponsePenaltyDate.ToString(),
                    SlaResolutionPenaltyDate = source.SlaResolutionPenaltyDate.ToString(),
                    MeetResponsePenaltySla = source.MeetResponsePenaltySla,
                    MeetResolutionPenaltySla = source.MeetResolutionPenaltySla,
                    MeetResolutionSLA = source.MeetResolutionSla,
                    MeetResponseSLA = source.MeetResponseSla,
                };
            }
            return result;
        }

        public static IList<WOAnalysisDto> ToServiceListDto(this IList<WorkOrderAnalysis> source)
        {
            return source?.MapList(x => x.ToServiceListDto());
        }
    }
}