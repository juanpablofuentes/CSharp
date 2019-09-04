using Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate;
using Group.Salto.SOM.Web.Models.PurchaseRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PurchaseRateFilterViewModelExtensions
    {
        public static PurchaseRateFilterDto ToDto(this PurchaseRateFilterViewModel source)
        {
            PurchaseRateFilterDto result = null;
            if (source != null)
            {
                result = new PurchaseRateFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}