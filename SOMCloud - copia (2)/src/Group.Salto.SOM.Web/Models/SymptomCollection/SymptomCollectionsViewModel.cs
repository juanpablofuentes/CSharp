using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.SymptomCollection
{
    public class SymptomCollectionsViewModel
    {
        public MultiItemViewModel<SymptomCollectionViewModel, int> SymptomCollections { get; set; }
        public SymptomCollectionFilterViewModel SymptomCollectionFilter { get; set; }
    }
}