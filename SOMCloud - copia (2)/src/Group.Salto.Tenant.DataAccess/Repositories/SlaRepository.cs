using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SlaRepository : BaseRepository<Sla>, ISlaRepository
    {
        public SlaRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Sla GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public IQueryable<Sla> GetAll()
        {
            return All();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                   .OrderBy(o => o.Name)
                   .ToDictionary(t => t.Id, t => t.Name);
        }

        public SaveResult<Sla> CreateSla(Sla sla)
        {
            sla.UpdateDate = DateTime.UtcNow;
            Create(sla);
            var result = SaveChange(sla);
            return result;
        }

        public SaveResult<Sla> UpdateSla(Sla sla)
        {
            sla.UpdateDate = DateTime.UtcNow;
            Update(sla);
            var result = SaveChange(sla);
            return result;
        }

        public SaveResult<Sla> DeleteSla(Sla entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            entity.StatesSla.Clear();
            Delete(entity);
            SaveResult<Sla> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public Sla GetByIdWithStates(int id)
        {
            return Find(x => x.Id == id,GetIncludeStatesSla());
        }

        public IQueryable<Sla> GetByReferenceTimeId(int referenceId)
        {
            return Filter(v => v.MinutesPenaltyWithoutResolution == referenceId,GetIncludeReferenceTime());
        }

        private List<Expression<Func<Sla, object>>> GetIncludeReferenceTime()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(People) });
        }

        private List<Expression<Func<Sla, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Sla, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(ReferenceTimeSla))
                {
                    includesPredicate.Add(p => p.StatesSla);
                }
            }
            return includesPredicate;
        }

        private List<Expression<Func<Sla, object>>> GetIncludeStatesSla()
        {
            return GetIncludesPredicateStates(new List<Type>() { typeof(StatesSla),typeof(WorkOrderCategories),typeof(WorkOrderTypes) });
        }

        private List<Expression<Func<Sla, object>>> GetIncludesPredicateStates(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Sla, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(StatesSla))
                {
                    includesPredicate.Add(p => p.StatesSla);
                }
                if (element == typeof(WorkOrderCategories))
                {
                    includesPredicate.Add(p => p.WorkOrderCategories);
                }
                if (element == typeof(WorkOrderTypes))
                {
                    includesPredicate.Add(p => p.WorkOrderTypes);
                }
            }
            return includesPredicate;
        }
    }
}