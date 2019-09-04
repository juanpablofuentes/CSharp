using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements
{
    public class WarehouseMovementsFilterDto : BaseFilterDto
    {
        public IList<int> Items { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}