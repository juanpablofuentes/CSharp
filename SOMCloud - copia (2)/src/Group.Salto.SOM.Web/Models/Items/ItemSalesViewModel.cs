using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemSalesViewModel
    {
        public IList<RateViewModel> SalesRates { get; set; }
        public IList<RateViewModel> OtherSalesRates { get; set; }
    }
}