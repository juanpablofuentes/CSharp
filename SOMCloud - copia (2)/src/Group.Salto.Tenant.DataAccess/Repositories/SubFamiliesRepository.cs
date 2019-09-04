using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class SubFamiliesRepository : BaseRepository<SubFamilies>, ISubFamiliesRepository
    {
        public SubFamiliesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<SubFamilies> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<SubFamilies> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        private IQueryable<SubFamilies> FilterQuery(FilterQueryDto filterQuery, IQueryable<SubFamilies> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Nom.Contains(filterQuery.Name));
            }
            return query;
        }

        public SubFamilies GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public SubFamilies DeleteOnContextSubFamilie(SubFamilies entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            return entity;
        }

        public IQueryable<SubFamilies> FilterByClientSite(string filter, int?[] parents) 
        {
            IQueryable<SubFamilies> query = Filter(x => x.Nom.Contains(filter));
            if (parents?.Length > 0)
            {
                query = query.Where(x => parents.Contains(x.FamilyId));
            }
            return query;
        }
    }
}