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
    public class ModelsRepository : BaseRepository<Models>, IModelsRepository
    {
        public ModelsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Models> FilterById(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.Id));
        }

        public IQueryable<Models> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<Models> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        public Models GetById(int id) 
        {
            return Find(x => x.Id == id);
        }

        private IQueryable<Models> FilterQuery(FilterQueryDto filterQuery, IQueryable<Models> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }

        public Models DeleteOnContextModels(Models entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            return entity;
        }
        
        public IQueryable<Models> FilterByBrand(string filter, int?[] parents) 
        {
            IQueryable<Models> query = Filter(x => x.Name.Contains(filter));
            if (parents?.Length > 0)
            {
                query = query.Where(x => parents.Contains(x.BrandId));
            }
            return query;
        }
    }
}