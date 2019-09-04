using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionTypeWorkOrders;
using Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CollectionTypeWorkOrdersDetailViewModelExtensions
    {
        public static ResultViewModel<CollectionTypeWorkOrdersDetailViewModel> ToResultViewModel(this ResultDto<CollectionTypeWorkOrdersDetailDto> source)
        {
            ResultViewModel<CollectionTypeWorkOrdersDetailViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<CollectionTypeWorkOrdersDetailViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }

        public static CollectionTypeWorkOrdersDetailViewModel ToViewModel(this CollectionTypeWorkOrdersDetailDto source)
        {
            CollectionTypeWorkOrdersDetailViewModel result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersDetailViewModel();
                source.ToViewModel(result);
                result.Childs = source.WorkOrderTypes.ToViewModel();
            }

            return result;
        }

        public static CollectionTypeWorkOrdersDetailDto ToDto(this CollectionTypeWorkOrdersDetailViewModel source)
        {
            CollectionTypeWorkOrdersDetailDto result = null;
            if (source != null)
            {
                result = new CollectionTypeWorkOrdersDetailDto();
                source.ToDto(result);
                result.WorkOrderTypes = source.Childs.ToDto();
            }

            return result;
        }
    }
}