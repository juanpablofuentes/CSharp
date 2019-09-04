using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ContractsRepository : BaseRepository<Contracts>, IContractsRepository
    {
        public ContractsRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IQueryable<Contracts> GetAll()
        {
            return All().Include(x => x.Client).Where(x => !x.IsDeleted);
        }

        public Contracts GetById(int Id)
        {
            return Filter(x => x.Id == Id && !x.IsDeleted)
                   .Include(x => x.ContractContacts)
                       .ThenInclude(x => x.Contact)
                   .FirstOrDefault();
        }

        public SaveResult<Contracts> CreateContracts(Contracts contracts)
        {
            contracts.UpdateDate = DateTime.UtcNow;
            Create(contracts);
            SaveResult<Contracts> result = SaveChange(contracts);
            result.Entity = contracts;

            return result;
        }

        public SaveResult<Contracts> UpdateContracts(Contracts contracts)
        {
            contracts.UpdateDate = DateTime.UtcNow;
            Update(contracts);
            SaveResult<Contracts> result = SaveChange(contracts);
            result.Entity = contracts;

            return result;
        }

        public bool DeleteContracts(Contracts contracts)
        {
            Delete(contracts);
            SaveResult<Contracts> result = SaveChange(contracts);
            result.Entity = contracts;

            return result.IsOk;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Where(x => !x.IsDeleted)
                .OrderBy(o => o.Object)
                .ToDictionary(t => t.Id, t => t.Object);
        }
    }
}