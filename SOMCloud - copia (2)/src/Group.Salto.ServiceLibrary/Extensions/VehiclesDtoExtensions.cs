using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class VehiclesDtoExtensions
    {
        public static VehiclesDto ToDto(this Vehicles source)
        {
            VehiclesDto result = null;
            if (source != null)
            {
                result = new VehiclesDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    UpdateDate = !ValidationsHelper.IsMinDateValue(source.UpdateDate) ? source.UpdateDate.ToShortDateString() : string.Empty,
                    Observations = source.Observations,
                    Driver = source.PeopleDriver?.FullName
                };
            }
            return result;
        }

        public static IList<VehiclesDto> ToDto(this IList<Vehicles> source)
        {
            return source.MapList(x => x.ToDto());
        }

        public static Vehicles Update(this Vehicles target, VehiclesDetailsDto source)
        {
            if (target != null && source != null)
            {
                target.Name = source.Name;
                target.Description = source.Description;
                target.Observations = source.Observations;
                target.PeopleDriverId = source.DriverId;
            }

            return target;
        }

        public static Vehicles ToEntity(this VehiclesDto source)
        {
            Vehicles result = null;
            if (source != null)
            {
                result = new Vehicles()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    Observations = source.Observations,
                    PeopleDriverId = source.DriverId
                };
            }

            return result;
        }
    }
}