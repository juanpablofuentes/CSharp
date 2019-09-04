using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.SalesRate
{
    public class SalesRatesViewModel
    {
        public MultiItemViewModel<SalesRateViewModel, int> SalesRates { get; set; }
        public SalesRateFilterViewModel SalesRateFilters { get; set; }
    }
}
