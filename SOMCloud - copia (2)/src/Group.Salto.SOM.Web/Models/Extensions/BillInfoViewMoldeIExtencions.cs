using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using Group.Salto.SOM.Web.Models.Bill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BillInfoViewMoldeIExtencions
    {
        public static BillInfoDto ToDto(this BillViewModel source)
        {
            BillInfoDto result = null;
            if (source != null)
            {
                result = new BillInfoDto()
                {
                    Id = source.Id,
                    WorkOrderId = source.WorkOrderId,
                    ServiceId = source.ServiceId,
                    Date = source.Date,
                    Task = source.Task,
                    InternalIdentifier = source.InternalIdentifier,
                    Status = source.Status,
                    ProjectSerie = source.ProjectSerie,
                    ExternalSistemNumber = source.ExternalSistemNumber,
                    WorkerName = source.WorkerName,
                };
            }
            return result;
        }

        public static IList<BillInfoDto> ToListDto(this IList<BillViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static BillViewModel ToViewModel(this BillInfoDto source)
        {
            BillViewModel result = null;
            if (source != null)
            {
                result = new BillViewModel()
                {
                    Id = source.Id,
                    WorkOrderId = source.WorkOrderId,
                    ServiceId = source.ServiceId,
                    Date = source.Date,
                    Task = source.Task,
                    InternalIdentifier = source.InternalIdentifier.Value,
                    Status = source.Status,
                    ProjectSerie = source.ProjectSerie,
                    ExternalSistemNumber = source.ExternalSistemNumber,
                    WorkerName = source.WorkerName,
                };
            }
            return result;
        }

        public static IList<BillViewModel> ToListViewModel(this IList<BillInfoDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}