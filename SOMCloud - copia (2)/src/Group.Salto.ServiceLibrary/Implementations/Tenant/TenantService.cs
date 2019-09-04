using System;
using System.Linq;
using Group.Salto.Common.Cache;
using Group.Salto.Common.Constants;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Customer;
using Group.Salto.ServiceLibrary.Extensions;

namespace Group.Salto.ServiceLibrary.Implementations.Tenant
{
    public class TenantService : BaseService, ITenantService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICache _cacheService;

        public TenantService(ILoggingService logginingService, ICustomerRepository customerRepository, ICache cacheService) : base(logginingService)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException($"{nameof(customerRepository)} is null ");
            _cacheService = cacheService ?? throw new ArgumentNullException($"{nameof(cacheService)} is null");
        }

        public ResultDto<TenantIdsDTO> GetTenant(Guid tenantId)
        {
            LogginingService.LogInfo($"GetTenantIds");
            ResultDto<TenantIdsDTO> result = new ResultDto<TenantIdsDTO>();

            TenantIdsDTO res = (TenantIdsDTO)_cacheService.GetData(AppCache.TenantsIds, tenantId.ToString());
            if (res == null)
            {
                LogginingService.LogVerbose("Saving TenantsIds on Cache");
                var tids = _customerRepository.GetTenantIds().ToTenantIdsDto();
                tids.ToList().ForEach(t =>
                {
                    if (t.Id == tenantId)
                        res = t;
                    _cacheService.SetData(AppCache.TenantsIds, t.Id.ToString(), t);
                });
            }

            result.Data = res;
            return result;
        }
    }
}