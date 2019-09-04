using Group.Salto.ServiceLibrary.Common.Dtos.Brand;
using Group.Salto.SOM.Web.Models.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BrandsFilterViewModelExtensions
    {
        public static BrandsFilterDto ToDto(this BrandsFilterViewModel source)
        {
            BrandsFilterDto result = null;
            if (source != null)
            {
                result = new BrandsFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }
    }
}