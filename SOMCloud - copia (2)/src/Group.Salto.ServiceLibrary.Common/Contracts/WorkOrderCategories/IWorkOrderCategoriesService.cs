using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Group.Salto.ServiceLibrary.Common.Contracts.WorkOrderCategories
{
    public interface IWorkOrderCategoriesService
    {
        ResultDto<WorkOrderCategoryDetailsDto> GetById(int Id);
        ResultDto<WorkOrderCategoryDetailsDto> Create(WorkOrderCategoryDetailsDto workOrderCategoryDetailDto);
        ResultDto<WorkOrderCategoryDetailsDto> Update(WorkOrderCategoryDetailsDto workOrderCategoryDetailDto);
        ResultDto<bool> Delete(int id);
        ResultDto<ErrorDto> CanDelete(int id);
        IList<BaseNameIdDto<int>> GetAllKeyValues();
        IList<BaseNameIdDto<int>> GetAllKeyValuesByProject(List<int> projectsIds, int userId);
        ResultDto<IList<WorkOrderCategoriesListDto>> GetAllFiltered(WorkOrderCategoriesFilterDto filter);
        ResultDto<List<MultiSelectItemDto>> GetPermissionList(int? workOrderCategoryId);
        Task<ResultDto<List<MultiSelectItemDto>>> GetPermissionRoleList(int? workOrderCategoryId);
        IList<BaseNameIdDto<int>> Filter(QueryRequestDto queryRequest);
        ResultDto<List<bool>> HasSLADates(int id);
        Entities.Tenant.WorkOrderCategories GetByIdWithSLA(int Id);
        IList<BaseNameIdDto<int>> FilterByProject(QueryCascadeDto queryRequest);
        ResultDto<List<MultiSelectItemDto>> GeWorkOrderCategoryMultiSelect(List<int> selectItems, int userId);
        string GetNamesComaSeparated(List<int> ids);
    }
}