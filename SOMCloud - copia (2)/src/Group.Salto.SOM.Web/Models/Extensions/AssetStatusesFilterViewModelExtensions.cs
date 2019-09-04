using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.SOM.Web.Models.AssetStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetStatusesFilterViewModelExtensions
    {
        public static AssetStatusesFilterDto ToDto(this AssetStatusesFilterViewModel source)
        {
            AssetStatusesFilterDto result = null;
            if (source != null)
            {
                result = new AssetStatusesFilterDto()
                {
                    IsRetiredState = source.IsRetiredState,
                    Name = source.Name,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}