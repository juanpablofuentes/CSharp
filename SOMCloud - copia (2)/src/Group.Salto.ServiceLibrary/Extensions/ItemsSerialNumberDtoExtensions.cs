using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ItemsSerialNumberDtoExtensions
    {
        public static ItemsSerialNumberDto ToDto(this ItemsSerialNumber source)
        {
            ItemsSerialNumberDto result = null;
            if (source != null)
            {
                result = new ItemsSerialNumberDto()
                {
                    Id = source.ItemId,
                    SerialNumber = source.SerialNumber,
                    Observations = source.Observations,
                    SerialNumberStatusId = source.ItemsSerialNumberStatusesId
                };                             
            }
            return result;
        }

        public static IList<ItemsSerialNumberDto> ToListDto(this IList<ItemsSerialNumber> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}