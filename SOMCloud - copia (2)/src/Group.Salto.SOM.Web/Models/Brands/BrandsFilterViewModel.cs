using Group.Salto.Controls.Table.Models;
using System;

namespace Group.Salto.SOM.Web.Models.Brands
{
    public class BrandsFilterViewModel : BaseFilter
    {
        public BrandsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}