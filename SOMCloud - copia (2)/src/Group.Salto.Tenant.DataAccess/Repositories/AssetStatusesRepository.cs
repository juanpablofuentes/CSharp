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
using System.Text;

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class AssetStatusesRepository : BaseRepository<AssetStatuses>, IAssetStatusesRepository
    {

        public AssetStatusesRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }

        public SaveResult<AssetStatuses> CreateAssetStatuses(AssetStatuses assetStatuses)
        {
            assetStatuses.UpdateDate = DateTime.UtcNow;
            Create(assetStatuses);
            var result = SaveChange(assetStatuses);
            return result;
        }

        public SaveResult<AssetStatuses> UpdateAssetStatuses(AssetStatuses assetStatuses)
        {
            assetStatuses.UpdateDate = DateTime.UtcNow;
            Update(assetStatuses);
            var result = SaveChange(assetStatuses);
            return result;
        }

        public SaveResult<AssetStatuses> DeleteAssetStatuses(AssetStatuses entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Delete(entity);
            SaveResult<AssetStatuses> result = SaveChange(entity);
            result.Entity = entity;
            return result;
        }

        public AssetStatuses SetIsDefault(AssetStatuses assetStatuses)
        {
            var modify = Find(x => x.IsDefault == true);
            if (modify != null)
            {
                modify.IsDefault = false;
                SaveChange(modify);
            }
            assetStatuses.IsDefault = true;
            SaveChange(assetStatuses);
            return assetStatuses;
        }

        public AssetStatuses GetById(int id)
        {
            return Find(x=>x.Id == id);
        }

        public AssetStatuses GetByIdWithTeams(int id)
        {
            return Find(x => x.Id == id,GetIncludeTeams());
        }


        public IQueryable<AssetStatuses> GetAll()
        {
            return All();
        }

        public IQueryable<AssetStatuses> GetAllByFilters(FilterQueryDto filterQuery)
        {
            IQueryable<AssetStatuses> query = All();
            query = FilterQuery(filterQuery, query);
            return query;
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.IsDefault)
                .ToDictionary(t => t.Id, t => t.Name);        
        }
        
        private IQueryable<AssetStatuses> FilterQuery(FilterQueryDto filterQuery, IQueryable<AssetStatuses> query)
        {
            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.Name.Contains(filterQuery.Name));
            }
            return query;
        }

        private List<Expression<Func<AssetStatuses, object>>> GetIncludeTeams()
        {
            return GetIncludesPredicate(new List<Type>() { typeof(Assets) });
        }

        private List<Expression<Func<AssetStatuses, object>>> GetIncludesPredicate(IList<Type> includeCollection)
        {
            var includesPredicate = new List<Expression<Func<AssetStatuses, object>>>();
            foreach (var element in includeCollection)
            {
                if (element == typeof(Assets))
                {
                    includesPredicate.Add(p => p.Assets);
                }
            }
            return includesPredicate;
        }
    }
}