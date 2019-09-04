using Group.Salto.Common.Enums;

namespace Group.Salto.SOM.Web.Models.Result
{
    public class FeedbackViewModel
    {
        public FeedbackTypeEnum FeedbackType { get; set; }
        public string MessageKey { get; set; }
        public string TitleKey { get; set; }
        public bool IsFixed { get; set; }
      
    }
}
