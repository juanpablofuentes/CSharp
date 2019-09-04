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
    public class SymptomCollectionRepository : BaseRepository<SymptomCollections>, ISymptomCollectionRepository
    {
        public SymptomCollectionRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<SymptomCollections> GetAll()
        {
            return Filter(x=>!x.IsDeleted);
        }
        
        public SymptomCollections GetById(int id) 
        {
            var result = Filter(x => x.Id == id && !x.IsDeleted)
                .Include(x => x.SymptomCollectionSymptoms)                    
                .SingleOrDefault();
            return result;        
        }
        
        public SaveResult<SymptomCollections> CreateSymptomCollection(SymptomCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result; 
        }

        public SaveResult<SymptomCollections> UpdateSymptomCollection(SymptomCollections entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<SymptomCollections> DeleteSymptomCollection(SymptomCollections entity) 
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<SymptomCollections> result = SaveChange(entity);
            result.Entity = entity;
            return result;           
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return Filter(x => !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}