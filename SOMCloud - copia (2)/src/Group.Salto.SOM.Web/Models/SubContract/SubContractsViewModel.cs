using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.SubContract
{
    public class SubContractsViewModel
    {
        public MultiItemViewModel<SubContractViewModel, int> SubContracts { get; set; }
        public SubContractFilterViewModel SubContractFilter { get; set; }
    }
}