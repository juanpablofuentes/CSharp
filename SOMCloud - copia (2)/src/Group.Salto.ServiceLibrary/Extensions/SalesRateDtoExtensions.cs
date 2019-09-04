using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SalesRate;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SalesRateDtoExtensions
    {
        public static SalesRateBaseDto ToDto(this SalesRate source)
        {
            SalesRateBaseDto result = null;
            if (source != null)
            {
                result = new SalesRateBaseDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    ReferenceERP = source.ErpReference,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<SalesRateBaseDto> ToDto(this IList<SalesRate> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static SalesRate ToEntity(this SalesRateBaseDto source)
        {
            SalesRate result = null;
            if (source != null)
            {
                result = new SalesRate()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ErpReference = source.ReferenceERP                    
                };
            }
            return result;
        }

        public static SalesRate Update(this SalesRate target, SalesRateBaseDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.ErpReference = source.ReferenceERP;              
            }
            return target;
        }
    }
}
