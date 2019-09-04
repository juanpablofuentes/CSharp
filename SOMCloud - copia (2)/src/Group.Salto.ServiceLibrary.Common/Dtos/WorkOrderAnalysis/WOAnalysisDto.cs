namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderAnalysis
{
    public class WOAnalysisDto
    {
        public int Id { get; set; }
        public string ResponseTime { get; set; }
        public string ResolutionTime { get; set; }
        public int? Visits { get; set; }
        public int? Interventions { get; set; }
        public string SLAResponseDate { get; set; }
        public string SLAResolutionDate { get; set; }
        public string SlaResponsePenaltyDate { get; set; }
        public string SlaResolutionPenaltyDate { get; set; }
        public bool? MeetResponsePenaltySla { get; set; }
        public bool? MeetResolutionPenaltySla { get; set; }
        public bool? MeetResponseSLA { get; set; }
        public bool? MeetResolutionSLA { get; set; }
    }
}