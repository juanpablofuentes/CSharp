using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Helpers;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Enums;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class WorkOrderFormsDtoExtensions
    {
        public static WorkOrderFormsDto ToDto(this Services source, bool isFather)
        {
            WorkOrderFormsDto result = null;
            if (source != null)
            {
                result = new WorkOrderFormsDto()
                {
                    Id = source.Id,
                    CreationDate = DateTimeZoneHelper.ToLocalTimeByUser(source.CreationDate).ToString(),
                    PredefinedService = source.PredefinedService?.Name,
                    Status = source.FormState,
                    DeliveryNote = source.DeliveryNote,
                    TechnicianName = source.PeopleResponsible?.Name,
                    TechnicianSurname = source.PeopleResponsible?.FisrtSurname,
                    Observations = source.Observations,
                    ExtraFieldsValues = source.ExtraFieldsValues.ToList().ToExtraFieldListDto(),
                    DateForOrder = source.CreationDate,
                    IsFather = isFather,
                    IsWOClosed = source.WorkOrder?.WorkOrderStatus?.IsWoclosed ?? false,
                    WOBillable = source.WorkOrder?.Billable ?? false,
                    HasBill = source.WorkOrder?.Bill != null,
                    HasWOAnalisis = source.WorkOrder?.WorkOrderAnalysis != null,
                    WorkOrderId = source.WorkOrder?.Id,
                    TaskId = (source.WorkOrder?.Bill != null && source.WorkOrder?.Bill.Count() > 0) ? source.WorkOrder?.Bill?.FirstOrDefault(x => x.ServiceId == source.Id)?.TaskId : null,
                    BillState = (source.WorkOrder?.Bill != null && source.WorkOrder?.Bill.Count() > 0) ? (BillStatus?)source.WorkOrder?.Bill?.FirstOrDefault(x => x.ServiceId == source.Id)?.Status : null,
                };

                SetIsEditable(result);
                result.IsSystemForm = GetIsSystemForm(source);
            }
            return result;
        }

        public static IList<WorkOrderFormsDto> ToListDto(this IList<Services> source, bool isFather)
        {
            return source?.MapList(x => x.ToDto(isFather));
        }

        private static void SetIsEditable(WorkOrderFormsDto service)
        {
            if (service.IsClosedOrderAccounting())
            {
                service.IsEditable = false;
            }
            else if (service.IsWorkOrderOpen())
            {
                if (service.BillState == BillStatus.Processed)
                {
                    service.IsEditable = false;
                }
            }
            else if (service.IsClosedOrder())
            {
                if (service.BillState == BillStatus.Processed)
                {
                    service.IsEditable = false;
                }
            }
        }

        private static bool GetIsSystemForm(Services source)
        {
             return source.ExtraFieldsValues.Any(x => x.ExtraField.DelSystem);
        }
    }
}