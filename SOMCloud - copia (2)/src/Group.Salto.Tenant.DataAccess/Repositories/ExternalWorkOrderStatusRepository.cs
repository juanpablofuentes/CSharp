using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ExternalWorkOrderStatusRepository : BaseRepository<ExternalWorOrderStatuses>, IExternalWorkOrderStatusRepository
    {
        public ExternalWorkOrderStatusRepository(ITenantUnitOfWork uow) : base(uow)
        {

        }

        public IQueryable<ExternalWorOrderStatuses> GetAllWithIncludeTranslations()
        {
            return this.Filter(x => !x.IsDeleted, GetIncludeWorkOrderStatusesTranslations());
        }
        public ExternalWorOrderStatuses GetById(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeWorkOrderStatusesTranslations());
        }

        public ExternalWorOrderStatuses GetByIdWithWorkOrders(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeWorkOrderStatusesTranslationsAndWorkOrders());
        }

        public IQueryable<ExternalWorOrderStatuses> GetByIds(IList<int> ids)
        {
            return Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        public SaveResult<ExternalWorOrderStatuses> UpdateExternalWorkOrderStatus(ExternalWorOrderStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<ExternalWorOrderStatuses> CreateExternalWorkOrderStatus(ExternalWorOrderStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<ExternalWorOrderStatuses> DeleteExternalWorkOrderStatus(ExternalWorOrderStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            return result;
        }

        private List<Expression<Func<ExternalWorOrderStatuses, object>>> GetIncludeWorkOrderStatusesTranslationsAndWorkOrders()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(ExternalWorkOrderStatusesTranslations), typeof(WorkOrders) });
        }

        private List<Expression<Func<ExternalWorOrderStatuses, object>>> GetIncludeWorkOrderStatusesTranslations()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(ExternalWorkOrderStatusesTranslations) });
        }

        private List<Expression<Func<ExternalWorOrderStatuses, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<ExternalWorOrderStatuses, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(ExternalWorkOrderStatusesTranslations))
                {
                    includesPredicate.Add(p => p.ExternalWorkOrderStatusesTranslations);
                }
                if (element == typeof(WorkOrders))
                {
                    includesPredicate.Add(p => p.WorkOrders);
                }
            }
            return includesPredicate;
        }
    }
}