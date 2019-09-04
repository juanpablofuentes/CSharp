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
    public class FamiliesRepository : BaseRepository<Families>, IFamiliesRepository
    {
        public FamiliesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public IQueryable<Families> GetAll()
        {
            return All();
        }

        public Families GetFamilieWithSubFamilies(int id)
        {
            return Filter(x => x.Id == id).Include(x=>x.SubFamilies).ThenInclude(x=>x.Assets).FirstOrDefault();
        }

        public SaveResult<Families> CreateFamilies(Families families)
        {
            families.UpdateDate = DateTime.UtcNow;
            Create(families);
            var result = SaveChange(families);
            return result;
        }

        public SaveResult<Families> UpdateFamilies(Families families)
        {
            families.UpdateDate = DateTime.UtcNow;
            Update(families);
            var result = SaveChange(families);
            return result;
        }

        public SaveResult<Families> DeleteFamilies(Families entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<Families> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public IQueryable<Families> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<Families> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        private List<Expression<Func<Families, object>>> GetIncludeSubFamilies()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(SubFamilies) });
        }

        private List<Expression<Func<Families, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<Families, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(SubFamilies))
                {
                    includesPredicate.Add(p => p.SubFamilies);
                }
            }
            return includesPredicate;
        }

        private IQueryable<Families> FilterQuery(FilterQueryDto filterQuery, IQueryable<Families> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }
    }
}