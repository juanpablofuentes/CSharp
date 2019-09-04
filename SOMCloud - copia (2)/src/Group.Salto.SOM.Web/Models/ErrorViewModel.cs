using System;

namespace Group.Salto.SOM.Web.Models
{
    public class ErrorViewModel : IBaseViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Message { get; set; }
        public int ErrorCode { get; set; }
    }
}