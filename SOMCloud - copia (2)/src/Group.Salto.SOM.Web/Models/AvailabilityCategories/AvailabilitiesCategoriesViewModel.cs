using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.AvailabilityCategories
{
    public class AvailabilitiesCategoriesViewModel
    {
        public MultiItemViewModel<AvailabilityCategoriesViewModel, int> AvailabilitiesCategories { get; set; }
    }
}
