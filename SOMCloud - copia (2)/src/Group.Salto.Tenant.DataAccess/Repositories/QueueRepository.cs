using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class QueueRepository : BaseRepository<Queues>, IQueueRepository
    {
        public QueueRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Queues> GetAllWithIncludeTranslations()
        {
            return this.Filter(x => !x.IsDeleted, GetIncludeQueuesTranslations());
        }

        public IQueryable<Queues> GetAllById(IList<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public Queues GetById(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeQueuesAndPermisionsTranslations());
        }

        public Queues GetByIdWithWorkOrders(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeQueuesTranslationsAndWorkOrders());
        }

        public IQueryable<Queues> GetByIds(IList<int> ids)
        {
            return Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        public SaveResult<Queues> UpdateQueue(Queues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Queues> CreateQueue(Queues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Queues> DeleteQueue(Queues entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            return result;
        }

        public IQueryable<Queues> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery)
        {
            IQueryable<Queues> query = GetAllWithIncludeTranslations();
            query = FilterQuery(query, filterQuery);
            return query;
        }
    
        private IQueryable<Queues> FilterQuery(IQueryable<Queues> query, PermisionsFilterQueryDto filterQuery)
        {
            if (filterQuery.Persmisions != null && filterQuery.Persmisions.Any())
            {
                query = query.Where(x => x.PermissionQueue.Any(p => filterQuery.Persmisions.Contains(p.PermissionId)));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            query = query.OrderBy(x => x.Name);
            return query;
        }

        private List<Expression<Func<Queues, object>>> GetIncludeQueuesTranslations()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(QueuesTranslations) });
        }

        private List<Expression<Func<Queues, object>>> GetIncludeQueuesAndPermisionsTranslations()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(QueuesTranslations), typeof(Permissions) });
        }

        private List<Expression<Func<Queues, object>>> GetIncludeQueuesTranslationsAndWorkOrders()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(QueuesTranslations), typeof(WorkOrders) });
        }

        private List<Expression<Func<Queues, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Queues, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(QueuesTranslations))
                {
                    includesPredicate.Add(p => p.QueuesTranslations);
                }
                if (element == typeof(WorkOrders))
                {
                    includesPredicate.Add(p => p.WorkOrders);
                }
                if (element == typeof(Permissions))
                {
                    includesPredicate.Add(p => p.PermissionQueue);
                }
            }
            return includesPredicate;
        }
    }
}