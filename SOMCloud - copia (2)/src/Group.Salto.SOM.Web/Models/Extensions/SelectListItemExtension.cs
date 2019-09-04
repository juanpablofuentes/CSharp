using Group.Salto.Common.Entities.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class SelectListItemExtension
    {
        public static SelectListItem ToSelectListItem<T>(this T source) where T : IKeyValue
        {
            SelectListItem result = null;
            if (source != null)
            {
                result = new SelectListItem()
                {
                    Value = source.Id.ToString(),
                    Text = source.Name,
                };
            }

            return result;
        }

        public static IEnumerable<SelectListItem> ToSelectListItemEnumerable<T>(this IEnumerable<T> source) where T : IKeyValue
        {
            foreach (var data in source)
            {
                yield return ToSelectListItem(data);
            }
        }

        public static IEnumerable<SelectListItem> ToSelectListItemString(this string[] source)
        {
            List<SelectListItem> result = new List<SelectListItem>();
            if (source != null)
            {
                foreach (var data in source)
                { 
                    result.Add(new SelectListItem()
                    {
                        Value = data,
                        Text = data,
                    });
                }
            }
            return result.AsEnumerable();
        }
    }
}
