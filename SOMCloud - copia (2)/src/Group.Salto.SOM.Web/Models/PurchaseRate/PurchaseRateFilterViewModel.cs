using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.PurchaseRate
{
    public class PurchaseRateFilterViewModel : BaseFilter
    {
        public PurchaseRateFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}