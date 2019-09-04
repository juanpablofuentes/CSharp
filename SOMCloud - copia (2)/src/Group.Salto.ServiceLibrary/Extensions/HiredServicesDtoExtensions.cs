using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class HiredServicesDtoExtensions
    {
        public static HiredServicesDto ToDto(this HiredServices source)
        {
            HiredServicesDto result = null;
            if (source != null)
            {
                result = new HiredServicesDto()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Observations = source.Observations
                };
            }
            return result;
        }

        public static HiredServices ToEntity(this HiredServicesDto source)
        {
            HiredServices result = null;
            if (source != null)
            {
                result = new HiredServices()
                {
                    Name = source.Name,
                    Observations = source.Observations
                };
            }
            return result;
        }
    }
}