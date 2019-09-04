using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class VehiclesDetailsViewModelExtensions
    {
       
            public static ResultViewModel<VehicleDetailsViewModel> ToViewModel(this ResultDto<VehiclesDetailsDto> source)
            {
                var response = source != null ? new ResultViewModel<VehicleDetailsViewModel>()
                {
                    Data = source.Data.ToViewModel(),
                    Feedbacks = source.Errors.ToViewModel()
                } : null;
                return response;
            }

            public static VehicleDetailsViewModel ToViewModel(this VehiclesDetailsDto source)
            {
                VehicleDetailsViewModel result = null;
                if (source != null)
                {
                    result = new VehicleDetailsViewModel()
                    {
                        Id = source.Id,
                        LicensePlate = source.Name,
                        Description = source.Description,
                        Observations = source.Observations,
                        PeopleDriverId = source.DriverId,
                        Drivers = source.Drivers
                    };
                }
                return result;
            }
            public static VehiclesDetailsDto ToDto(this VehicleDetailsViewModel source)
            {
                VehiclesDetailsDto result = null;
                if (source != null)
                {
                    result = new VehiclesDetailsDto()
                    {
                        Id = source.Id ?? 0,
                        Name = source.LicensePlate,
                        Description = source.Description,
                        Observations = source.Observations,
                        DriverId = source.PeopleDriverId,
                        Driver = source.Driver,
                    };
                }
                return result;
            }
        }
    }

