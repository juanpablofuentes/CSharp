using Group.Salto.Controls.Table.Models;
using Group.Salto.SOM.Web.Models.Vehicles;
using System;

namespace Group.Salto.SOM.Web.Models.Brands
{
    public class BrandsViewModel
    {
        public MultiItemViewModel<BrandViewModel, int> Brands { get; set; }

        public BrandsFilterViewModel BrandsFilters { get; set; }
    }
}