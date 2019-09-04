using System;
using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Group.Salto.Common;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class PermissionsRepository : BaseRepository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(ITenantUnitOfWork uow) : base(uow) { }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .Select(s => new { s.Id, s.Name })
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public IQueryable<Permissions> GetAllById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public IQueryable<Permissions> GetAllNotDeleted()
        {
            //TODO: awaiting confirmation of deleting
            //return Filter(x => !x.IsDeleted);
            return All();
        }

        public Permissions GetByIdNotDeleted(int id)
        {
            return Find(x => x.Id == id, GetAllInclude());
        }

        public SaveResult<Permissions> UpdatePermissions(Permissions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<Permissions> CreatePermissions(Permissions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<Permissions> DeletePermissions(Permissions entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }


        private List<Expression<Func<Permissions, object>>> GetAllInclude()
        {
            return GetIncludesPredicate(new List<Type>() {  typeof(WorkOrderCategoryPermissions),
                                                            typeof(PredefinedServicesPermission),
                                                            typeof(PermissionsTasks),
                                                            typeof(PermissionsQueues),
                                                            typeof(ProjectsPermissions),
                                                            typeof(PeoplePermissions)
            });
        }

        private List<Expression<Func<Permissions, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Permissions, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(WorkOrderCategoryPermissions))
                {
                    includesPredicate.Add(p => p.WorkOrderCategoryPermission);
                }
                if (element == typeof(PredefinedServicesPermission))
                {
                    includesPredicate.Add(p => p.PredefinedServicesPermission);
                }
                if (element == typeof(PermissionsTasks))
                {
                    includesPredicate.Add(p => p.PermissionTask);
                }
                if (element == typeof(PermissionsQueues))
                {
                    includesPredicate.Add(p => p.PermissionQueue);
                }
                if (element == typeof(ProjectsPermissions))
                {
                    includesPredicate.Add(p => p.ProjectPermission);
                }
                if (element == typeof(PeoplePermissions))
                {
                    includesPredicate.Add(p => p.PeoplePermission);
                }
            }
            return includesPredicate;
        }
    }
}