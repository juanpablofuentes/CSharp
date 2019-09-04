using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BillDetailDtoExtensions
    {
        public static BillDetailDto ToDtoBillDetail(this BillLine source)
        {
            BillDetailDto result = null;
            if (source != null)
            {
                result = new BillDetailDto()
                {
                    Id = source.ItemId,
                    Item = source.Item?.Name,
                    Units = source.Units,
                    SerialNumber = source.SerialNumber ,
                };
            }
            return result;
        }

        public static IList<BillDetailDto> ToListDtoBillDetail(this IList<BillLine> source)
        {
            return source?.MapList(x => x.ToDtoBillDetail());
        }
    }
}