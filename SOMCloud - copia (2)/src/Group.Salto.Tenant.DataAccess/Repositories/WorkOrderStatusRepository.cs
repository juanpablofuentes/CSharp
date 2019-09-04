using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Group.Salto.Common;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderStatusRepository : BaseRepository<WorkOrderStatuses>, IWorkOrderStatusRepository
    {
        public WorkOrderStatusRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<WorkOrderStatuses> GetAllWithIncludeTranslations()
        {
            return this.Filter(x => !x.IsDeleted, GetIncludeWorkOrderStatusesTranslations());
        }

        public WorkOrderStatuses GetById(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeWorkOrderStatusesTranslations());
        }

        public WorkOrderStatuses GetByIdWithWorkOrders(int id)
        {
            return this.Find(x => x.Id == id && !x.IsDeleted, GetIncludeWorkOrderStatusesTranslationsAndWorkOrders());
        }

        public IQueryable<WorkOrderStatuses> GetByIds(IList<int> ids)
        {
            return this.Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        public SaveResult<WorkOrderStatuses> CreateWorkOrderStatus(WorkOrderStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<WorkOrderStatuses> UpdateWorkOrderStatus(WorkOrderStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<WorkOrderStatuses> DeleteWorkOrderStatus(WorkOrderStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            return result;
        }

        private List<Expression<Func<WorkOrderStatuses, object>>> GetIncludeWorkOrderStatusesTranslations()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(WorkOrderStatusesTranslations)});
        }

        private List<Expression<Func<WorkOrderStatuses, object>>> GetIncludeWorkOrderStatusesTranslationsAndWorkOrders()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(WorkOrderStatusesTranslations), typeof(WorkOrders) });
        }

        private List<Expression<Func<WorkOrderStatuses, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<WorkOrderStatuses, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(WorkOrderStatusesTranslations))
                {
                    includesPredicate.Add(p => p.WorkOrderStatusesTranslations);
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