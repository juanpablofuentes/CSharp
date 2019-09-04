using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FamiliesDtoExtensions
    {
        public static FamiliesDto ToDto(this Families source)
        {
            FamiliesDto result = null;
            if (source != null)
            {
                result = new FamiliesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IList<FamiliesDto> ToDto(this IList<Families> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}