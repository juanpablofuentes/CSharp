using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.AdvancedSearch;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Projects;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.Project
{
    public interface IProjectsService
    {
        ResultDto<ProjectsDetailsDto> GetById(int Id);
        ResultDto<ProjectsDetailsDto> Create(ProjectsDetailsDto projectsDetailsDto);
        ResultDto<ProjectsDetailsDto> Update(ProjectsDetailsDto projectsDetailsDto);
        ResultDto<bool> Delete(int id);
        ResultDto<IList<ProjectsDto>> GetAllFiltered(ProjectsFilterDto filter);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        ResultDto<List<MultiSelectItemDto>> GetPermissionList(int? projectId);
        ResultDto<ErrorDto> CanDelete(int id);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        IList<BaseNameIdDto<int>> FilterByClient(QueryCascadeDto queryRequest);
        ResultDto<IList<AdvancedSearchDto>> GetAdvancedSearch(QueryTypeParametersDto queryTypeParameters);
        IList<BaseNameIdDto<int>> GetProjectFilteredUserId(int userId);
        ResultDto<List<MultiSelectItemDto>> GetProjectMultiSelect(List<int> selectItems, int userId);
        string GetNamesComaSeparated(List<int> ids);
    }
}