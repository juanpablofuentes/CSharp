using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.CollectionsExtraField;

namespace Group.Salto.SOM.Web.Models.CollectionsExtraField
{
    public class CollectionsExtraFieldListViewModel
    {
        public MultiItemViewModel<CollectionsExtraFieldViewModel, int> CollectionsExtraFieldList { get; set; }
        public CollectionsExtraFieldFilterViewModel Filters { get; set; }
    }
}