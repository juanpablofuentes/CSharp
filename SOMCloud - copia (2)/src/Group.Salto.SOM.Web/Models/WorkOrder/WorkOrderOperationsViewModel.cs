using Group.Salto.SOM.Web.Models.ServicesAnalysis;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WorkOrder
{
    public class WorkOrderOperationsViewModel
    {
        public int Id { get; set; }
        public string ResponseTime { get; set; }
        public string ResolutionTime { get; set; }
        public int Visits { get; set; }
        public int Interventions { get; set; }
        public string SLAResponseDate { get; set; }
        public string SLAResolutionDate { get; set; }
        public string SlaResponsePenaltyDate { get; set; }
        public string SlaResolutionPenaltyDate { get; set; }
        public string MeetResponsePenaltySla { get; set; }
        public string MeetResolutionPenaltySla { get; set; }
        public string MeetResponseSLA { get; set; }
        public string MeetResolutionSLA { get; set; }
        public IList<ServiceAnalysisViewModel> Services { get; set; }

        public string IsEmptyTimeValue(string Value)
        {
            if(Value == "")
            {
                Value = "-";
            }
            return Value;
        }
    }
}