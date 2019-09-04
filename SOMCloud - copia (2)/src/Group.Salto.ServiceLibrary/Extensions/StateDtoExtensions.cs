using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Country;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class StateDtoExtensions
    {
        public static StateDto ToDto(this States source)
        {
            StateDto result = null;
            if (source != null)
            {
                result = new StateDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Municipalities = source?.Municipalities?.ToList()?.ToDto(),
                };
            }
            return result;
        }

        public static string ToInsertDto(this List<States> source)
        {
            string result = string.Empty;
            if (source != null)
            {
                foreach (States state in source)
                { 
                    result += $"({state.Id}, '{state.Name}'),";
                }
            }
            return result.Substring(0, result.Length - 1);
        }

        public static IList<StateDto> ToDto(this IList<States> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}
