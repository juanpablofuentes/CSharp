using System;

namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service
{
    public class ValidateServiceFieldsValuesDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartClosingDate { get; set; }
        public DateTime? EndClosingDate { get; set; }
        public int? WaitTime { get; set; }
        public double? Kilometers { get; set; }
        public int? TravelTime { get; set; }
        public DateTime? FinalInitDate { get; set; }
        public DateTime? FinalEndDate { get; set; }
        public int? FinalWaitTime { get; set; }
        public bool? FinalIsIntervention { get; set; }
    }
}
