using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Client
{
    public class ClientsViewModel
    {
        public MultiItemViewModel<ClientViewModel, int> Clients { get; set; }
        public ClientsFilterViewModel ClientsFilters { get; set; }
    }
}