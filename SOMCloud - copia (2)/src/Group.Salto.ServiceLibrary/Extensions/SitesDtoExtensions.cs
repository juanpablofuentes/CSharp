using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Sites;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SitesDtoExtensions
    {
        public static SitesListDto ToListDto(this Locations source)
        {
            SitesListDto result = null;
            if (source != null)
            {
                result = new SitesListDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Code = source.Code,
                    City = source.City,
                    Observations = source.Observations,
            };

            }
            return result;
        }

        public static IList<SitesListDto> ToListDto(this IList<Locations> source)
        {
            return source?.MapList(x => x.ToListDto());
        }
    }
}