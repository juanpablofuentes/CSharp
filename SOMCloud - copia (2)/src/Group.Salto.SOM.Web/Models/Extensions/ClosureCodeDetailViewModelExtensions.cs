using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;
using Group.Salto.SOM.Web.Models.ClosureCode;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ClosureCodeDetailViewModelExtensions
    {
        public static ResultViewModel<ClosureCodeDetailViewModel> ToResultViewModel(this ResultDto<ClosureCodeDto> source)
        {
            ResultViewModel<ClosureCodeDetailViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<ClosureCodeDetailViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel(),
                };
            }

            return result;
        }

        public static ClosureCodeDetailViewModel ToViewModel(this ClosureCodeDto source)
        {
            ClosureCodeDetailViewModel result = null;
            if (source != null)
            {
                result = new ClosureCodeDetailViewModel();
                source.ToViewModel(result);
                result.Childs = source.ClosingCodes.ToViewModel();
            }

            return result;
        }

        public static ClosureCodeDto ToDto(this ClosureCodeDetailViewModel source)
        {
            ClosureCodeDto result = null;
            if (source != null)
            {
                result = new ClosureCodeDto();
                source.ToDto(result);
                result.ClosingCodes = source.Childs.ToDto();
            }

            return result;
        }
    }
}