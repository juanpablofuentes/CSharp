using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class ProjectRepository : BaseRepository<Projects>, IProjectRepository
    {
        public ProjectRepository(ITenantUnitOfWork uow) : base(uow) { }

        public Projects GetWithPeopleProjectById(int id)
        {
            var query = Filter(x => x.Id == id)
                .Include(x => x.PeopleProjects)
                .Include(x => x.ProjectPermission)
                .Include(x => x.ProjectsContacts)
                    .ThenInclude(x => x.Contact)
                .Include(x => x.TechnicalCodes)
                    .ThenInclude(x => x.PeopleTechnic)
                .Include(x => x.PredefinedServices)
                    .ThenInclude(x => x.PredefinedServicesPermission)
                        .ThenInclude(x => x.Permission)
                .Include(x => x.PredefinedServices)
                    .ThenInclude(x => x.CollectionExtraField);

            return query.FirstOrDefault();
        }

        public Projects GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public Projects GetByIdWithIncludesToDelete(int id)
        {
            return Find(x => x.Id == id, GetIncludesToDelete());
        }

        public Projects GetByIdWithIncludesPermissions(int id)
        {
            return Find(x => x.Id == id, GetIncludesPermissions());
        }

        public Projects GetByIdWithZoneProjectAndContract(int id)
        {
            return Filter(x => x.Id == id)
                .Include(p => p.ZoneProject)
                    .ThenInclude(zp => zp.ZoneProjectPostalCode)
                .Include(p => p.Contract)
                .FirstOrDefault();
        }

        public Projects GetByIdWithZoneProject(int id)
        {
            return Filter(x => x.Id == id)
                .Include(p => p.ZoneProject)
                    .ThenInclude(zp => zp.ZoneProjectPostalCode)
                .FirstOrDefault();
        }

        public Projects GetProjectByProjectName(string ProjectName)
        {
            return Find(x => x.Name == ProjectName);
        }

        public IQueryable<Projects> GetAllWithPeopleProjectsAndPeople()
        {
            return Filter(x => !x.IsDeleted)
                .Include(x => x.Contract)
                .Include(x => x.PeopleProjects)
                    .ThenInclude(x => x.People);
        }

        public List<Projects> GetProjectForAdvancedSearch(FilterQueryDto filterQuery)
        {
            IQueryable<Projects> query = Filter(x => !x.IsDeleted)
                .Include(x => x.PeopleProjects)
                    .ThenInclude(x => x.People)
                .Include(x => x.Contract);

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }

            return query.ToList();
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return Filter(x => x.IsActive && !x.IsDeleted)
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<Projects> GetAllById(IList<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id && !x.IsDeleted));
        }

        public SaveResult<Projects> CreateProjects(Projects project)
        {
            project.UpdateDate = DateTime.UtcNow;
            project.Status = "ACTIU";//TODOO: hasta que se elimine el campo
            Create(project);
            var result = SaveChange(project);
            return result;
        }

        public SaveResult<Projects> UpdateProjects(Projects project)
        {
            project.Status = "ACTIU";//TODOO: hasta que se elimine el campo
            project.UpdateDate = DateTime.UtcNow;
            Update(project);
            var result = SaveChange(project);
            return result;
        }

        public SaveResult<Projects> DeleteProjects(Projects project)
        {
            project.UpdateDate = DateTime.UtcNow;
            Delete(project);
            SaveResult<Projects> result = SaveChange(project);
            result.Entity = project;
            return result;
        }

        public IQueryable<Projects> GetAllByFiltersWithPermisions(PermisionsFilterQueryDto filterQuery)
        {
            IQueryable<Projects> query = Filter(x => !x.IsDeleted);
            query = FilterQuery(query, filterQuery);
            return query;
        }

        public IQueryable<Projects> FilterByClient(string text, int?[] selected)
        {
         IQueryable<Projects> query = Filter(x => x.Name.Contains(text));
            if (selected?.Length > 0)
            {
                query = query.Where(x => selected.Contains(x.Contract.ClientId));
            }
            return query;
        }

        public IQueryable<Projects> GetByIds(IList<int> ids)
        {
            return Filter(x => !x.IsDeleted && ids.Contains(x.Id));
        }

        private IQueryable<Projects> FilterQuery(IQueryable<Projects> query, PermisionsFilterQueryDto filterQuery)
        {
            if (filterQuery.Persmisions != null && filterQuery.Persmisions.Any())
            {
                query = query.Where(x => x.ProjectPermission.Any(p => filterQuery.Persmisions.Contains(p.PermissionId)));
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            query = query.OrderBy(x => x.Name);
            return query;
        }

        private List<Expression<Func<Projects, object>>> GetIncludesToDelete()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(WorkOrders), typeof(WorkOrdersDeritative), typeof(PeopleProjects), typeof(TechnicalCodes),
                typeof(ProjectsCalendars), typeof(PredefinedServices), typeof(ProjectsContacts), typeof(ZoneProject), typeof(PreconditionsLiteralValues), });
        }

        private List<Expression<Func<Projects, object>>> GetIncludesPermissions()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(ProjectsPermissions) });
        }

        private List<Expression<Func<Projects, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Projects, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(WorkOrders))
                {
                    includesPredicate.Add(p => p.WorkOrders);
                }
                if (element == typeof(WorkOrdersDeritative))
                {
                    includesPredicate.Add(p => p.WorkOrdersDeritative);
                }
                if (element == typeof(PeopleProjects))
                {
                    includesPredicate.Add(p => p.PeopleProjects);
                }
                if (element == typeof(TechnicalCodes))
                {
                    includesPredicate.Add(p => p.TechnicalCodes);
                }
                if (element == typeof(ProjectsCalendars))
                {
                    includesPredicate.Add(p => p.ProjectsCalendars);
                }
                if (element == typeof(PredefinedServices))
                {
                    includesPredicate.Add(p => p.PredefinedServices);
                }
                if (element == typeof(ProjectsContacts))
                {
                    includesPredicate.Add(p => p.ProjectsContacts);
                }
                if (element == typeof(ZoneProject))
                {
                    includesPredicate.Add(p => p.ZoneProject);
                }
                if (element == typeof(PreconditionsLiteralValues))
                {
                    includesPredicate.Add(p => p.PreconditionsLiteralValues);
                }
                if (element == typeof(ProjectsPermissions))
                {
                    includesPredicate.Add(p => p.ProjectPermission);
                }
            }
            return includesPredicate;
        }
    }
}