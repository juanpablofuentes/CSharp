using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class VehiclesDetailsDtoExtensions
    {
        public static VehiclesDetailsDto ToDetailDto(this Vehicles source)
        {
            VehiclesDetailsDto result = null;
            if (source != null)
            {
                //TODO What show in list DriverId or DriverName on List??
                result = new VehiclesDetailsDto()
                {
                    Name = source.Name,
                    Id = source.Id,
                    Description = source.Description,
                    Observations = source.Observations,
                    DriverId = source.PeopleDriverId,
                    Driver = source.PeopleDriver?.FullName
                };
            }
            return result;
        }
        public static Vehicles ToEntity(this VehiclesDetailsDto source)
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