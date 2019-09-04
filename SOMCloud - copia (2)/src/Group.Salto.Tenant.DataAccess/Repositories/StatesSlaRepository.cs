using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class StatesSlaRepository : BaseRepository<StatesSla>, IStatesSlaRepository
    {
        public StatesSlaRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public StatesSla GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public IQueryable<StatesSla> GetAllBySlaId(int id)
        {
            return Filter(x => x.SlaId == id);
        }

        public IQueryable<StatesSla> GetAll()
        {
            return All();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                   .OrderBy(o => o.MinutesToTheEnd)
                   .ToDictionary(t => t.Id, t => t.RowColor);
        }

        public SaveResult<StatesSla> CreateStatesSla(StatesSla sla)
        {
            sla.UpdateDate = DateTime.UtcNow;
            Create(sla);
            var result = SaveChange(sla);
            return result;
        }

        public SaveResult<StatesSla> UpdateStatesSla(StatesSla statesla)
        {
            statesla.UpdateDate = DateTime.UtcNow;
            Update(statesla);
            var result = SaveChange(statesla);
            return result;
        }

        public SaveResult<StatesSla> DeleteStatesSla(StatesSla entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<StatesSla> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public StatesSla DeleteOnContextStatesSla(StatesSla entity)
        {
            Delete(entity);
            return entity;
        }

        public string GetColor(int minutes, int slaId)
        {
            string color = Filter(x => x.SlaId == slaId && minutes < x.MinutesToTheEnd)
            .OrderBy(o => o.MinutesToTheEnd)
            .Select(s => s.RowColor)
            .FirstOrDefault();

            return color;
        }
    }
}