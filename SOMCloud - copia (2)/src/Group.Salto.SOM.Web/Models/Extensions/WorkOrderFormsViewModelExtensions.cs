using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderFormsViewModelExtensions
    {
        public static WorkOrderFormsViewModel ToFormsViewModel(this WorkOrderFormsDto source)
        {
            WorkOrderFormsViewModel result = null;
            if(source != null)
            {
                result = new WorkOrderFormsViewModel();
                source.ToFormsViewModel(result);
            }
            return result;
        }

        public static void ToFormsViewModel(this WorkOrderFormsDto source, WorkOrderFormsViewModel target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.CreationDate = source.CreationDate;
                target.PrefinedService = source.PredefinedService;
                target.Status = source.Status;
                target.DeliveryNote = source.DeliveryNote;
                target.TechnicianName = source.TechnicianName;
                target.TechnicianSurname = source.TechnicianSurname;
                target.Observations = source.Observations;
                target.IsFather = source.IsFather;
                target.IsEditable = source.IsEditable;
                target.ExtraFieldValues = source.ExtraFieldsValues.ToExtraFieldValuesViewModel();
            }
        }

        public static IList<WorkOrderFormsViewModel> ToListViewModel(this IList<WorkOrderFormsDto> source)
        {
            return source?.MapList(x => x.ToFormsViewModel());
        }
    }
}