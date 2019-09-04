using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.BillLines;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BillLinesDtoExtensions
    {
        public static BillLinesDto ToDto(this BillLine source)
        {
            BillLinesDto result = null;
            if (source != null)
            {
                result = new BillLinesDto()
                {
                    Id = source.Id,
                    Name = source.Item.Name,
                    Units = source.Units,
                };
            }
            return result;
        }

        public static IList<BillLinesDto> ToListDto(this IList<BillLine> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}