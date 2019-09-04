using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ItemsListDtoExtensions
    {
        public static ItemsListDto ToListDto(this Items source)
        {
            ItemsListDto result = null;
            if (source != null)
            {
                result = new ItemsListDto
                {
                    Id = source.Id,
                    Description = source.Description,
                    ERPReference = source.ErpReference,
                    InternalReference = source.InternalReference,
                    IsBlocked = source.IsBlocked,
                    ItemsTypeId = source.Type,
                    Name = source.Name,
                    SyncErp = source.SyncErp
                };
            }
            return result;
        }

        public static IList<ItemsListDto> ToListDto(this IQueryable<Items> source, IList<ItemTypes> types)
        {
            var result = source?.MapList(x => x.ToListDto());
            return result.ToListWithTypeDto(types);
        }

        private static IList<ItemsListDto> ToListWithTypeDto(this IList<ItemsListDto> source, IList<ItemTypes> types)
        {
            foreach (var item in source)
            {
                item.ItemsTypeName = types.FirstOrDefault(s => s.Id == item.ItemsTypeId)?.Name;
            }
            return source;
        }
    }
}