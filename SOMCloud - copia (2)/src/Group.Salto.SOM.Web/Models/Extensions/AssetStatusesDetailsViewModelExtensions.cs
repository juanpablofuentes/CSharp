using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using Group.Salto.SOM.Web.Models.AssetStatuses;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetStatusesDetailsViewModelExtensions
    {
        public static AssetStatusesDto ToDto(this AssetStatusesDetailsViewModel source)
        {
            AssetStatusesDto result = null;
            if (source != null)
            {
                result = new AssetStatusesDto()
                {
                    Id = source.Id ,
                    Name = source.Name,
                    Color = source.Color,
                    IsRetiredState = source.IsRetiredState,
                    IsDefault = source.IsDefault
                };
            }
            return result;
        }
    }
}