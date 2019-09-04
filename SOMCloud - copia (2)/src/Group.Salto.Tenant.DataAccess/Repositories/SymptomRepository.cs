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
    public class SymptomRepository : BaseRepository<Symptom>, ISymptomRepository
    {
        public SymptomRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Symptom> GetAll()
        {
            return All();
        }

        public Symptom GetById(int id) 
        {
            return Filter(x=>x.Id == id)
                .Include(x => x.SymptomCollectionSymptoms)
                .FirstOrDefault();        
        }
        
        public SaveResult<Symptom> CreateSymptom(Symptom entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result; 
        }

        public SaveResult<Symptom> UpdateSymptom(Symptom entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Symptom> DeleteSymptom(Symptom entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<Symptom> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public IQueryable<Symptom> GetAllById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));            
        }

        public Dictionary<int, string> GetOrphansKeyValue() 
        {
            return this.Filter(x => x.SymptomFatherId == null)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }
    }
}