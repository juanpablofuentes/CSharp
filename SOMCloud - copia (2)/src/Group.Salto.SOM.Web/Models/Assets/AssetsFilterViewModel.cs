using Group.Salto.SOM.Web.Models.MultiCombo;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Assets
{
    public class AssetsFilterViewModel : BaseFilter
    {
        public AssetsFilterViewModel()
        {
            OrderBy = "SerialNumber";
        }

        public string SerialNumber { get; set; }
        public int SitesId { get; set; }
        public string SitesName { get; set; }
        public int? FromSiteId { get; set; }
        public IList<MultiComboViewModel<int, int>> StatusesSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> ModelsSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> BrandsSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> FamiliesSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> SubFamiliesSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> SitesSelected { get; set; }
        public IList<MultiComboViewModel<int, int>> FinalClientsSelected { get; set; }
    }
}