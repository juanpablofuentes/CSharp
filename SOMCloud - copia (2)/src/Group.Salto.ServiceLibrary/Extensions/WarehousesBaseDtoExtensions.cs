using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Warehouses;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WarehousesBaseDtoExtensions
    {
        public static WarehousesBaseDto ToBaseDto(this Warehouses source)
        {
            WarehousesBaseDto result = null;
            if (source != null)
            {
                result = new WarehousesBaseDto
                {
                    Id = source.Id,
                    Code = source.Code,
                    ErpReference = source.ErpReference,
                    Name = source.Name
                };
            }
            return result;
        }

        public static IList<WarehousesBaseDto> ToListDto(this IQueryable<Warehouses> source)
        {
            return source?.MapList(x => x.ToBaseDto());            
        }

        public static Warehouses ToEntity(this WarehousesBaseDto source)
        {
            Warehouses result = null;
            if (source != null)
            {
                result = new Warehouses
                {
                    Id = source.Id,
                    Code = source.Code,
                    ErpReference = source.ErpReference,
                    Name = source.Name
                };
            }
            return result;
        }
        
        public static Warehouses Update(this Warehouses target, WarehousesBaseDto source)
        {
            if (target != null && source != null)
            {
                target.Code = source.Code;
                target.ErpReference = source.ErpReference;
                target.Name = source.Name;
            }
            return target;
        }
    }
}