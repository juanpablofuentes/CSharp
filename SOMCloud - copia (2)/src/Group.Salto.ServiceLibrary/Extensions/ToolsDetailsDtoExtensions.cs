using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class ToolsDetailsDtoExtensions
    {
        public static ToolsDetailsDto ToDetailDto(this Tools source)
        {
            ToolsDetailsDto result = null;
            if (source != null)
            {
                result = new ToolsDetailsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Observations = source.Observations,
                    VehicleId = source.VehicleId,
                    VehicleName = source.Vehicle?.Name,
                    Types = source.ToolsToolTypes?.ToList().ToToolsToolTypeDto()
              
                };
            }
            return result;
        }

        public static Tools ToEntity(this ToolsDetailsDto source)
        {
            Tools result = null;
            if (source != null)
            {
                result = new Tools()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    VehicleId = source.VehicleId,
                    
                };
            }
            return result;
        }

        public static Tools Update(this Tools target, ToolsDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.VehicleId = source.VehicleId;             
            }

            return target;
        }
    }
}