using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Contracts
{
    public class ContractsListViewModel
    {
        public MultiItemViewModel<ContractListViewModel, int> Contract { get; set; }
        public ContractsFilterViewModel ContractsFilters { get; set; }
    }
}