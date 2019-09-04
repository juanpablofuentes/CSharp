using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Company;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class CompanyDtoExtensions
    {
        public static CompanyDto ToDto(this Companies source)
        {
            CompanyDto result = null;
            if (source != null)
            {
                result = new CompanyDto();
                source.ToDto(result);
            }

            return result;
        }

        public static void ToDto(this Companies source, CompanyDto target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.CostKm = source.CostKm;
                target.Name = source.Name;
                target.HasPeopleAssigned = source.People?.Any() ?? false;
            }
        }

        public static Companies ToEntity(this CompanyDto source)
        {
            Companies result = null;
            if (source != null)
            {
                result = new Companies();
                source.ToEntity(result);
            }

            return result;
        }

        public static void ToEntity(this CompanyDto source, Companies target)
        {
            if (source != null && target != null)
            {
                target.Id = source.Id;
                target.CostKm = source.CostKm;
                target.Name = source.Name;
            }
        }

        public static IList<CompanyDto> ToDto(this IList<Companies> source)
        {
            return source?.MapList(c => c.ToDto());
        }
    }
}