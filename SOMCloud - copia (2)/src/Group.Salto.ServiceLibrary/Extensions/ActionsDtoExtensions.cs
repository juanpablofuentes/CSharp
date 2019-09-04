using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Actions;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ActionsDtoExtensions
    {
        public static ActionDto ToDto(this Actions source)
        {
            ActionDto result = null;
            if (source != null)
            {
                result = new ActionDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    UpdateDate = !ValidationsHelper.IsMinDateValue(source.UpdateDate) ? source.UpdateDate.ToShortDateString() : string.Empty
                };
            }

            return result;
        }

        public static Actions Update(this Actions target, ActionDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
            }

            return target;
        }

        public static IList<ActionDto> ToDto(this IList<Actions> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static ActionDto FromDictionaryToActionDto(this KeyValuePair<int, string> source)
        {
            ActionDto result = new ActionDto()
            {
                Id = source.Key,
                Name = source.Value
            };

            return result;
        }

        public static IEnumerable<ActionDto> FromDictionaryToActionDto(this Dictionary<int, string> source)
        {
            foreach (KeyValuePair<int, string> data in source)
            {
                yield return FromDictionaryToActionDto(data);
            }
        }
    }
}