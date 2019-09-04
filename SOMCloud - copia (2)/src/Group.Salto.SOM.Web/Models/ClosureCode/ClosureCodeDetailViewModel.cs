using System.Collections.Generic;
using Group.Salto.SOM.Web.Models.ClosingCode;

namespace Group.Salto.SOM.Web.Models.ClosureCode
{
    public class ClosureCodeDetailViewModel : ClosureCodeViewModel
    {
        public IList<ClosingCodeDetailViewModel> Childs { get; set; }
    }
}