using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Country;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RegionDtoExtensions
    {
        public static RegionDto ToDto(this Regions source)
        {
            RegionDto result = null;
            if (source != null)
            {
                result = new RegionDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    States = source.States?.ToList()?.ToDto(),
                };
            }
            return result;
        }

        public static IList<RegionDto> ToDto(this IList<Regions> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
