using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.PurchaseRate
{
    public class PurchaseRatesViewModel
    {
        public MultiItemViewModel<PurchaseRateViewModel, int> PurchaseRate { get; set; }
        public PurchaseRateFilterViewModel PurchaseRateFilters { get; set; }
    }
}