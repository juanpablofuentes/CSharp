using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.SOM.Web.Models.Customer;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CountryViewModelExtensions
    {
        public static CountryViewModel ToViewModel(this CountryDto source)
        {
            CountryViewModel result = null;
            if (source != null)
            {
                result = new CountryViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                };
            }

            return result;
        }

        public static IList<CountryViewModel> ToViewModel(this IList<CountryDto> source)
        {
            return source?.MapList(c => c.ToViewModel());
        }

        public static IList<KeyValuePair<int, string>> ToKeyValuePair(this IList<CountryDto> cities)
        {
            return cities?.Select(c => new KeyValuePair<int, string>(c.Id, c.Name))?.ToList();
        }
    }
}