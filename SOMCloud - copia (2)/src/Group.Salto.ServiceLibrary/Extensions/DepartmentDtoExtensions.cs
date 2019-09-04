using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class DepartmentDtoExtensions
    {
        public static DepartmentDto ToDto(this Departments source)
        {
            DepartmentDto result = null;
            if (source != null)
            {
                result = new DepartmentDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    HasPeopleAssigned = source.People?.Any() ?? false,
                };
            }

            return result;
        }

        public static Departments ToEntity(this DepartmentDto source)
        {
            Departments result = null;
            if (source != null)
            {
                result = new Departments()
                {
                    Name = source.Name,
                    Id = source.Id,
                };
            }

            return result;
        }

        public static void UpdateDepartment(this Departments target, DepartmentDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
            }
        }

        public static IList<DepartmentDto> ToDto(this IList<Departments> source)
        {
            return source?.MapList(d => d.ToDto());
        }

        public static IList<Departments> ToEntity(this IList<DepartmentDto> source)
        {
            return source?.MapList(d => d.ToEntity());
        }
    }
}