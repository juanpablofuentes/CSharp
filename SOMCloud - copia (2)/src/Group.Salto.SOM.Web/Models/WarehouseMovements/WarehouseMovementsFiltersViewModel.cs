using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.WarehouseMovements
{
    public class WarehouseMovementsFiltersViewModel : BaseFilter
    {
        public WarehouseMovementsFiltersViewModel()
        {
            OrderBy = "StartDate";
        }

        public int WarehouseId { get; set; }
        public IEnumerable<int> Items { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}