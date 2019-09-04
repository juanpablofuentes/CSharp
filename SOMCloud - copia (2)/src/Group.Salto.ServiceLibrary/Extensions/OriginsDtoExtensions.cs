using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Origins;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class OriginsDtoExtensions
    {
        public static OriginsDto ToListDto(this Origins source)
        {
            OriginsDto result = null;
            if (source != null)
            {
                result = new OriginsDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return result;
        }

        public static OriginsDto ToBaseDto(this Origins source)
        {
            OriginsDto result = null;
            if (source != null)
            {
                result = new OriginsDto();
                source.ToBaseDto(result);
            }

            return result;
        }

        public static void ToBaseDto(this Origins source, OriginsDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.CanBeDeleted = source.CanBeDeleted;
            }
        }

        public static IList<OriginsDto> ToBaseDto(this IList<Origins> source)
        {
            return source?.MapList(s => s.ToBaseDto());
        }

        public static Origins Update(this Origins target, OriginsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
            }

            return target;
        }

        public static Origins ToEntity(this OriginsDto source)
        {
            Origins result = null;
            if (source != null)
            {
                result = new Origins()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }

            return result;
        }
    }
}
