using Group.Salto.Common.Constants;
using Group.Salto.Common.Enums;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.SOM.Web.Models.Result
{
    public static class FeedbackViewModelExtensions
    {
        public static FeedbackViewModel ToViewModel(this ErrorDto source)
        {
            FeedbackViewModel result = null;
            if (source != null)
            {
                result = new FeedbackViewModel()
                {
                    TitleKey = LocalizationsConstants.Error,
                    MessageKey = !string.IsNullOrEmpty(source.ErrorMessageKey) ?
                                    source.ErrorMessageKey
                                    : $"{source.ErrorType.ToString()}{AppConstants.LocalizationPostfix}",
                    FeedbackType = FeedbackTypeEnum.Error,
                };
            }

            return result;
        }

        public static FeedbacksViewModel ToViewModel(this ErrorsDto source)
        {
            FeedbacksViewModel result = null;
            if (source != null)
            {
                result = new FeedbacksViewModel()
                {
                    Feedbacks = source.Errors.MapList(e => e.ToViewModel()),
                };
            }

            return result;
        }
    }
}
