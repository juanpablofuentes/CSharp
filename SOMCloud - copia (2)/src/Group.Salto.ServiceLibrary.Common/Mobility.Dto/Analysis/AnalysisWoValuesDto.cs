namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Analysis
{
    public class AnalysisWoValuesDto
    {
        public int WorkedTime { get; set; }
        public int? EstimatedTime { get; set; }
        public int OnsiteTime { get; set; }
        public int TravelTime { get; set; }
        public int WaitTime { get; set; }
        public decimal Kilometers { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal SubcontractCost { get; set; }
        public decimal TravelCost { get; set; }
        public decimal MaterialCost { get; set; }
        public decimal? WaitCost { get; set; }
        public decimal? KmCost { get; set; }
        public decimal? ExpenseCost { get; set; }
        public decimal Tolls { get; set; }
        public decimal Parking { get; set; }
        public decimal Mexpenses { get; set; }
        public decimal Oexpenses { get; set; }
        public decimal? Margin { get; set; }
        public int? PercentWorked { get; set; }
        public int NumberOfIntervention { get; set; }
        public int NumberOfVisitsToClient { get; set; }
        public decimal? SubcontractorCost { get; set; }
    }
}
