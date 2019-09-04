using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFieldTypes;
using Group.Salto.SOM.Web.Models.ExtraFieldsTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExtraFieldsTypesViewModelExtensions
    {
        public static ExtraFieldsTypesViewModel ToExtraFieldsTypes(this ExtraFieldsTypesDto source)
        {
            ExtraFieldsTypesViewModel result = null;
            if (source != null)
            {
                result = new ExtraFieldsTypesViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    AllowedValuesVisibility = source.AllowedValuesVisibility,
                    ErpSystemVisibility = source.ErpSystemVisibility,
                    IsMandatoryVisibility = source.IsMandatoryVisibility,
                    MultipleChoiceVisibility = source.MultipleChoiceVisibility
                };
            }
            return result;
        }

        public static IList<ExtraFieldsTypesViewModel> ToExtraFieldsTypes(this IList<ExtraFieldsTypesDto> source)
        {
            return source?.MapList(pk => pk.ToExtraFieldsTypes());
        }
    }
}
