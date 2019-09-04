using System;
using Group.Salto.Common.Enums;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.SOM.Web.Models.Result
{
    public static class ResultViewModelExtensions
    {
        public static ResultViewModel<TViewModel> ToViewModel<TViewModel, TDto>(this ResultDto<TDto> source, 
                                                Func<TDto,TViewModel> mappingFunc, 
                                                string title = null, string message = null)
        {
            ResultViewModel<TViewModel> result = null;
            if (source != null)
            {
                result = new ResultViewModel<TViewModel>()
                {
                    Data = mappingFunc(source.Data),
                    Feedbacks = source.Errors.ToViewModel(),
                };
                if (source.Errors == null && !string.IsNullOrEmpty(title) 
                                          && !string.IsNullOrEmpty(message))
                {
                    result.Feedbacks.AddFeedback(title, message, FeedbackTypeEnum.Success);
                }
            }
            return result;
        }
    }
}
