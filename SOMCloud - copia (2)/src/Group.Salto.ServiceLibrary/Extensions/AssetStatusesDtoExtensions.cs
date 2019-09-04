using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.AssetStatuses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AssetStatusesDtoExtensions
    {
        public static AssetStatusesDto ToDto(this AssetStatuses source)
        {
            AssetStatusesDto result = null;
            if (source != null)
            {
                result = new AssetStatusesDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Color= source.Color,
                    IsDefault = source.IsDefault ?? false
                };
            }
            return result;
        }

        public static IList<AssetStatusesDto> ToDto(this IList<AssetStatuses> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static AssetStatuses Update(this AssetStatuses target, AssetStatusesDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Color = source.Color;
                target.IsRetiredState = source.IsRetiredState;
            }

            return target;
        }

        public static AssetStatusesDto ToDetailDto(this AssetStatuses source)
        {
            AssetStatusesDto result = null;
            if (source != null)
            {
                result = new AssetStatusesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Color = source.Color,
                    IsDefault = source.IsDefault ?? false,
                    IsRetiredState = source.IsRetiredState ?? false
                };
            }
            return result;
        }

        public static AssetStatuses ToEntity(this AssetStatusesDto source)
        {
            AssetStatuses result = null;
            if (source != null)
            {
                result = new AssetStatuses()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Color = source.Color,
                    IsRetiredState = source.IsRetiredState
                };
            }
            return result;
        }
    }
}