using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Service;

namespace Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstanceQuery
{
    public interface IErpSystemInstanceQueryService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        decimal? GetErpPriceFromItem(int billLineItemId, string erpNameKey);
        IEnumerable<FieldMaterialFormGetDto> GetMaterialFormItemsFromPeople(int peopleId);
    }
}