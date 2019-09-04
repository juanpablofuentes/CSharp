using Group.Salto.Controls.Table.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Families
{
    public class FamiliesViewModel
    {
        public MultiItemViewModel<FamilieViewModel, int> Families { get; set; }
        public FamiliesFilterViewModel FamiliesFilters { get; set; }
    }
}