using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.FinalClients
{
    public class FinalClientsListViewModel
    {
        public MultiItemViewModel<FinalClientsViewModel, int> FinalClientsList { get; set; }
        public FinalClientsFilterViewModel Filters { get; set; }
    }
}