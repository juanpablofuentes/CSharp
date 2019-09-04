using Group.Salto.Common;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Locations : BaseEntity
    {
        public string Name { get; set; }
        public string StreetType { get; set; }
        public string Street { get; set; }
        public string Code { get; set; }
        public int? Number { get; set; }
        public string Escala { get; set; }
        public string GateNumber { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public int? PostalCode { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string Zone { get; set; }
        public string Subzone { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Observations { get; set; }
        public int? PeopleResponsibleLocationId { get; set; }
        public int? HashCity { get; set; }
        public int? HashProvincie { get; set; }
        public int? HashZone { get; set; }
        public int? HashSubzone { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? MunicipalityId { get; set; }
        public int? RegionId { get; set; }
        public int? PostalCodeId { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }

        public People PeopleResponsibleLocation { get; set; }
        public ICollection<ContactsLocationsFinalClients> ContactsLocationsFinalClients { get; set; }
        public ICollection<ExternalServicesConfiguration> ExternalServicesConfiguration { get; set; }
        public ICollection<LocationCalendar> LocationCalendar { get; set; }
        public ICollection<LocationsFinalClients> LocationsFinalClients { get; set; }
        public ICollection<PreconditionsLiteralValues> PreconditionsLiteralValues { get; set; }
        public ICollection<SiteUser> SiteUser { get; set; }
        public ICollection<Assets> AssetsLocation { get; set; }
        public ICollection<Assets> AssetsLocationClient { get; set; }
        public ICollection<WorkOrders> WorkOrders { get; set; }
        public ICollection<WorkOrdersDeritative> WorkOrdersDeritative { get; set; }
    }
}
