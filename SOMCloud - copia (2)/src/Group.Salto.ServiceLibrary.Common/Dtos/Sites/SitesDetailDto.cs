using Group.Salto.ServiceLibrary.Common.Dtos.Contacts;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Sites
{
    public class SitesDetailDto : SitesDto
    {
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public int FinalClientId {get; set;}
        public string Street { get; set; }
        public string StreetType { get; set; }
        public string GateNumber { get; set; }
        public string Gate { get; set; }
        public string PostalCode { get; set; }
        public int? CountryId { get; set; }
        public int? RegionId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public int? MunicipalityId { get; set; }
        public string Area { get; set; }
        public string Zone { get; set; }
        public string SubZone { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public IList<ContactsDto> ContactsSelected { get; set; }
    }
}