namespace Group.Salto.ServiceLibrary.Common.Dtos.ServiceAnalysis
{
    public class ServiceAnalysisDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public string Technician { get; set; }
        public string Subcontract { get; set; }
        public int? WorkedTime { get; set; }
        public int? TimeOnSite { get; set; }
        public int? WaitTime { get; set; }
        public int? TravelTime { get; set; }
        public decimal? Kilometers { get; set; }
    }
}