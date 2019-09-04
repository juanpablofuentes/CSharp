namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Sla
{
    public class SlaAnalysisValuesDto
    {
        public bool? MeetSlaResponse { get; set; }
        public bool? MeetSlaResolution { get; set; }
        public bool? MeetSlaResponsePenalty { get; set; }
        public bool? MeetSlaResolutionPenalty { get; set; }
        public int? ResolTime { get; set; }
    }
}
