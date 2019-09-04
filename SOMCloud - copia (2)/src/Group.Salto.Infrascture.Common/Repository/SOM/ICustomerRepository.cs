using System;
using Group.Salto.Common;
using Group.Salto.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.SOM
{
    public interface ICustomerRepository : IRepository<Customers>
    {
        SaveResult<Customers> CreateAndPersist(Customers customer);
        Customers CreateCustomer(Customers customer);
        SaveResult<Customers> UpdateCustomer(Customers customer);
        SaveResult<Customers> PersistChanges(Customers customer);
        IList<Customers> GetTenantIds();
        Customers FindById(Guid id);
        IQueryable<Customers> GetAll();
    }
}
