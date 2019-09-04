using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using Group.Salto.SOM.Web.Models.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BillFilterViewModelextencions
    {
        public static BillFilterDto ToDto(this BillFilterViewModel source)
        {
            BillFilterDto result = null;
            if (source != null)
            {
                result = new BillFilterDto()
                {
                    WorkOrderId = source.WorkOrderId,
                    StartDate = source.StartDate,
                    EndDate = source.EndDate,
                    InternalIdentifier = source.InternalIdentifier,
                    ProjectSerie = source.ProjectSerie,
                    Project = source.Project?.Value == 0  ? null : source.Project?.Text,
                    Status = source.Status?.Value == 0 ? null : source.Status?.Text,
                    ItemId = source.ItemId?.Value == 0? null : source.ItemId?.Value,
                    Id = source.Id,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                    Size = source.Size,
                    Page = source.Page,
                    
                };
            }
            return result;
        }
    }
}