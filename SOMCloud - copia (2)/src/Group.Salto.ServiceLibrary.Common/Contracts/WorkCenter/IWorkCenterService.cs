using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter
{
    public interface IWorkCenterService
    {
        ResultDto<IList<WorkCenterListDto>> GetAllFiltered(WorkCenterFilterDto filter);
        IList<BaseNameIdDto<int>> GetAllKeyValues(int companyId);
        ResultDto<WorkCenterDetailDto> GetByIdWithPeopleCompaniesIncludes(int id);
        ResultDto<WorkCenterDetailDto> Update(WorkCenterDetailDto workCenterDetailDto);
        ResultDto<WorkCenterDetailDto> Create(WorkCenterDetailDto workCenterDetailDto);
        ResultDto<bool> Delete(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
    }
}