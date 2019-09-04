using Group.Salto.ServiceLibrary.Common.Dtos.WarehouseMovements;
using Group.Salto.SOM.Web.Models.WarehouseMovements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WarehouseMovementsFiltersViewModelExtensions
    {
        public static WarehouseMovementsFilterDto ToDto (this WarehouseMovementsFiltersViewModel source)
        {
            WarehouseMovementsFilterDto result = null;
            if (source != null)
            {
                result = new WarehouseMovementsFilterDto()
                {
                    Items = source.Items?.ToList() ?? new List<int>(),
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new WarehouseMovementsFilterDto();
        }
    }
}