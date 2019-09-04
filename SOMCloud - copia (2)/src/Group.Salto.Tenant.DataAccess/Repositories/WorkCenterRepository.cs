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
    public class WorkCenterRepository : BaseRepository<WorkCenters>, IWorkCenterRepository
    {
        public WorkCenterRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public WorkCenters GetById(int id)
        {
            var workCenter = this.Find(w => w.Id == id);
            return workCenter;
        }

        public IQueryable<WorkCenters> GetAllAvailablesWithIncludes()
        {
            return this.Filter(w => !w.IsDeleted, GetIncludePeopleAndCompanies());
        }

        public WorkCenters GetByIdWithPeopleCompaniesIncludes(int? id)
        {
            var workCenter = this.Find(w => w.Id == id, GetIncludePeopleAndCompanies());
            return workCenter;
        }

        public SaveResult<WorkCenters> CreateWorkCenter(WorkCenters workCenter)
        {
            workCenter.UpdateDate = DateTime.UtcNow;
            Create(workCenter);
            var result = SaveChange(workCenter);
            return result;
        }

        public SaveResult<WorkCenters> UpdateWorkCenter(WorkCenters workCenter)
        {
            workCenter.UpdateDate = DateTime.UtcNow;
            Update(workCenter);
            var result = SaveChange(workCenter);
            return result;
        }

        public SaveResult<WorkCenters> DeleteWorkCenter(WorkCenters workCenter)
        {
            workCenter.UpdateDate = DateTime.UtcNow;
            Delete(workCenter);
            SaveResult<WorkCenters> result = SaveChange(workCenter);
            result.Entity = workCenter;
            return result;
        }

        public WorkCenters DeleteWorkCenterContext(WorkCenters workCenter)
        {
            workCenter.UpdateDate = DateTime.UtcNow;
            Delete(workCenter);
            return workCenter;
        }

        public Dictionary<int, string> GetActiveByCompanyKeyValue(int companyId)
        {
            var workCenter = this.Filter(w => w.Company.Id == companyId && !w.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
            return workCenter;
        }

        private List<Expression<Func<WorkCenters, object>>> GetIncludePeopleAndCompanies()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(People), typeof(Companies) });
        }

        private List<Expression<Func<WorkCenters, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<WorkCenters, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(People))
                {
                    includesPredicate.Add(p => p.People);
                    includesPredicate.Add(p => p.WorkCenterPeople);
                }
                if (element == typeof(Companies)) includesPredicate.Add(p => p.Company);
            }
            return includesPredicate;
        }        
    }
}