using System.Collections.Generic;
using Group.Salto.Common.Enums;

namespace Group.Salto.SOM.Web.Models.Result
{
    public class FeedbacksViewModel
    {
        public IList<FeedbackViewModel> Feedbacks { get; set; }
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public void AddFeedback(string titleKey, string messagekey, FeedbackTypeEnum type)
        {
            if (Feedbacks == null)
            {
                Feedbacks = new List<FeedbackViewModel>();
            }
            Feedbacks.Add(new FeedbackViewModel()
            {
                TitleKey = titleKey,
                MessageKey = messagekey,
                FeedbackType = type,
            });
        }

        public void AddFeedback(FeedbackViewModel feedback)
        {
            if (Feedbacks == null)
            {
                Feedbacks = new List<FeedbackViewModel>();
            }
            Feedbacks.Add(feedback);
        }
    }
}
