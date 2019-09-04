using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.SalesRate;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.SalesRate;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.SalesRate
{
    public class SalesRateService : BaseService, ISalesRateService
    {
        private readonly ISalesRateRepository _salesRateRepository;

        public SalesRateService(ILoggingService logginingService, ISalesRateRepository salesRateRepository) : base(logginingService)
        {
            _salesRateRepository = salesRateRepository ?? throw new ArgumentNullException(nameof(ISalesRateRepository));
        }

        public IList<BaseNameIdDto<int>> GetBaseSalesRates()
        {
            var sales = _salesRateRepository.GetAllNotDeleted();
            return sales.Select(x => new BaseNameIdDto<int>()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }

        public ResultDto<SalesRateBaseDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id Sales rate");
            var data = _salesRateRepository.GetById(id);
            return ProcessResult(data.ToDto());
        }

        public ResultDto<IList<SalesRateBaseDto>> GetAllFiltered(SalesRateFilterDto filter)
        {
            LogginingService.LogInfo($"Get salesrate filtered");
            var query = _salesRateRepository.GetAllNotDeleted();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.ErpReference.Contains(filter.Description));
            query = OrderBy(query, filter);
            return ProcessResult(query.ToList().ToDto());
        }

        private IQueryable<Entities.Tenant.SalesRate> OrderBy(IQueryable<Entities.Tenant.SalesRate> query, SalesRateFilterDto filter)
        {
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }           

        public ResultDto<SalesRateBaseDto> Create(SalesRateBaseDto model)
        {
            LogginingService.LogInfo($"Create SalesRate");
            var newSalesRate = model.ToEntity();
            var result = _salesRateRepository.CreateSalesRate(newSalesRate);
            return ProcessResult(result.Entity?.ToDto(), result);
        }

        public ResultDto<bool> Delete(int id)
        {
            LogginingService.LogInfo($"Delete salesRate by id {id}");
            ResultDto<bool> result = null;
            var vehicle = _salesRateRepository.GetByIdNotDeleted(id);
            if (vehicle != null)
            {
                var resultSave = _salesRateRepository.DeleteSalesRate(vehicle);
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

        public ResultDto<SalesRateBaseDto> Update(SalesRateBaseDto model)
        {
            LogginingService.LogInfo($"Update SalesRate");
            ResultDto<SalesRateBaseDto> result = null;
            var findSalesRate = _salesRateRepository.GetByIdNotDeleted(model.Id);
            if (findSalesRate != null)
            {
                var updatedSalesRate = findSalesRate.Update(model);
                var resultRepository = _salesRateRepository.UpdateSalesRate(updatedSalesRate);
                result = ProcessResult(resultRepository.Entity?.ToDto(), resultRepository);
            }
            return result ?? new ResultDto<SalesRateBaseDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = model
            };
        }
    }
}