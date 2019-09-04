using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using Group.Salto.SOM.Web.Models.WorkOrderCategoriesCollection;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderCategoriesCollectionFilterViewModelExtensions
    {
        public static WorkOrderCategoriesCollectionFilterDto ToDto(this WorkOrderCategoriesCollectionFilterViewModel source)
        {
            WorkOrderCategoriesCollectionFilterDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionFilterDto()
                {
                    Name = source.Name,
                    Info = source.Info,
                    OrderBy = source.OrderBy,
                    Asc = source.Asc,
                };
            }
            return result;
        }

        public static WorkOrderCategoriesCollectionFilterViewModel ToViewModel(this WorkOrderCategoriesCollectionFilterDto source)
        {
            WorkOrderCategoriesCollectionFilterViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionFilterViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this WorkOrderCategoriesCollectionFilterDto source, WorkOrderCategoriesCollectionFilterViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Info = source.Info;
            }
        }

        public static IList<WorkOrderCategoriesCollectionFilterViewModel> ToListViewModel(this IList<WorkOrderCategoriesCollectionFilterDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}