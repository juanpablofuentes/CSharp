using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.Vehicles;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.People;
using Group.Salto.ServiceLibrary.Common.Dtos.Vehicles;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.Vehicles
{
    public class VehiclesService : BaseService, IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        public VehiclesService(ILoggingService logginingService, IVehiclesRepository vehiclesRepository) : base(logginingService)
        {
            _vehiclesRepository = vehiclesRepository ?? throw new ArgumentNullException($"{nameof(vehiclesRepository)} is null ");
        }

        public ResultDto<IList<VehiclesDto>> GetAllFiltered(VehiclesFilterDto filter)
        {
            LogginingService.LogInfo($"Get All  Vehicle Filtered");
            var query = _vehiclesRepository.GetAllNotDeleted();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            data = OrderBy(data, filter);
            return ProcessResult<IList<VehiclesDto>>(data.ToList());
        }

        private IQueryable<VehiclesDto> OrderBy(IQueryable<VehiclesDto> query, VehiclesFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Vehicle");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Driver);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.UpdateDate);
            return query;
        }

        public ResultDto<VehiclesDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Vehicle");
            var data = _vehiclesRepository.GetById(id);
            return ProcessResult(data.ToDetailDto());
        }

        public ResultDto<VehiclesDetailsDto> UpdateVehicle(VehiclesDetailsDto source)
        {
            LogginingService.LogInfo($"Update Vehicle");
            ResultDto<VehiclesDetailsDto> result = null;
            var findVehicle = _vehiclesRepository.GetById(source.Id);
            if (findVehicle != null)
            {
                var updatedVehicle = findVehicle.Update(source);
                var resultRepository = _vehiclesRepository.UpdateVehicle(updatedVehicle);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<VehiclesDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeleteVehicle(int id)
        {
            LogginingService.LogInfo($"Delete vehicles by id {id}");
            ResultDto<bool> result = null;
            var vehicle = _vehiclesRepository.GetById(id);
            if (vehicle != null)
            {
                var resultSave = _vehiclesRepository.DeleteVehicle(vehicle);
                result = ProcessResult(resultSave.IsOk, resultSave);
            }
            return result ?? new ResultDto<bool>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = false,
            };
        }

        public ResultDto<VehiclesDetailsDto> CreateVehicle(VehiclesDetailsDto source)
        {
           LogginingService.LogInfo($"Create Vehicle");
           var newVehicle = source.ToEntity();
           var result = _vehiclesRepository.CreateVehicle(newVehicle);
           return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get Vehicle Key Value");
            var data = _vehiclesRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}