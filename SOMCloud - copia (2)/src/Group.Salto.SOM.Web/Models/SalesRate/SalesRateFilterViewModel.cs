using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.SalesRate
{
    public class SalesRateFilterViewModel : BaseFilter
    {
        public SalesRateFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}