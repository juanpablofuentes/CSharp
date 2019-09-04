using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategoriesCollection
{
    public interface IWorkOrderCategoriesCollectionService
    {
        ResultDto<IList<WorkOrderCategoriesCollectionDetailDto>> GetAllFiltered(WorkOrderCategoriesCollectionFilterDto filter);
        ResultDto<WorkOrderCategoriesCollectionDetailDto> GetById(int id);
        ResultDto<WorkOrderCategoriesCollectionDetailDto> Create(WorkOrderCategoriesCollectionDetailDto model);
        ResultDto<WorkOrderCategoriesCollectionDetailDto> Update(WorkOrderCategoriesCollectionDetailDto model);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<bool> Delete(int id);
        ResultDto<bool> CanDelete(int id);
    }
}