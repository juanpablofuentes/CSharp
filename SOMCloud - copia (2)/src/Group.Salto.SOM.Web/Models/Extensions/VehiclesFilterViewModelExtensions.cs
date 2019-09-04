using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;
using Group.Salto.SOM.Web.Models.Vehicles;
using Group.Salto.SOM.Web.Models.Result;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class VehiclesFilterViewModelExtensions
    {
        public static VehiclesFilterDto ToDto(this VehiclesFilterViewModel source)
        {
            VehiclesFilterDto result = null;
            if (source != null)
            {
                result = new VehiclesFilterDto()
                {
                    Name = source.Name,
                    Description = source.Description,
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }

            return result;
        }
    }
}