using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Families
{
    public class FamiliesDetailsViewModel :FamilieViewModel
    {
        public IList<SubFamiliesViewModel> SubFamiliesList { get; set; }
    }
}