using System;
using Group.Salto.ServiceLibrary.Common.Dtos.PredefinedDay;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Journey
{
    public class JourneyStateDto
    {
        public int Id { get; set; }
        public int PredefinedDayStatesId { get; set; }
        public DateTime Date { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int? PredefinedReasonsId { get; set; }
        public string Observations { get; set; }
        public PredefinedDayEnum JourneyStateType { get; set; }
    }
}
