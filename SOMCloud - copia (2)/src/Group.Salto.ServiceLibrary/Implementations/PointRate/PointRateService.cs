using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PointRate;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PointRate;
using Group.Salto.ServiceLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.PointRate
{
    public class PointRateService : BaseService, IPointRateService
    {
        private readonly IPointRateRepository _pointRateRepository;

        public PointRateService(ILoggingService logginingService,
                                IPointRateRepository pointRateRepository) : base(logginingService)
        {
            _pointRateRepository = pointRateRepository ?? throw new ArgumentNullException(nameof(IPointRateRepository));
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get PointRates Key Value");
            var data = _pointRateRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
        public ResultDto<IList<PointRateDto>> GetAll()
        {
            LogginingService.LogInfo($"Get All PointRates");
            var data = _pointRateRepository.GetAll().ToList();
            return ProcessResult(data.ToDto());
        }

        public ResultDto<IList<PointRateDto>> GetAllFiltered(PointRateFilterDto filter)
        {
            LogginingService.LogInfo($"Get All Point rate Filtered");
            var query = _pointRateRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().MapList(c => c.ToDto()));
        }

        private IQueryable<Entities.Tenant.PointsRate> OrderBy(IQueryable<Entities.Tenant.PointsRate> query, PointRateFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Point Rate");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.UpdateDate);
            return query;
        }

        public ResultDto<PointRateDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id");
            var data = _pointRateRepository.GetById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<PointRateDto> UpdatePointRate(PointRateDto source)
        {
            LogginingService.LogInfo($"Update points rate");
            ResultDto<PointRateDto> result = null;
            var findPointsRate = _pointRateRepository.GetById(source.Id);
            bool exists = false;
            if (findPointsRate != null)
            {
                if (source.Name.Trim().ToLower() != findPointsRate.Name.Trim().ToLower())
                {
                    if (_pointRateRepository.CheckNameExists(source.Name))
                    {
                        exists = true;
                        result = new ResultDto<PointRateDto>()
                        {
                            Errors = new ErrorsDto()
                            {
                                Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists } }
                            },
                            Data = source,
                        };
                    }
                }
                if (!exists)
                {
                    var updatedVehicle = findPointsRate.Update(source);
                    var resultRepository = _pointRateRepository.UpdatePointsRate(updatedVehicle);
                    result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
                }
            }
            return result ?? new ResultDto<PointRateDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeletePointRate(int id)
        {
            LogginingService.LogInfo($"Delete Point Rate by id {id}");
            ResultDto<bool> result = null;
            var vehicle = _pointRateRepository.GetById(id);
            if (vehicle != null)
            {
                var resultSave = _pointRateRepository.DeletePointsRate(vehicle);
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

        public ResultDto<PointRateDto> CreatePointRate(PointRateDto source)
        {
            LogginingService.LogInfo($"Create Point Rate");
            ResultDto<PointRateDto> result = null;
            var newVehicle = source.ToEntity();
            if (!_pointRateRepository.CheckNameExists(source.Name))
            {
                var resultOk = _pointRateRepository.CreatePointsRate(newVehicle);
                return ProcessResult(resultOk.Entity?.ToDto(), resultOk);
            }
            else
            {
                return result ?? new ResultDto<PointRateDto>()
                {
                    Errors = new ErrorsDto()
                    {
                        Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityAlredyExists } }
                    },
                    Data = source
                };
            };
        }

        public ResultDto<ErrorDto> CanDelete(int id)
        {
            var pointRate = _pointRateRepository.GetByIdCanDelete(id);

            ErrorDto result = new ErrorDto() { ErrorMessageKey = string.Empty };

            if (pointRate?.ItemsPointsRate?.Any() == true)
            {
                result.ErrorMessageKey = "PointRateCannotDeleteHaveRelatedItems";
            }
            else if (pointRate?.People?.Any() == true)
            {
                result.ErrorMessageKey = "PointRateCannotDeleteHaveRelatedPeople";
            }          
            else if (pointRate?.ItemsPointsRate?.Any() == true
                    && pointRate?.People?.Any() == true)
            {
                result.ErrorMessageKey = "PointRateCanNotDeleteHaveRelations";
            }

            return ProcessResult(result);
        }
    }
}