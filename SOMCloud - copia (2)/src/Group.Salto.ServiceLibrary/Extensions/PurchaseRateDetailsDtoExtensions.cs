using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PurchaseRateDetailsDtoExtensions
    {
        public static PurchaseRateDetailsDto ToDetailDto(this PurchaseRate source)
        {
            PurchaseRateDetailsDto result = null;
            if (source != null)
            {
                result = new PurchaseRateDetailsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    RefenrenceErp = source.ErpReference
                };
            }
            return result;
        }

        public static PurchaseRate ToEntity(this PurchaseRateDetailsDto source)
        {
            PurchaseRate result = null;
            if (source != null)
            {
                result = new PurchaseRate()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ErpReference = source.RefenrenceErp
                };
            }
            return result;
        }
    }
}
