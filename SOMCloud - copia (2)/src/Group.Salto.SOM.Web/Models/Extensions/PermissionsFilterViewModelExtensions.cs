using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Permisions;
using Group.Salto.SOM.Web.Models.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class PermissionsFilterViewModelExtensions
    {
        public static PermissionsFilterDto ToFilterDto(this PermissionsFilterViewModel source)
        {
            PermissionsFilterDto result = null;
            if (source != null)
            {
                result = new PermissionsFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }

        public static PermissionListViewModel ToViewModel(this PermissionsDto source)
        {
            PermissionListViewModel eventCategories = null;
            if (source != null)
            {
                eventCategories = new PermissionListViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations
                };
            }
            return eventCategories;
        }

        public static IList<PermissionListViewModel> ToViewModel(this IList<PermissionsDto> source)
        {
            return source?.MapList(ec => ec.ToViewModel());
        }
    }
}
