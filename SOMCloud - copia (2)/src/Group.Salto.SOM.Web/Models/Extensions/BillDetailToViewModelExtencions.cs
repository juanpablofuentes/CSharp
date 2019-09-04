using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using Group.Salto.SOM.Web.Models.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BillDetailToViewModelExtencions
    {
        public static BillDetailDto ToDto(this BillDetailViewModel source)
        {
            BillDetailDto result = null;
            if (source != null)
            {
                result = new BillDetailDto()
                {
                    Id = source.Id,
                    Item = source.Item,
                    Units = source.Units,
                    SerialNumber = source.SerialNumber,
                };
            }
            return result;
        }

        public static IList<BillDetailDto> ToListDto(this IList<BillDetailViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static BillDetailViewModel ToViewModelDetail(this BillDetailDto source)
        {
            BillDetailViewModel result = null;
            if (source != null)
            {
                result = new BillDetailViewModel()
                {
                    Id = source.Id,
                    Item = source.Item,
                    Units = source.Units,
                    SerialNumber = source.SerialNumber,
                };
            }
            return result;
        }

        public static IList<BillDetailViewModel> ToListViewModelDetail(this IList<BillDetailDto> source)
        {
            return source?.MapList(x => x.ToViewModelDetail());
        }
    }
}