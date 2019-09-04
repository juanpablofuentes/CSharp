using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Common;
using Group.Salto.Common.Helpers;
using Group.Salto.Extensions;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PurchaseRates;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;
using Group.Salto.ServiceLibrary.Common.Dtos.PurchaseRate;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.PurchaseRate
{
    public class PurchaseRateService : BaseService, IPurchaseRateService
    {
        private readonly IPurchaseRateRepository _purchaserateRepository;

        public PurchaseRateService(ILoggingService logginingService, IPurchaseRateRepository purchaseRateRepository) : base(logginingService)
        {
            _purchaserateRepository = purchaseRateRepository ?? throw new ArgumentNullException(nameof(IPurchaseRateRepository));
        }

        public ResultDto<PurchaseRateDetailsDto> CreatePurchaseRate(PurchaseRateDetailsDto source)
        {
            LogginingService.LogInfo($"Create Purchase Rate");
            var newPurchaseRate = source.ToEntity();
            var result = _purchaserateRepository.CreatePurchaseRate(newPurchaseRate);
            return ProcessResult(result.Entity?.ToDetailDto(), result);
        }

        public ResultDto<PurchaseRateDetailsDto> UpdatePurchaseRate(PurchaseRateDetailsDto source)
        {
            LogginingService.LogInfo($"Update purchase rate");
            ResultDto<PurchaseRateDetailsDto> result = null;
            var findPurchaseRate = _purchaserateRepository.GetById(source.Id);
            if (findPurchaseRate != null)
            {
                var updatedPurchaseRate = findPurchaseRate.Update(source);
                var resultRepository = _purchaserateRepository.UpdatePurchaseRate(updatedPurchaseRate);
                result = ProcessResult(resultRepository.Entity?.ToDetailDto(), resultRepository);
            }

            return result ?? new ResultDto<PurchaseRateDetailsDto>()
            {
                Errors = new ErrorsDto()
                {
                    Errors = new List<ErrorDto>() { new ErrorDto() { ErrorType = ErrorType.EntityNotExists } }
                },
                Data = source,
            };
        }

        public ResultDto<bool> DeletePurchaseRate(int id)
        {
            LogginingService.LogInfo($"Delete purchase rate by id {id}");
            ResultDto<bool> result = null;
            var purchaserate = _purchaserateRepository.GetById(id);
            if (purchaserate != null)
            {
                var resultSave = _purchaserateRepository.DeletePurchaseRate(purchaserate);
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

        public ResultDto<PurchaseRateDetailsDto> GetById(int id)
        {
            LogginingService.LogInfo($"Get By Id PurchaseRate");
            var data = _purchaserateRepository.GetById(id);
            return ProcessResult(data.ToDetailDto());
        }

        public IList<BaseNameIdDto<int>> GetBasePurchaseRate()
        {
            var sales = _purchaserateRepository.GetAll();
            return sales.Select(x => new BaseNameIdDto<int>()
            {
                Name = x.Name,
                Id = x.Id,
            }).ToList();
        }

        public ResultDto<IList<PurchaseRateDto>> GetAllFiltered(PurchaseRateFilterDto filter)
        {
            LogginingService.LogInfo($"Get All  Purchase Rate Filtered");
            var query = _purchaserateRepository.GetAll();
            query = query.WhereIfNotDefault(filter.Name, au => au.Name.Contains(filter.Name));
            query = query.WhereIfNotDefault(filter.Description, au => au.Description.Contains(filter.Description));
            var data = query.ToList().MapList(c => c.ToDto()).AsQueryable();
            data = OrderBy(data, filter);
            return ProcessResult<IList<PurchaseRateDto>>(data.ToList());
        }

        private IQueryable<PurchaseRateDto> OrderBy(IQueryable<PurchaseRateDto> query, PurchaseRateFilterDto filter)
        {
            LogginingService.LogInfo($"Order By Purchase Rate");
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Name);
            query = query.MaybeSort(filter.OrderBy, filter.Asc, au => au.Description);
            return query;
        }
    }
}