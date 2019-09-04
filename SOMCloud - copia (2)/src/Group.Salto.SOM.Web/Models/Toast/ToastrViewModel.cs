using System;
using System.Collections.Generic;
using Group.Salto.Common.Enums;
using Group.Salto.SOM.Web.Models;

namespace Group.Salto.SOM.Web.Models.Toast
{
    [Serializable]
    public class ToastrViewModel
    {
        public bool ShowNewestOnTop { get; set; }
        public bool ShowCloseButton { get; set; }
        public List<ToastMessageViewModel> ToastMessages { get; set; }

        public ToastrViewModel()
        {
            ToastMessages = new List<ToastMessageViewModel>();
            ShowNewestOnTop = false;
            ShowCloseButton = false;
        }
    }
}
