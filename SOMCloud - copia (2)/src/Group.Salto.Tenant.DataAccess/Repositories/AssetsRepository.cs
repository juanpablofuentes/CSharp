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

namespace Group.Salto.DataAccess.Tenant.Repositories
{
    public class AssetsRepository : BaseRepository<Assets>, IAssetsRepository
    {
        public AssetsRepository(ITenantUnitOfWork uow) : base(uow)
        {
        }
        
        public IQueryable<Assets> GetAll()
        {
            return All();
        }
        
        public Assets GetById(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.LocationClient.LocationsFinalClients)
                        .ThenInclude(x => x.FinalClient)
                    .Include(x => x.SubFamily)
                        .ThenInclude(x => x.Family)
                    .Include(x => x.Model)
                        .ThenInclude(x => x.Brand)
                    .Include(x => x.Guarantee)
                    .Include(x => x.AssetsContracts)
                    .Include(x => x.AssetsHiredServices)
                        .ThenInclude(x => x.HiredService)
                    .Include(x => x.User)
                    .Include(x => x.AssetsAudit)
                        .ThenInclude(x => x.AssetsAuditChanges)
                    .FirstOrDefault();
        }

        public Assets GetByIdIncludingLocation(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.Location)
                    .FirstOrDefault();
        }

        public Assets GetForWorkOrderEditById(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.Model)
                        .ThenInclude(x => x.Brand)
                    .Include(x => x.Guarantee)
                    .Include(x => x.AssetStatus)
                    .FirstOrDefault();
        }

        public SaveResult<Assets> CreateAsset(Assets entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Create(entity);
            var result = SaveChange(entity);
            return result;
        }

        public SaveResult<Assets> UpdateAsset(Assets entity)
        {
            entity.UpdateDate = DateTime.UtcNow;
            Update(entity);
            var result = SaveChange(entity);
            return result;
        }

        public List<Assets> GetAllFiltered(AssetsFilterRepositoryDto filter)
        {
            IQueryable<Assets> query = All()
                .Include(wo => wo.Guarantee);

            if (filter.SitesId != 0)
            {
                query = query.Where(x => x.LocationClientId == filter.SitesId);
            }
            if (!string.IsNullOrEmpty(filter.SerialNumber))
            {
                query = query.Where(x => (x.SerialNumber.Contains(filter.SerialNumber)));
            }

            if (filter.StatusesSelected != null)
            {
                query = query.Where(x => filter.StatusesSelected.Contains(x.AssetStatusId));
            }

            if (filter.ModelsSelected != null)
            {
                query = query.Where(x => x.ModelId.HasValue && filter.ModelsSelected.Contains(x.ModelId.Value));
            }

            if (filter.BrandsSelected != null)
            {
                query = query.Where(x => filter.BrandsSelected.Contains(x.Model.BrandId));
            }

            if (filter.SubFamiliesSelected != null)
            {
                query = query.Where(x => x.SubFamilyId.HasValue && filter.SubFamiliesSelected.Contains(x.SubFamilyId.Value));
            }

            if (filter.FamiliesSelected != null)
            {
                query = query.Where(x => filter.FamiliesSelected.Contains(x.SubFamily.FamilyId));
            }

            if (filter.SitesSelected != null)
            {
                query = query.Where(x => x.LocationId.HasValue && filter.SitesSelected.Contains(x.LocationId.Value));
            }

            if (filter.FinalClientsSelected != null)
            {
                query = query.Where(x => x.LocationClient.LocationsFinalClients.Any(lc => filter.FinalClientsSelected.Contains(lc.FinalClientId)));
            }

            return query.ToList();
        }

        public SaveResult<Assets> UpdateTransferedAsset(List<Assets> entities)
        {
            foreach (var asset in entities)
            {
                asset.UpdateDate = DateTime.UtcNow;
                Update(asset);
            }
            var result = SaveChanges();
            return result;
        }

        public IQueryable<Assets> GetAssetsByAssetStatusId(int id)
        {
            return Filter(x => x.AssetStatusId == id);
        }

        public IQueryable<Assets> GetAssetsBySubFamiliesId(IEnumerable<int> ids)
        {
            return Filter(x => ids.Any(k => k == x.SubFamilyId));
        }

        public IQueryable<Assets> GetAssetBySubFamilieId(int id)
        {
            return Filter(x => x.SubFamilyId == id);
        }

        public Dictionary<int, string> GetAllKeyValues()
        {
            return All()
                .OrderBy(o => o.Name)
                .ToDictionary(t => t.Id, t => t.Name);
        }

        public Dictionary<int, string> GetAllByFilters(FilterAdditionalQueryDto filterQuery)
        {
            IQueryable<Assets> query = All();
            query = FilterQuery(filterQuery, query);
            return query.ToDictionary(t => t.Id, t => t.SerialNumber);
        }

        public Assets GetLocationsAndUserSiteById(int id)
        {
            return Filter(x => x.Id == id)
                    .Include(x => x.LocationClient)
                        .ThenInclude(x => x.LocationsFinalClients)
                            .ThenInclude(x => x.FinalClient)
                    .Include(x => x.User)
                    .FirstOrDefault();
        }

        public List<Assets> GetAllAssetsByLocationFCIds(List<int> IdsToMatch) {
            return Filter(y => IdsToMatch.Contains(y.LocationClientId)).ToList();
        }

        private IQueryable<Assets> FilterQuery(FilterAdditionalQueryDto filterQuery, IQueryable<Assets> query)
        {
            if (filterQuery.Id != 0)
            {
                query = query.Where(x => x.LocationClientId == filterQuery.Id);
            }

            if (!string.IsNullOrEmpty(filterQuery.Name))
            {
                query = query.Where(x => x.SerialNumber.Contains(filterQuery.Name) || x.StockNumber.Contains(filterQuery.Name) || x.AssetNumber.Contains(filterQuery.Name) || x.Observations.Contains(filterQuery.Name));
            }
            return query;
        }
    }
}