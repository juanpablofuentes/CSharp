using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.CollectionsExtraField
{
    public interface ICollectionsExtraFieldService
    {
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<IList<CollectionsExtraFieldDto>> GetAllFiltered(CollectionsExtraFieldFilterDto filter);
        ResultDto<CollectionsExtraFieldDetailDto> GetByIdWithExtraFields(int id);
        ResultDto<CollectionsExtraFieldDetailDto> Create(CollectionsExtraFieldDetailDto model);
        ResultDto<CollectionsExtraFieldDetailDto> Update(CollectionsExtraFieldDetailDto model);
        ResultDto<ErrorDto> CanDelete(int id);
        ResultDto<bool> Delete(int id);

    }
}