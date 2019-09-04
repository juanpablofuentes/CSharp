using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class DepartmentViewModelExtensions
    {
        public static DepartmentViewModel ToViewModel(this DepartmentDto source)
        {
            DepartmentViewModel result = null;
            if (source != null)
            {
                result = new DepartmentViewModel()
                {
                    Id = source.Id,
                    Description = source.Name,
                    HasPeopleAssigned = source.HasPeopleAssigned,
                };
            }
            return result;
        }

        public static DepartmentDto ToDto(this DepartmentViewModel source)
        {
            DepartmentDto result = null;
            if (source != null)
            {
                result = new DepartmentDto()
                {
                    Id = source.Id,
                    Name = source.Description,
                };
            }
            return result;
        }

        public static IList<DepartmentDto> ToDto(this IList<DepartmentViewModel> source)
        {
            return source?.MapList(d => d.ToDto());
        }

        public static IList<DepartmentViewModel> ToViewModel(this IList<DepartmentDto> source)
        {
            return source?.MapList(d => d.ToViewModel());
        }
    }
}
