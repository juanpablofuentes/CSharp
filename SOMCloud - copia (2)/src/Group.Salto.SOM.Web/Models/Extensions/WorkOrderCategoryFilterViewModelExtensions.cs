using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.SOM.Web.Models.WorkOrderCategory;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderCategoryFilterViewModelExtensions
    {
        public static WorkOrderCategoriesFilterDto ToFilterDto(this WorkOrderCategoryFilterViewModel source)
        {
            WorkOrderCategoriesFilterDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }
    }
}