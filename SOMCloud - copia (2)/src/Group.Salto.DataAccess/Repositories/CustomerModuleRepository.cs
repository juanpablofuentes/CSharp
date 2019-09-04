using DataAccess.Common;
using Group.Salto.Entities;
using Group.Salto.Infrastructure.Common.Repository.SOM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Group.Salto.DataAccess.Repositories
{
    public class CustomerModuleRepository : BaseRepository<CustomerModule>, ICustomerModuleRepository
    {
        public CustomerModuleRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<CustomerModule> GetModulesByCustomerId(Guid customerId)
        {
            return Filter(x => x.CustomerId == customerId).Include(cm => cm.Module).ThenInclude(m => m.ModuleActionGroups);
        }
    }
}