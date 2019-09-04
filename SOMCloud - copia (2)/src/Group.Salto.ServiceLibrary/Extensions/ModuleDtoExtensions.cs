using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities;
using Group.Salto.ServiceLibrary.Common.Dtos.Modules;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ModuleDtoExtensions
    {
        public static ModuleDto ToDto(this Module source)
        {
            ModuleDto result = null;
            if (source != null)
            {
                result = new ModuleDto()
                {
                    Id = source.Id,
                    Key = source.Key,
                    Description = source.Description
                };
            }

            return result;
        }

        public static IList<ModuleDto> ToDto(this IList<Module> source)
        {
            return source.MapList(x => x.ToDto());
        }
    }
}
