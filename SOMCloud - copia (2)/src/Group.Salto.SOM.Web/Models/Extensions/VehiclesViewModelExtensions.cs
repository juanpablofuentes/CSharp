using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;
using Group.Salto.SOM.Web.Models.Vehicles;
using Group.Salto.SOM.Web.Models.Result;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class VehiclesViewModelExtension
    {
        public static ResultViewModel<VehicleViewModel> ToViewModel(this ResultDto<VehiclesDto> source)
        {
            var response = source != null ? new ResultViewModel<VehicleViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static VehicleViewModel ToViewModel(this VehiclesDto source)
        {
            VehicleViewModel result = null;
            if (source != null)
            {
                result = new VehicleViewModel()
                {
                    Id = source.Id,
                    LicensePlate = source.Name,
                    UpdateDate = source.UpdateDate,
                    Description = source.Description,
                    Observations = source.Observations,
                    Driver = source.Driver
                };
            }
            return result;
        }

        public static IList<VehicleViewModel> ToViewModel(this IList<VehiclesDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static VehiclesDto ToDto(this VehicleViewModel source)
        {
            VehiclesDto result = null;
            if (source != null)
            {
                result = new VehiclesDto()
                {
                    Id = source.Id ?? 0,
                    Name = source.LicensePlate,
                    UpdateDate = source.UpdateDate,
                    Description = source.Description,
                    Observations = source.Observations,
                    Driver = source.Driver
                };
            }
            return result;
        }
    }
}