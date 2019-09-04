using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.CollectionsExtraField;
using Group.Salto.SOM.Web.Models.CollectionsExtraField;
using Group.Salto.SOM.Web.Models.Result;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class CollectionsExtraFieldDetailViewModelExtensions
    {
        public static CollectionsExtraFieldDetailViewModel ToViewModel(this CollectionsExtraFieldDetailDto source)
        {
            CollectionsExtraFieldDetailViewModel result = null;
            if (source != null)
            {
                result = new CollectionsExtraFieldDetailViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ExtraFieldsSelected = source.CollectionsExtraFieldExtraField.ToExtraFieldsDetail(source.ExtraFieldsTypes),
                    ExtraFieldsTypes = source.ExtraFieldsTypes.ToExtraFieldsTypes()
                }; 
            }
            return result;
        }

        public static ResultViewModel<CollectionsExtraFieldDetailViewModel> ToViewModel(this ResultDto<CollectionsExtraFieldDetailDto> source)
        {
            var response = source != null ? new ResultViewModel<CollectionsExtraFieldDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static CollectionsExtraFieldDetailDto ToDto(this CollectionsExtraFieldDetailViewModel genericVM)
        {
            CollectionsExtraFieldDetailDto result = null;
            if (genericVM != null)
            {
                result = new CollectionsExtraFieldDetailDto()
                {
                    Id = genericVM.Id,
                    Name = genericVM.Name,
                    Description = genericVM.Description,
                    CollectionsExtraFieldExtraField = genericVM.ExtraFieldsSelected.ToCollectionsExtraFieldExtraFieldDto()
                };
            }
            return result;
        }
    }
}