using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.SitesFinalClients
{
    public interface ISitesFinalClientsService
    {
        ResultDto<SitesFinalClientsDto> Create(SitesFinalClientsDto model);
        ResultDto<SitesFinalClientsDto> GetBySiteId(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValuesByFinalClientsIds(List<int> IdsToMatch);
    }
}