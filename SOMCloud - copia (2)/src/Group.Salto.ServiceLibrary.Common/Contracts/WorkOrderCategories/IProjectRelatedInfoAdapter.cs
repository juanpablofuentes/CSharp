using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories
{
    public interface IProjectRelatedInfoAdapter
    {
        ResultDto<ProjectRelatedInfoDto> GetProjectRelatedInfo(int id);
    }
}