using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.ExtraFields
{
    public class ExtraFieldsListViewModel
    {
        public MultiItemViewModel<ExtraFieldsDetailViewModel, int> ExtraFieldsList { get; set; }
        public ExtraFieldsFilterViewModel Filters { get; set; }
    }
}