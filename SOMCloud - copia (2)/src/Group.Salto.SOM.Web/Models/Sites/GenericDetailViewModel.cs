using Group.Salto.SOM.Web.Models.Contracts;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Sites
{
    public class GenericDetailViewModel : SitesViewModel
    {
        public GenericDetailViewModel() {
            ContactsSelected = new List<ContactsEditViewModel>();
        }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Fax { get; set; }
        public int FinalClientId { get; set; }
        public IList<ContactsEditViewModel> ContactsSelected { get; set; }
    }
}