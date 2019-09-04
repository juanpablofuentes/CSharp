using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using Group.Salto.SOM.Web.Models.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RolIndexViewModelExtension
    {
        public static RolViewModel ToViewModel(this RolListDto source)
        {
            RolViewModel result = null;
            if (source != null)
            {
                result = new RolViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }

            return result;
        }

        public static IEnumerable<RolViewModel> ToViewModel(this IEnumerable<RolListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}