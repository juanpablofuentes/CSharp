using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Families;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class FamiliesDetailsDtoExtensions
    {
        public static FamiliesDetailsDto ToDetailDto(this Families source)
        {
            FamiliesDetailsDto result = null;
            if (source != null)
            {
                result = new FamiliesDetailsDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    SubFamiliesList = source.SubFamilies?.ToList().ToDto()
                };
            }
            return result;
        }

        public static Families ToEntity(this FamiliesDetailsDto source)
        {
            Families result = null;
            if (source != null)
            {
                result = new Families()
                {
                    Name = source.Name,
                    Description = source.Description,
                    SubFamilies = source.SubFamiliesList?.ToList().ToEntity()
                };
            }
            return result;
        }

        public static Families Update(this Families target, FamiliesDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;   
            }

            return target;
        }
    }
}