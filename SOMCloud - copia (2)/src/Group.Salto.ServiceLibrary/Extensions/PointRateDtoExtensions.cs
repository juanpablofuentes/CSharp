using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PointRate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PointRateDtoExtensions
    {
        public static PointRateDto ToDto(this PointsRate source)
        {
            PointRateDto result = null;
            if (source != null)
            {
                result = new PointRateDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ErpReference = source.ErpReference
                };
            }
            return result;
        }

        public static IList<PointRateDto> ToDto(this IList<PointsRate> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static PointsRate Update(this PointsRate target, PointRateDto source)
        {
            if (target != null && source != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.ErpReference = source.ErpReference;
            }

            return target;
        }

        public static PointsRate ToEntity(this PointRateDto source)
        {
            PointsRate result = null;
            if (source != null)
            {
                result = new PointsRate()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ErpReference = source.ErpReference
                };
            }

            return result;
        }
    }
}
