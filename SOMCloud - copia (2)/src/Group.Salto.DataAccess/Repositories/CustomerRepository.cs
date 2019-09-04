using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Repositories
{
    public class CustomerRepository : BaseRepository<Customers>, ICustomerRepository
    {
        public CustomerRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<Customers> UpdateCustomer(Customers customer)
        {
            customer.UpdateDate = DateTime.UtcNow;
            Update(customer);
            var result = SaveChange(customer);
            result.Entity = customer;
            return result;
        }

        public SaveResult<Customers> CreateAndPersist(Customers customer)
        {
            var contextCustomer = CreateCustomer(customer);
            return PersistChanges(contextCustomer);
        }

        public Customers CreateCustomer(Customers customer)
        {
            customer.UpdateDate = DateTime.UtcNow;
            customer.DateCreated = DateTime.UtcNow;
            Create(customer);
            return customer;
        }

        public SaveResult<Customers> PersistChanges(Customers customer)
        {
            var result = SaveChange(customer);
            result.Entity = customer;
            return result;
        }

        public IList<Customers> GetTenantIds()
        {
            return base.All().ToList();
        }

        public Customers FindById(Guid id)
        {
            return DbSet.Include(x => x.ModulesAssigned)
                        .Include(x=>x.Municipality)
                            .ThenInclude(c=>c.State)
                                .ThenInclude(s=>s.Region)
                                    .ThenInclude(r=>r.Country)
                    .SingleOrDefault(x=>x.Id == id);
        }

        public IQueryable<Customers> GetAll()
        {
            return All();
        }
    }
}
