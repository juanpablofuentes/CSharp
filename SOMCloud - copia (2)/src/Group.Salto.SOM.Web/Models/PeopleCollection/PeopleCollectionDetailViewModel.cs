using System.Collections.Generic;
using Group.Salto.SOM.Web.Models.MultiCombo;

namespace Group.Salto.SOM.Web.Models.PeopleCollection
{
    public class PeopleCollectionDetailViewModel : PeopleCollectionViewModel
    {
        public IList<MultiComboViewModel<int, int>> People { get; set; }
        public IList<MultiComboViewModel<int, int>> PeopleAdministrator { get; set; }
    }
}