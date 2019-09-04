using Group.Salto.ServiceLibrary.Common.Dtos.Query;
using Group.Salto.SOM.Web.Models.Query;
using System;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AdvancedSearchQueryTypeViewModelExtensions
    {
        public static AdvancedSearchQueryTypeDto ToDto(this AdvancedSearchQueryTypeViewModel source)
        {
            AdvancedSearchQueryTypeDto result = null;
            if (source != null)
            {
                result = new AdvancedSearchQueryTypeDto()
                {
                    SearchType = source.SearchType,
                    Value = source.Value,
                    Text = source.Text,
                };
            }
            return result;
        }
    }
}