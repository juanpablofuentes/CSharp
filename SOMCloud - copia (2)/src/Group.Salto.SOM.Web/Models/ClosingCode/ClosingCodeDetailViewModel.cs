using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.ClosingCode
{
    public class ClosingCodeDetailViewModel : ClosingCodeViewModel
    {
        public IList<ClosingCodeDetailViewModel> Childs { get; set; }
    }
}