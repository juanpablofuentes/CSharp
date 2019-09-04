using System;
using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;

namespace Group.Salto.SOM.Web.Models.Modules
{
    public static class ModuleViewModelExtensions
    {
        public static ModuleViewModel ToViewModel(this ModuleDto source)
        {
            ModuleViewModel result = null;
            if (source != null)
            {
                result = new ModuleViewModel()
                {
                    Id = source.Id,
                    Key = source.Key,
                    Description = source.Description,
                };
            }

            return result;
        }
        
        public static IList<ModuleViewModel> ToViewModel(this IList<ModuleDto> source)
        {
            return source.MapList(x => x.ToViewModel());
        }
        
        public static ModuleDto ToDto(this ModuleViewModel source)
        {
            ModuleDto result = null;
            if (source != null)
            {
                result = new ModuleDto()
                {
                    Id = source.Id ?? default(Guid),
                    Key = source.Key,
                    Description = source.Description,
                };
            }

            return result;
        }

        public static IList<ModuleDto> ToDto(this IList<ModuleViewModel> source)
        {
            return source.MapList(x => x.ToDto());
        }
    }
}
