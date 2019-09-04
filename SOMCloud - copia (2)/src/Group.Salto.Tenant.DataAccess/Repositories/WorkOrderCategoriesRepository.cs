using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class WorkOrderCategoriesRepository : BaseRepository<WorkOrderCategories>, IWorkOrderCategoriesRepository
    {
        public WorkOrderCategoriesRepository(ITenantUnitOfWork uow) : base(uow){ }

        public IQueryable<WorkOrderCategories> GetAll()
        {
            return All();
        }

        public WorkOrderCategories GetById(int id)
        {
            return Filter(x => x.Id == id && !x.IsDeleted)
                  .Include(x => x.TechnicalCodes)
                      .ThenInclude(x => x.PeopleTechnic)
                   .Include(x => x.WorkOrderCategoryPermission)
                   .Include(x => x.WorkOrderCategoryRoles)
                   .Include(x => x.WorkOrderCategoryKnowledge)
                       .ThenInclude(x => x.Knowledge)
                  .FirstOrDefault();
        }

        public WorkOrderCategories GetByIdWithSLA(int id)
        {
            return Filter(x => x.Id == id && !x.IsDeleted)
                  .Include(x => x.Sla)
                  .FirstOrDefault();
        }

        public SaveResult<WorkOrderCategories> CreateWorkOrderCategories(WorkOrderCategories workOrderCategory)
        {
            workOrderCategory.UpdateDate = DateTime.UtcNow;
            Create(workOrderCategory);
            var result = SaveChange(workOrderCategory);
            return result;
        }

        public SaveResult<WorkOrderCategories> UpdateWorkOrderCategories(WorkOrderCategories workOrderCategory)
        {
            workOrderCategory.UpdateDate = DateTime.UtcNow;
            Update(workOrderCategory);
            var result = SaveChange(workOrderCategory);
            return result;
        }

        public SaveResult<WorkOrderCategories> DeleteWorkOrderCategories(WorkOrderCategories workOrderCategory)
        {
            workOrderCategory.UpdateDate = DateTime.UtcNow;
            Delete(workOrderCategory);
            SaveResult<WorkOrderCategories> result = SaveChange(workOrderCategory);
            result.Entity = workOrderCategory;
            return result;
        }

        public IQueryable<WorkOrderCategories> GetAllByName(string name)
        {
            IQueryable<WorkOrderCategories> query = Filter(x => !x.IsDeleted);
            query = FilterQueryByName(query, name);
            return query;
        }

        public List<WorkOrderCategories> GetAllWOCategoriesByProjectIds(List<int> projectsIds)
        {
            return Filter(y => projectsIds.Contains(y.WorkOrderCategoriesCollection.Projects.Select(x => x.Id).FirstOrDefault()))
                .Include(wcc => wcc.WorkOrderCategoriesCollection)
                    .ThenInclude(p => p.Projects).ToList();
        }

        public IQueryable<WorkOrderCategories> GetAllByIds(IList<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id) && !x.IsDeleted);
        }

        public IQueryable<WorkOrderCategories> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery)
        {
            IQueryable<WorkOrderCategories> query = Filter(x => !x.IsDeleted);
            query = FilterQuery(query, filterQuery);
            return query;
        }

        public WorkOrderCategories GetWorkOrderCategoryRelationshipsById(int id)
        {
            var result = Filter(x => x.Id == id && !x.IsDeleted)
               .Include(x => x.WorkOrders)
               .Include(x => x.WorkOrderCategoryRoles)
               .Include(x => x.WorkOrderCategoryKnowledge)
               .Include(x => x.TechnicalCodes)
               .Include(x => x.PreconditionsLiteralValues)
               .Include(x => x.ExternalServicesConfigurationProjectCategories)
               .Include(x => x.ExternalServicesConfiguration)
               .FirstOrDefault();
            return result;
        }

        public Dictionary<int, string> GetAllKeyValuesByWorkOrderCategoriesCollectionId(int id)
        {
            return Filter(x => x.WorkOrderCategoriesCollectionId == id && !x.IsDeleted)
                  .OrderBy(x => x.Name)
                  .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<WorkOrderCategories> FilterByProject(string text, int?[] selected)
        {
            IQueryable<WorkOrderCategories> query = Filter(x => x.Name.Contains(text));
            if (selected?.Length > 0)
            {
                query = query.Where(x => selected.Contains(x.WorkOrderCategoriesCollectionId));
            }
            return query;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Where(x => !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<WorkOrderCategories> GetByIds(IList<int> ids)
        {
            return Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        private IQueryable<WorkOrderCategories> FilterQueryByName(IQueryable<WorkOrderCategories> query, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                query = query.Where(x => (x.Name.Contains(text)));
            }

            return query;
        }

        private IQueryable<WorkOrderCategories> FilterQuery(IQueryable<WorkOrderCategories> query, PermisionsFilterQueryDto filterQuery)
        {
            if (filterQuery.Persmisions != null && filterQuery.Persmisions.Any())
            {
                query = query.Where(x => x.WorkOrderCategoryPermission.Any(p => filterQuery.Persmisions.Contains(p.PermissionId)));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            query = query.OrderBy(x => x.Name);
            return query;
        }

        public IQueryable<WorkOrderCategories> FilterByProject(string text, int? selected)
        { 
            IQueryable<WorkOrderCategories> query = Filter(x => x.Name.Contains(text));
            if (selected.HasValue)
            {
                query = query.Where(x => x.WorkOrderCategoriesCollectionId ==  selected.Value);
            }
            return query;
        }
    }
}