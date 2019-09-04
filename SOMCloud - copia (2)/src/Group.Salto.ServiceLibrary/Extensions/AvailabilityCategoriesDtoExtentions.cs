using Group.Salto.ServiceLibrary.Common.Dtos.AvailabilityCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AvailabilityCategoriesDtoExtentions
    {
        public static IEnumerable<AvailabilityCategoriesDto> FromDictionaryToAvailabilityCategoriesDto(this Dictionary<int, string> source)
        {
            foreach (KeyValuePair<int, string> data in source)
            {
                yield return FromDictionaryToAvailabilityCategoriesDto(data);
            }
        }
        public static AvailabilityCategoriesDto FromDictionaryToAvailabilityCategoriesDto(this KeyValuePair<int, string> source)
        {
            AvailabilityCategoriesDto result = new AvailabilityCategoriesDto()
            {
                Id = source.Key,
                Name = source.Value
            };

            return result;
        }
    }
}
