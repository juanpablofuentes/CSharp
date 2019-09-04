using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;

namespace Group.Salto.SOM.Web.Models.Sites
{
    public class GeolocationDetailViewModel
    {
        public IList<KeyValuePair<int, string>> Countries { get; set; }
        public int? CountrySelected { get; set; }
        public int? RegionSelected { get; set; }
        public int? StateSelected { get; set; }
        public int? CitySelected { get; set; }
        public int? MunicipalitySelected { get; set; }
        public string PostalCode { get; set; }
        public string Area { get; set; }
        public string Zone { get; set; }
        public string SubZone { get; set; }
        public string Street { get; set; }
        public string StreetType { get; set; }
        public string Number { get; set; }
        public string Gate { get; set; }
        public string GateNumber { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Apikey { get; set; }
        public string Language { get; set; } = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public string WorkRadiusKm { get; set; }
    }
}
