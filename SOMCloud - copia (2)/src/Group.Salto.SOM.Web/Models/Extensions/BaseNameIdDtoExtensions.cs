using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class BaseNameIdDtoExtensions
    {
        public static IList<KeyValuePair<T, string>> ToKeyValuePair<T>(this IList<BaseNameIdDto<T>> source)
        {
            return source?.Select(c => new KeyValuePair<T, string>(c.Id, c.Name))?.OrderBy(x => x.Value)?.ToList();
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this IList<BaseNameIdDto<T>> source)
        {
            return source.MapList(x => new SelectListItem(x.Name, x.Id.ToString()));
        }
        public static IEnumerable<SelectListItem> ToInverseSelectList<T>(this IList<BaseNameIdDto<T>> source)
        {
            return source.MapList(x => new SelectListItem(x.Id.ToString(), x.Name));
        }
        public static IEnumerable<MultiSelectList> ToMultiSelectList<T>(this IList<BaseNameIdDto<T>> source)
        {
            return source.MapList(x => new MultiSelectList(x.Name, x.Id.ToString()));
        }
    }
}