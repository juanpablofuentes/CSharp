using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Group.Salto.Log;
using Group.Salto.ServiceLibrary.Common.Contracts.CustomerModules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Implementations.CustomerModules
{
    public class CustomerModuleService : BaseService, ICustomerModuleService
    {
        private readonly ICustomerModuleRepository _customerModuleRepository;

        public CustomerModuleService(ILoggingService logginingService,
            ICustomerModuleRepository customerModuleRepository) : base(logginingService)
        {
            _customerModuleRepository = customerModuleRepository ?? throw new ArgumentNullException(nameof(ICustomerModuleRepository));
        }

        public IList<CustomerModule> GetModulesByCustomer(Guid customerId)
        {
            return _customerModuleRepository.GetModulesByCustomerId(customerId).ToList();
        }
    }
}