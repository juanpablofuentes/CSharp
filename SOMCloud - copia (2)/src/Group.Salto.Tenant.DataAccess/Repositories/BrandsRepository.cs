using DataAccess.Common;
using DataAccess.Common.UoW;
using Group.Salto.Common;
using Group.Salto.Common.Enums;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using Group.Salto.Infrastructure.Common.Repository.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class BrandsRepository : BaseRepository<Brands>, IBrandsRepository
    {

        public BrandsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public Brands GetById(int id)
        {
            return Find(x => x.Id == id);
        }

        public IQueryable<Brands> GetAll()
        {
            return All();
        }
        public Brands GetBrandsWithModels(int id)
        {
            return Find(x=>x.Id == id, GetIncludeModels());
        }

        public SaveResult<Brands> CreateBrands(Brands brands)
        {
            brands.UpdateDate = DateTime.UtcNow;
            Create(brands);
            var result = SaveChange(brands);
            return result;
        }

        public SaveResult<Brands> UpdateBrands(Brands brands)
        {
            brands.UpdateDate = DateTime.UtcNow;
            Update(brands);
            var result = SaveChange(brands);
            return result;
        }

        public SaveResult<Brands> DeleteBrands(Brands entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<Brands> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        private List<Expression<Func<Brands, object>>> GetIncludeModels()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(Models) });
        }

        private List<Expression<Func<Brands, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Brands, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(Models))
                {
                    includesPredicate.Add(p => p.Models);
                }
            }
            return includesPredicate;
        }

        public IQueryable<Brands> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<Brands> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        private IQueryable<Brands> FilterQuery(FilterQueryDto filterQuery, IQueryable<Brands> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }
    }
}