using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.PeopleCollection
{
    public class PeopleCollectionsViewModel
    {
        public MultiItemViewModel<PeopleCollectionViewModel, int> PeopleCollections { get; set; }
        public PeopleCollectionFilterViewModel PeopleCollectionFilter { get; set; }
    }
}