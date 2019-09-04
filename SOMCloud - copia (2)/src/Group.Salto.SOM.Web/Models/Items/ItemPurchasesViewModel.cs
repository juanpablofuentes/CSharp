using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemPurchasesViewModel
    {
        public IList<RateViewModel> PurchaseRates { get; set; }
        public IList<RateViewModel> OtherPurchasesRates { get; set; }
    }
}