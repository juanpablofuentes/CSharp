using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.ClosureCode;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ClosureCodeBaseDtoExtensions
    {
        public static ClosureCodeBaseDto ToBaseDto(this CollectionsClosureCodes source)
        {
            ClosureCodeBaseDto result = null;
            if (source != null)
            {
                result = new ClosureCodeBaseDto();
                source.ToBaseDto(result);
            }

            return result;
        }

        public static void ToBaseDto(this CollectionsClosureCodes source, ClosureCodeBaseDto target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Id = source.Id;
            }
        }

        public static void ToEntity(this ClosureCodeBaseDto source, CollectionsClosureCodes target)
        {
            if (source != null && target != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
            }
        }

        public static IList<ClosureCodeBaseDto> ToBaseDto(this IList<CollectionsClosureCodes> source)
        {
            return source?.MapList(s => s.ToBaseDto());
        }
    }
}