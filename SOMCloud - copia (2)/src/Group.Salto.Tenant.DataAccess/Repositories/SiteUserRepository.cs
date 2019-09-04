using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SiteUserRepository : BaseRepository<SiteUser>, ISiteUserRepository
    {
        public SiteUserRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<SiteUser> GetAll(int Id)
        {
            IQueryable<SiteUser> query = All();
            if (Id != 0)
            {
                query = query.Where(x => x.LocationId == Id);
            }

            return query;
        }

        public SiteUser GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SiteUser GetByIdIncludeReferencesToDelete(int id)
        {
            return Find(x => x.Id == id, GetIncludeDeleteRefenreces());
        }

        public IQueryable<SiteUser> FilterByClientSite(string text, int?[] selected)
        {
            IQueryable<SiteUser> query = Filter(x => x.Name.Contains(text));
            if (selected?.Length > 0)
            {
                query = query.Where(x => selected.Contains(x.LocationId));
            }
            return query;
        }

        public SaveResult<SiteUser> UpdateSiteUser(SiteUser entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public SaveResult<SiteUser> CreateSiteUser(SiteUser entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SiteUser GetSiteUserRelationshipsById(int id)
        {
            return Filter(p => p.Id == id)
                .Include(p => p.WorkOrders)
                .Include(p => p.WorkOrdersDeritative)
                .FirstOrDefault();
        }

        public bool DeleteSiteUser(SiteUser siteUser)
        {
            Delete(siteUser);
            SaveResult<SiteUser> result = SaveChange(siteUser);
            result.Entity = siteUser;
            return result.IsOk;
        }

        private List<Expression<Func<SiteUser, object>>> GetIncludeDeleteRefenreces()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(Assets),
                                                            typeof(WorkOrdersDeritative),
                                                            typeof(WorkOrders)});
        }

        private List<Expression<Func<SiteUser, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<SiteUser, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(Assets))
                {
                    includesPredicate.Add(p => p.Assets);
                }
                if (element == typeof(WorkOrders))
                {
                    includesPredicate.Add(p => p.WorkOrders);
                }
                if (element == typeof(WorkOrdersDeritative))
                {
                    includesPredicate.Add(p => p.WorkOrdersDeritative);
                }
            }
            return includesPredicate;
        }
    }
}