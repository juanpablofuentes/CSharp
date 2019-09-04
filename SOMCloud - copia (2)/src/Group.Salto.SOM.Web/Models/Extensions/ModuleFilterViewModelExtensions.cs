using Group.Salto.ServiceLibrary.Common.Dtos.Modules;
using Group.Salto.SOM.Web.Models.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ModuleFilterViewModelExtensions
    {
        public static ModuleFilterDto ToDto(this ModuleFilterViewModel source)
        {
            ModuleFilterDto result = null;
            if (source != null)
            {
                result = new ModuleFilterDto()
                {
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result ?? new ModuleFilterDto();
        }
    }
}