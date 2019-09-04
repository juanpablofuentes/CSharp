using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Items
{
    public interface IItemsService
    {
        ResultDto<IList<ItemsListDto>> GetAllFiltered(ItemsFilterDto filter);
        ResultDto<ItemsDetailsDto> CreateItem(ItemsDetailsDto source);
        ResultDto<ItemsDetailsDto> UpdateItem(ItemsDetailsDto source);
        ResultDto<ItemsDetailsDto> GetById(int id);
        ResultDto<bool> DeleteItem(int id);
        ResultDto<ErrorDto> CanDelete(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
    }
}