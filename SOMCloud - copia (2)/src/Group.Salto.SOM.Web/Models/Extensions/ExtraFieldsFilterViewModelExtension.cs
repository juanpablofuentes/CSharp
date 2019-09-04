using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields;
using Group.Salto.SOM.Web.Models.ExtraFields;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ExtraFieldsFilterViewModelExtension
    {
        public static ExtraFieldsFilterDto ToDto(this ExtraFieldsFilterViewModel source)
        {
            ExtraFieldsFilterDto result = null;
            if (source != null)
            {
                result = new ExtraFieldsFilterDto()
                {
                    Name = source.Name,
                    LanguageId = source.LanguageId,
                    OrderBy = source.OrderBy,
                    Asc = source.Asc,
                };
            }
            return result;
        }
    }
}