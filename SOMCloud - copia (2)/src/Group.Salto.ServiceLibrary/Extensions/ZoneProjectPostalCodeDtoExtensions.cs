using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ZoneProjectPostalCode;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ZoneProjectPostalCodeDtoExtensions
    {
        public static ZoneProjectPostalCodeDto ToDto(this ZoneProjectPostalCode source)
        {
            ZoneProjectPostalCodeDto result = null;
            if (source != null)
            {
                result = new ZoneProjectPostalCodeDto();
                result.PostalCode = source.PostalCode;
                result.ZoneProjectId = source.ZoneProjectId;
                result.PostalCodeId = source.Id;                              
            }
            return result;
        }

        public static IList<ZoneProjectPostalCodeDto> ToDto(this IList<ZoneProjectPostalCode> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static ZoneProjectPostalCode ToEntity(this ZoneProjectPostalCodeDto source)
        {
            ZoneProjectPostalCode result = null;
            if (source != null)
            {
                result = new ZoneProjectPostalCode()
                {
                    ZoneProjectId = source.ZoneProjectId,
                    ZoneProject = source.ZoneProject.ToEntity(),
                    PostalCode = source.PostalCode, 
                    UpdateDate = DateTime.UtcNow
            };
            }
            return result;
        }
        public static IList<ZoneProjectPostalCode> ToEntity(this IList<ZoneProjectPostalCodeDto> source)
        {
            return source.MapList(x => x.ToEntity());
        }

        public static void UpdatePostalCode(this ZoneProjectPostalCode target, ZoneProjectPostalCode source)
        {
            if (source != null && target != null)
            {
                target.PostalCode = source.PostalCode;
            }
        }
    }
}