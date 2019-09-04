using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.SOM.Web.Models.MultiSelect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class MultiSelectItemExtension
    {
        public static MultiSelectItem ToViewModel(this MultiSelectItemDto source)
        {
            MultiSelectItem result = null;

            if (source != null)
            {
                result = new MultiSelectItem()
                {
                    LabelName = source.LabelName,
                    Value = source.Value,
                    IsChecked = source.IsChecked
                };
            }

            return result;
        }

        public static IList<MultiSelectItem> ToViewModel(this IList<MultiSelectItemDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static MultiSelectItemDto ToDto(this MultiSelectItem source)
        {
            MultiSelectItemDto result = null;

            if (source != null)
            {
                result = new MultiSelectItemDto()
                {
                    LabelName = source.LabelName,
                    Value = source.Value,
                    IsChecked = source.IsChecked
                };
            }

            return result;
        }

        public static IList<MultiSelectItemDto> ToDto(this IList<MultiSelectItem> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static MultiSelectViewModel SetSelectedValues(this IList<BaseNameIdDto<int>> items, IList<int> selected, string title = "")
        {
            MultiSelectViewModel result = null;
            if (items != null && items.Any())
            {
                result = new MultiSelectViewModel(title);
                result.Items = new List<MultiSelectItem>();
                foreach (var element in items)
                {
                    result.Items.Add(new MultiSelectItem()
                    {
                        Value = element.Id.ToString(),
                        IsChecked = selected?.Any(x => x == element.Id) ?? false,
                        LabelName = element.Name
                    });
                }
            }

            return result;
        }

        public static MultiSelectViewModel SetSelectedValues(this IList<BaseNameIdDto<Guid>> items, IList<Guid> selected, string title = "")
        {
            MultiSelectViewModel result = null;
            if (items != null && items.Any())
            {
                result = new MultiSelectViewModel(title)
                {
                    Items = new List<MultiSelectItem>()
                };
                foreach (var element in items)
                {
                    result.Items.Add(new MultiSelectItem()
                    {
                        Value = element.Id.ToString(),
                        IsChecked = selected?.Any(x => x == element.Id) ?? false,
                        LabelName = element.Name
                    });
                }
            }

            return result;
        }
    }
}