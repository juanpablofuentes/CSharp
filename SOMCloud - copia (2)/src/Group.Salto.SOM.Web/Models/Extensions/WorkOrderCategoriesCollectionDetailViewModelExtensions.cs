using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderCategoriesCollection;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrderCategoriesCollection;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderCategoriesCollectionDetailViewModelExtensions
    {
        public static WorkOrderCategoriesCollectionDto ToDto(this WorkOrderCategoriesCollectionBaseViewModel source)
        {
            WorkOrderCategoriesCollectionDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Info = source.Info
                };
            }
            return result;
        }

        public static WorkOrderCategoriesCollectionDetailDto ToDetailDto(this WorkOrderCategoriesCollectionBaseViewModel source)
        {
            WorkOrderCategoriesCollectionDetailDto result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionDetailDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Info = source.Info
                };
            }
            return result;
        }

        public static ResultViewModel<WorkOrderCategoriesCollectionBaseViewModel> ToResultViewModel(this ResultDto<WorkOrderCategoriesCollectionDetailDto> source)
        {
            ResultViewModel<WorkOrderCategoriesCollectionBaseViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<WorkOrderCategoriesCollectionBaseViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }

        public static WorkOrderCategoriesCollectionBaseViewModel ToViewModel(this WorkOrderCategoriesCollectionDetailDto source)
        {
            WorkOrderCategoriesCollectionBaseViewModel result = null;
            if (source != null)
            {
                result = new WorkOrderCategoriesCollectionBaseViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this WorkOrderCategoriesCollectionDetailDto source, WorkOrderCategoriesCollectionBaseViewModel target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Info = source.Info;
                target.Id = source.Id;
                target.CategoriesSelected = source.WorkOrderCategories.ToListViewModel();
            }
        }

        public static IList<WorkOrderCategoriesCollectionBaseViewModel> ToListViewModel(this IList<WorkOrderCategoriesCollectionDetailDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}