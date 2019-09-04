using System.Collections.Generic;
using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Mobility.Dto.Vehicle;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class VehicleBasicDtoExtensions
    {
        public static VehicleBasicDto ToBasicDto(this Vehicles source)
        {
            VehicleBasicDto result = null;
            if (source != null)
            {
                result = new VehicleBasicDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    PeopleId = source.PeopleDriverId
                };
            }
            return result;
        }

        public static IList<VehicleBasicDto> ToBasicDto(this IList<Vehicles> source)
        {
            return source.MapList(x => x.ToBasicDto());
        }
    }
}
