using Group.Salto.Common.Constants.TenantConfiguration;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.TenantConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.TenantConfiguration
{
    public class TenantConfigurationService : BaseService, ITenantConfigurationService
    {
        private ITenantConfigurationRepository _tenantConfigurationRepository;

        public TenantConfigurationService(ILoggingService logginingService, ITenantConfigurationRepository tenantConfigurationRepository)
            : base(logginingService)
        {
            _tenantConfigurationRepository = tenantConfigurationRepository ?? throw new ArgumentNullException($"{nameof(tenantConfigurationRepository)} is null ");
        }

        public IEnumerable<string> GetValueByGroup(string group)
        {
            LogginingService.LogInfo($"TenantConfigurationService->GetValueByGroup");
            IEnumerable<string> result = _tenantConfigurationRepository.GetByGroup(TenantConfigurationConstants.NumberEntriesPerPage).Select(x => x.Value);
            return result;
        }
    }
}