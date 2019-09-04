using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RolListDtoExtension
    {
        public static RolListDto ToListDto(this Roles source)
        {
            RolListDto result = null;
            if (source != null)
            {
                result = new RolListDto()
                {
                    Id = System.Convert.ToInt32(source.Id),
                    Name = source.Name,
                    Description = source.Description
                };
            }

            return result;
        }

        public static IList<RolListDto> ToListDto(this IList<Roles> source)
        {
            return source?.MapList(c => c.ToListDto());
        }
    }
}