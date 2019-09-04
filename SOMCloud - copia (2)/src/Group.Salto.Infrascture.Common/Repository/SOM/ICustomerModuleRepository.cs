using Group.Salto.Entities;
using System;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ICustomerModuleRepository
    {
        IQueryable<CustomerModule> GetModulesByCustomerId(Guid customerId);
    }
}