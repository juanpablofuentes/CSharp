using Group.Salto.Common;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Journeys : BaseEntity
    {
        public int PeopleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCompanyVehicle { get; set; }
        public int? CompanyVehicleId { get; set; }
        public bool Finished { get; set; }
        public double StartKm { get; set; }
        public double? EndKm { get; set; }
        public string Observations { get; set; }

        public People People { get; set; }
        public ICollection<JourneysStates> JourneysStates { get; set; }
    }
}
