using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.SOM.Web.Models.People;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.Common.Helpers;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PeopleGeoLocalitzationEditViewModelExtensions
    {
        public static GeoLocalitzationEditViewModel ToGeoLocalitzationViewModel(this PeopleDto source, string apiKey)
        {
            GeoLocalitzationEditViewModel people = null;
            if (source != null)
            {
                people = new GeoLocalitzationEditViewModel()
                {
                    Apikey = apiKey,
                    Latitude = ((decimal?)source.Latitude)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    Longitude = ((decimal?)source.Longitude)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName),
                    WorkRadiusKm = ((decimal?)source.WorkRadiusKm)?.DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName)
                };
            }

            return people;
        }
    }
}