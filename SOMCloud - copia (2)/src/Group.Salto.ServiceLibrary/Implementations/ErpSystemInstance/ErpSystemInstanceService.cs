using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.ErpSystemInstance;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.ErpSystemInstance
{
    public class ErpSystemInstanceService : BaseService, IErpSystemInstanceService
    {
        private readonly IErpSystemInstanceRepository _erpSystemInstanceRepository;

        public ErpSystemInstanceService(ILoggingService logginingService,
                                             IErpSystemInstanceRepository erpSystemInstanceRepository) : base(logginingService)
        {
            _erpSystemInstanceRepository = erpSystemInstanceRepository ?? throw new ArgumentNullException(nameof(IErpSystemInstanceRepository));
        }
        
        public IList<BaseNameIdDto<int>> GetAllKeyValues()
        {
            LogginingService.LogInfo($"Get ErpSystemInstance Key Value");
            var data = _erpSystemInstanceRepository.GetAllKeyValues();

            return data.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
        }
    }
}