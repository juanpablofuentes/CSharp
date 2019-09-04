using Group.Salto.Common.Constants;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.WorkOrders;
using Group.Salto.ServiceLibrary.Helpers;
using Group.Salto.SOM.Web.Models.WorkOrder;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WorkOrderExtraFieldsValuesViewModelExtensions
    {
        public static WorkOrderExtraFieldsValuesViewModel ToExtraFieldValuesViewModel(this WorkOrderExtraFieldsValuesDto source)
        {
            WorkOrderExtraFieldsValuesViewModel result = new WorkOrderExtraFieldsValuesViewModel();
            if (source != null)
            {
                result.Id = source.Id;
                result.ExtraFieldValue = source.ExtraFieldValue;
                result.ExtraFieldName = source.ExtraFieldName;
                result.Signature = source.Signature;
                result.ExtraFieldType = source.ExtraFieldType;
                result.Files = source.Files;
                result.IsFile = source.IsFile;
                result.MaterialForm = source.MaterialForm;
            }
            return result;
        }

        public static IList<WorkOrderExtraFieldsValuesViewModel> ToExtraFieldValuesViewModel(this IList<WorkOrderExtraFieldsValuesDto> source)
        {
            return source?.MapList(x => x.ToExtraFieldValuesViewModel());
        }
    }
}