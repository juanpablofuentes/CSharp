using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Billing;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BillDtoExtensions
    {
        public static BillDto ToDto(this Bill source)
        {
            BillDto result = null;
            if (source != null)
            {
                result = new BillDto()
                {
                    Id = source.Id,
                    DeliveryNotesId = source.Id,
                    ExternalSystemNumber = source.ExternalSystemNumber,
                    Date = source.Date.ToShortDateString(),
                    Task = source.Task,
                    DeliveryNotesLines = source?.BillLine?.Count ?? 0
                };
            }
            return result;
        }

        public static IList<BillDto> ToListDto(this IList<Bill> source)
        {
            return source?.MapList(x => x.ToDto());
        }
    }
}