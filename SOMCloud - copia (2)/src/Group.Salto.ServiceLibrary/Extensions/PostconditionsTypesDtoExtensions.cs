using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.PostconditionsTypes;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PostconditionsTypesDtoExtensions
    {
        public static PostconditionsTypesDto ToDto(this PostconditionTypes source)
        {
            PostconditionsTypesDto result = null;

            if (source != null)
            {
                result = new PostconditionsTypesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return result;
        }

        public static List<PostconditionsTypesDto> ToListDto(this IQueryable<PostconditionTypes> source)
        {
            return source?.MapList(x => x.ToDto()).ToList();
        }
    }
}