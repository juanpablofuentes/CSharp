namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Analysis
{
    public class AnalysisFormValuesDto
    {
        public int? TravelTime { get; set; }
        public int? WaitTime { get; set; }
        public double? Kilometers { get; set; }
        public string Description { get; set; }
        public int Worked { get; set; }
        public int? OnSite { get; set; }
        public decimal? PersonCost { get; set; }
        public decimal? WorkedCost { get; set; }
        public decimal? TravelCost { get; set; }
        public decimal? KmCost { get; set; }
        public int SubCode { get; set; }
        public string SubName { get; set; }
        public double SubContractCost { get; set; }
    }
}
