using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.PreconditionsTypes;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class PreconditionTypesDtoExtensions
    {
        public static PreconditionsTypesDto ToDto(this PreconditionTypes source)
        {
            PreconditionsTypesDto result = null;

            if (source != null)
            {
                result = new PreconditionsTypesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                };
            }
            return result;
        }

        public static List<PreconditionsTypesDto> ToListDto(this IQueryable<PreconditionTypes> source)
        {
            return source?.MapList(x => x.ToDto()).ToList();
        }
    }
}