using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Clients;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Clients
{
    public interface IClientService
    {
        ResultDto<ClientDetailDto> GetById(int id);
        ResultDto<IList<ClientListDto>> GetAllFiltered(ClientFilterDto filter);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<ClientDetailDto> Create(ClientDetailDto clientDetailDto);
        ResultDto<ClientDetailDto> Update(ClientDetailDto clientDetailDto);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<List<MultiSelectItemDto>> GetClientMultiSelect(List<int> selectItems);
        string GetNamesComaSeparated(List<int> ids);
    }
}