using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Country;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class MunicipalitiesDtoExtensions
    {
        public static MunicipalityDto ToDto(this Municipalities source)
        {
            MunicipalityDto result = null;
            if (source != null)
            {
                result = new MunicipalityDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                };
            }

            return result;
        }

        public static IList<MunicipalityDto> ToDto(this IList<Municipalities> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
