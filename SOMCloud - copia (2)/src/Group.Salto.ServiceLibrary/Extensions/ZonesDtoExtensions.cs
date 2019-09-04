using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Zones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ZonesDtoExtensions
    {
        public static ZonesDto ToDto(this Zones source)
        {
            ZonesDto result = null;
            if (source != null)
            {
                result = new ZonesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    ZoneProject = source.ZoneProject?.ToList().ToListDto()
                };
            }
            return result;
        }

        public static IList<ZonesDto> ToListDto(this IList<Zones> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static Zones Update(this Zones target, ZonesDto source)
        {
            if (target != null && source != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;             
            }
            return target;
        }

        public static Zones ToEntity(this ZonesDto source)
        {
            Zones result = null;
            if (source != null)
            {
                result = new Zones()
                {
                    Id = source.Id,
                    Name = source.Name,
                };
            }
            return result;
        }
    }
}