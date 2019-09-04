using Group.Salto.SOM.Web.Models.MultiCombo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Brands
{
    public class BrandsDetailsViewModel :BrandViewModel
    {
        public IList<ModelViewModel> ModelsSelected { get; set; }
    }
}