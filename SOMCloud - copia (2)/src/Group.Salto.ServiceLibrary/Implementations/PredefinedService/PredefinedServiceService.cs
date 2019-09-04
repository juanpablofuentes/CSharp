using System;
using System.Collections.Generic;
using System.Linq;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.PredefinedServices;
using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Implementations.PredefinedService
{
    public class PredefinedServiceService : BaseService, IPredefinedServiceService
    {
        private readonly IPredefinedServiceRepository _predefinedServiceRepository;

        public PredefinedServiceService(ILoggingService logginingService, IPredefinedServiceRepository predefinedServiceRepository) : base(logginingService)
        {
            _predefinedServiceRepository = predefinedServiceRepository ?? throw new ArgumentNullException($"{nameof(IPredefinedServiceRepository)} is null");
        }

        public bool CanDelete(int id)
        {
            LogginingService.LogInfo($"Get PredefinedServices CanDelete");
            var data = _predefinedServiceRepository.GetByIdForCanDelete(id);
            if (data != null)
            { 
                if (data.BillingLineItems.Any() || data.DerivedServices.Any() || data.Services.Any() || data.Tasks.Any())
                {
                    return false;
                }
            }
            return true;
        }

        public IList<BaseNameIdDto<int>> GetAllKeyValue()
        {
            LogginingService.LogInfo($"Get PredefinedServices Key Value");
            var data = _predefinedServiceRepository.GetAllKeyValuesWithProjects();
            var result = data?.Select(x => new BaseNameIdDto<int>()
            {
                Id = x.Key,
                Name = x.Value,
            }).ToList();
            return result;
        }
    }
}