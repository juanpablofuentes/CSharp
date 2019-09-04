using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemPointsViewModel
    {
       public IList<RateViewModel> PointsRates { get; set; }
       public IList<RateViewModel> OtherPointsRates { get; set; }
    }
}