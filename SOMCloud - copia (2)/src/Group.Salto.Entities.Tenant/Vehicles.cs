using Group.Salto.Common.Entities;
using System;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Vehicles : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? Height { get; set; }
        public double? Direction { get; set; }
        public double? Speed { get; set; }
        public string Observations { get; set; }
        public int? PeopleDriverId { get; set; }
        public People PeopleDriver { get; set; }
        public ICollection<Tools> Tools { get; set; }
    }
}
