using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SubContractRepository : BaseRepository<SubContracts>, ISubContractRepository
    {
        public SubContractRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<SubContracts> GetAll()
        {
            return Filter(x=>!x.IsDeleted);
        }

        public SubContracts GetById(int id)
        {
            return DbSet.Include(x => x.KnowledgeSubContracts)
                        .ThenInclude(x => x.Knowledge)
                            .Include(x => x.People)
                                .ThenInclude(x=>x.Subcontract)
                        .SingleOrDefault(x => x.Id == id && !x.IsDeleted);
        }

        public SaveResult<SubContracts> CreateSubContract(SubContracts entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<SubContracts> UpdateSubContract(SubContracts entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return Filter(x => !x.IsDeleted)
                .Select(s => new { s.Id, s.Name })
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public SaveResult<SubContracts> DeleteSubContractor(SubContracts entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<SubContracts> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }
    }
}