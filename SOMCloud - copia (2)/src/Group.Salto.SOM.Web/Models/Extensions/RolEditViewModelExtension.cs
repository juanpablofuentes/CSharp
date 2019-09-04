using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Rols;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Rol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Group.Salto.SOM.Web.Models.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Group.Salto.Common.Entities.Contracts;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class RolEditViewModelExtension
    {
        public static RolEditViewModel ToEditViewModel(this RolDto source)
        {
            RolEditViewModel result = null;
            if (source != null)
            {
                result = new RolEditViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description
                };
            }
            return result;
        }

        public static IEnumerable<RolEditViewModel> ToEditViewModel(this IEnumerable<RolDto> source)
        {
            return source?.MapList(x => x.ToEditViewModel());
        }

        public static RolDto ToRolDto(this RolEditViewModel source)
        {
            RolDto result = null;
            if (source != null)
            {
                result = new RolDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ActionsRoles = source.ActionRoles.Items.ToDto()
                };
            }
            return result;
        }


        public static SelectListItem ToSelectRolListItem<T>(this T source) where T : IKeyValue
        {
            SelectListItem result = null;
            if (source != null)
            {
                result = new SelectListItem()
                {
                    Value = source.Id.ToString(),
                    Text = source.Name,
                };
            }

            return result;
        }

        public static IEnumerable<SelectListItem> ToSelectRolListItemEnumerable<T>(this IEnumerable<T> source) where T : IKeyValue
        {
            foreach (var data in source)
            {
                yield return ToSelectRolListItem(data);
            }
        }
    }
}