using Group.Salto.Common.Enums;

namespace Group.Salto.SOM.Web.Models.Toast
{
    public static class ToastrViewModelExtensions
    {
        public static ToastMessageViewModel AddToastMessage(this ToastrViewModel source, 
                                                                string title, string message,
                                                                FeedbackTypeEnum toastType)
        {
            source = source ?? new ToastrViewModel();
            var toast = new ToastMessageViewModel()
            {
                Title = title,
                Message = message,
                ToastType = toastType
            };
            source.ToastMessages.Add(toast);
            return toast;
        }
    }
}
