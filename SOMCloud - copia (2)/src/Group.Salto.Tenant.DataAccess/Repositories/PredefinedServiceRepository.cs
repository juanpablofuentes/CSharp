using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PredefinedServiceRepository : BaseRepository<PredefinedServices>, IPredefinedServiceRepository
    {
        public PredefinedServiceRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<PredefinedServices> GetAll()
        {
            return All();
        }

        public IQueryable<PredefinedServices> GetAllById(IList<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public Dictionary<int, string> GetAllKeyValuesWithProjects()
        {
            return All(GetIncludeProjects()).OrderBy(x => x.Name).ToDictionary(t => t.Id, t => $"{t.Name}-{t.Project.Name}");
        }

        public PredefinedServices GetByIdForCanDelete(int id)
        {
            return Find(x => x.Id == id, GetIncludesForDelete());
        }

        public PredefinedServices DeleteOnContext(PredefinedServices entity)
        {
            Delete(entity);
            return entity;
        }

        public PredefinedServices GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public PredefinedServices GetByIdIncludeExtraFields(int id)
        {
            return Filter(s => s.Id == id).Include(s => s.CollectionExtraField.CollectionsExtraFieldExtraField)
                .ThenInclude(ef => ef.ExtraField).FirstOrDefault();
        }

        private List<Expression<Func<PredefinedServices, object>>> GetIncludeProjects()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(Projects) });
        }

        private List<Expression<Func<PredefinedServices, object>>> GetIncludesForDelete()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(BillingLineItems), typeof(DerivedServices), typeof(Services), typeof(Tasks) });
        }

        private List<Expression<Func<PredefinedServices, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<PredefinedServices, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(Projects))
                {
                    includesPredicate.Add(p => p.Project);
                }
                if (element == typeof(BillingLineItems))
                {
                    includesPredicate.Add(p => p.BillingLineItems);
                }
                if (element == typeof(DerivedServices))
                {
                    includesPredicate.Add(p => p.DerivedServices);
                }
                if (element == typeof(Services))
                {
                    includesPredicate.Add(p => p.Services);
                }
                if (element == typeof(Tasks))
                {
                    includesPredicate.Add(p => p.Tasks);
                }
            }
            return includesPredicate;
        }
    }
}