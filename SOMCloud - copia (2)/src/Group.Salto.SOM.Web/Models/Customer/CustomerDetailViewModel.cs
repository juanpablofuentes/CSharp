using System.Collections.Generic;
using Group.Salto.SOM.Web.Models.Modules;

namespace Group.Salto.SOM.Web.Models.Customer
{
    public class CustomerDetailViewModel
    {
        public CustomerViewModel Customer { get; set; }
        public IList<ModuleViewModel> Modules { get; set; }
        public string ModuleIdsSelected { get; set; }
        public IList<KeyValuePair<int,string>> Countries { get; set; }
        public int CountrySelected { get; set; }
        public int StateSelected { get; set; }
        public int RegionSelected { get; set; }
        public int MunicipalitySelected { get; set; }
    }
}
