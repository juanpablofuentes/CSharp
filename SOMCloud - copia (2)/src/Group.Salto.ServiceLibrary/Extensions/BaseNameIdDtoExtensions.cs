using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BaseNameIdDtoExtensions
    {
        public static IList<BaseNameIdDto<int>> ToBaseNameId(this Dictionary<int, string> source)
        {
            return source.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}