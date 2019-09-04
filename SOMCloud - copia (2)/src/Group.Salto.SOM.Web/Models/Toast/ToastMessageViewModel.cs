
using System;
using Group.Salto.Common.Enums;

namespace Group.Salto.SOM.Web.Models.Toast
{
    [Serializable]
    public class ToastMessageViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public FeedbackTypeEnum ToastType { get; set; }
        public bool IsSticky { get; set; }
    }
}
