using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.SubFamilies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class SubFamiliesDtoExtensions
    {
        public static SubFamiliesDto ToDto(this SubFamilies source)
        {
            SubFamiliesDto result = null;
            if (source != null)
            {
                result = new SubFamiliesDto()
                {
                    Id = source.Id,
                    Nom = source.Nom,
                    Descripcio = source.Descripcio,
                };
            }
            return result;
        }

        public static IList<SubFamiliesDto> ToDto(this IList<SubFamilies> source)
        {
            return source?.MapList(c => c.ToDto());
        }

        public static SubFamilies ToEntity(this SubFamiliesDto source)
        {
            SubFamilies result = null;
            if (source != null)
            {
                result = new SubFamilies()
                {
                    Nom = source.Nom,
                    Id = source.Id,
                    Descripcio = source.Descripcio
                };
            }
            return result;
        }
        public static IList<SubFamilies> ToEntity(this IList<SubFamiliesDto> source)
        {
            return source?.MapList(sC => sC.ToEntity());
        }

        public static SubFamilies Update(this SubFamilies target, SubFamiliesDto source)
        {
            if (target != null && source != null)
            {
                target.Nom = source.Nom;
                target.Descripcio = source.Descripcio;
            }

            return target;
        }
    }
}