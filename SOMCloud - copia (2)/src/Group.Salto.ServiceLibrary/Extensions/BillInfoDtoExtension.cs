using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Bill;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BillInfoDtoExtension
    {
        public static BillInfoDto ToDtoBill(this Bill source)
        {
            BillInfoDto result = null;
            if (source != null)
            {
                result = new BillInfoDto()
                {
                    Id = source.Id,
                    WorkOrderId = source.WorkorderId,
                    ServiceId = source.ServiceId,
                    Date = source.Date.ToShortDateString(),
                    Task = source.Task,
                    InternalIdentifier = source.Workorder?.FinalClientId,
                    Status = source.Status.ToString(),
                    ProjectSerie = source.Workorder?.Project?.Serie,
                    ExternalSistemNumber = source.ExternalSystemNumber,
                    WorkerName = source.People?.Name + ' ' + source.People?.FisrtSurname + ' ' + source.People?.SecondSurname,
                };
            }
            return result;
        }

        public static IList<BillInfoDto> ToListDtoBill(this IList<Bill> source)
        {
            return source?.MapList(x => x.ToDtoBill());
        }
    }
}