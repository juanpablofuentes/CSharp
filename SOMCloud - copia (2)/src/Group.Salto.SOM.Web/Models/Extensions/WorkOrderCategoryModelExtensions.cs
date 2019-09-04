using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategories;
using Group.Salto.SOM.Web.Models.WorkOrderCategory;
using System.Collections.Generic;
using System.Globalization;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderCategoryModelExtensions
    {
        public static WorkOrderCategoryBaseViewModel ToViewModel(this WorkOrderCategoriesListDto source)
        {
            WorkOrderCategoryBaseViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderCategoryBaseViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this WorkOrderCategoriesListDto source, WorkOrderCategoryBaseViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Info = source.Info;
                target.EstimatedDuration = ((decimal)source.EstimatedDuration).DecimalToString(CultureInfo.InvariantCulture.TwoLetterISOLanguageName);
                target.Id = source.Id;
            }
        }

        public static IList<WorkOrderCategoryBaseViewModel> ToListViewModel(this IList<WorkOrderCategoriesListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}