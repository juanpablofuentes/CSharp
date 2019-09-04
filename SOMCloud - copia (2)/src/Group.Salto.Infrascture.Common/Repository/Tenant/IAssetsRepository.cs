using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IAssetsRepository : IRepository<Assets>
    {
        SaveResult<Assets> UpdateTransferedAsset(List<Assets> entities);
        IQueryable<Assets> GetAssetsByAssetStatusId(int id);
        IQueryable<Assets> GetAssetsBySubFamiliesId(IEnumerable<int> ids);
        IQueryable<Assets> GetAssetBySubFamilieId(int id);
        Dictionary<int, string> GetAllKeyValues();
        //Dictionary<int, string> GetAllByFilters(FilterQueryDto filterQuery);
        IQueryable<Assets> GetAll();
        Assets GetById(int id);
        Assets GetByIdIncludingLocation(int id);
        SaveResult<Assets> CreateAsset(Assets entity);
        SaveResult<Assets> UpdateAsset(Assets entity);
        List<Assets> GetAllFiltered(AssetsFilterRepositoryDto filter);
        Dictionary<int, string> GetAllByFilters(FilterAdditionalQueryDto filterQuery);
        Assets GetLocationsAndUserSiteById(int id);
        Assets GetForWorkOrderEditById(int id);
        List<Assets> GetAllAssetsByLocationFCIds(List<int> IdsToMatch);
    }
}