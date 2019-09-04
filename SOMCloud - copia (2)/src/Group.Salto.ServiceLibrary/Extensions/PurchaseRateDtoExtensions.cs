using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PurchaseRateDtoExtensions
    {
        public static PurchaseRateDto ToDto(this PurchaseRate source)
        {
            PurchaseRateDto result = null;
            if (source != null)
            {
                result = new PurchaseRateDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    RefenrenceErp = source.ErpReference,
                };
            }
            return result;
        }

        public static IList<PurchaseRateDto> ToDto(this IList<PurchaseRate> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static PurchaseRate Update(this PurchaseRate target, PurchaseRateDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.ErpReference = source.RefenrenceErp;
            }

            return target;
        }
    }
}