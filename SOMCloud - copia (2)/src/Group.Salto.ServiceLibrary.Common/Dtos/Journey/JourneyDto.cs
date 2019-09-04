using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Journey
{
    public class JourneyDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? CompanyVehicleId { get; set; }
        public bool Finished { get; set; }
        public double StartKm { get; set; }
        public double? EndKm { get; set; }
        public string Observations { get; set; }
        public JourneyVehicleTypeEnum VehicleType { get; set; }
        public IEnumerable<JourneyStateDto> JourneyStates { get; set; }
    }
}
