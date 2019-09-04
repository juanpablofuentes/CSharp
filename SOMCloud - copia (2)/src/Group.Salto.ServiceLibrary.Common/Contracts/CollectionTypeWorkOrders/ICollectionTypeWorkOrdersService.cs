using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderTypes;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.CollectionTypeWorkOrders
{
    public interface ICollectionTypeWorkOrdersService
    {
        ResultDto<IList<CollectionTypeWorkOrdersDto>> GetAllFiltered(CollectionTypeWorkOrdersFilterDto filter);
        ResultDto<CollectionTypeWorkOrdersDetailDto> GetById(int id);
        ResultDto<CollectionTypeWorkOrdersDetailDto> Create(CollectionTypeWorkOrdersDetailDto model);
        ResultDto<CollectionTypeWorkOrdersDetailDto> Update(CollectionTypeWorkOrdersDetailDto model);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> GetAllWorkOrderTypesKeyValues();
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<bool> CanDelete(int id);
        ResultDto<bool> CanDeleteTreeLevel(int id);
        List<string> GetWorkOrderTypes(int initvalue, IList<WorkOrderTypeFatherDto> workOrderTypes);
        IList<BaseNameIdDto<int>> GetAllWOTypesKeyValues(List<int?> IdsToMatch);
        ResultDto<List<MultiSelectItemDto>> GetWorkOrderTypesMultiSelect(List<int> selectItems);
        string GetNamesComaSeparated(List<int> ids);
    }
}