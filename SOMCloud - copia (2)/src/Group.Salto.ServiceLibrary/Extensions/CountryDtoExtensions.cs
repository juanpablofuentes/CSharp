using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CountryDtoExtensions
    {
        public static CountryDto ToDto(this Countries source)
        {
            CountryDto result = null;
            if (source != null)
            {
                result = new CountryDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Regions = source?.Regions?.ToList()?.ToDto(),
                };
            }
            return result;
        }

        public static IList<CountryDto> ToDto(this IList<Countries> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
