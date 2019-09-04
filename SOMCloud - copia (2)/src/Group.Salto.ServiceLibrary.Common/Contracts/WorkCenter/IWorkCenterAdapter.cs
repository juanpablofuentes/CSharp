using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkCenter;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkCenter
{
    public interface IWorkCenterAdapter
    {
        ResultDto<IList<WorkCenterListDto>> GetList(WorkCenterFilterDto filter);
        ResultDto<WorkCenterDetailDto> GetById(int id);
    }
}