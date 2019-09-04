using Group.Salto.Entities;
using System;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.CustomerModules
{
    public interface ICustomerModuleService
    {
        IList<CustomerModule> GetModulesByCustomer(Guid customerId);
    }
}