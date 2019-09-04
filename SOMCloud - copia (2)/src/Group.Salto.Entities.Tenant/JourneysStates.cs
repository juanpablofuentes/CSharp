using Group.Salto.Common;
using System;

namespace Group.Salto.Entities.Tenant
{
    public class JourneysStates : BaseEntity
    {
        public int PredefinedDayStatesId { get; set; }
        public int JourneyId { get; set; }
        public DateTime Data { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int? PredefinedReasonsId { get; set; }
        public string Observations { get; set; }

        public Journeys Journey { get; set; }
    }
}
