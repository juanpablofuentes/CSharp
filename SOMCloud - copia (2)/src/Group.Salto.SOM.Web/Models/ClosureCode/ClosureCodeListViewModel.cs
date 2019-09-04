using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.ClosureCode
{
    public class ClosureCodeListViewModel
    {
        public MultiItemViewModel<ClosureCodeViewModel, int> ClosureCodes { get; set; }
        public ClosureCodeFilterViewModel Filters { get; set; }
    }
}