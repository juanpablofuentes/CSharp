using Group.Salto.Common;
using Group.Salto.Entities.Tenant;
using Group.Salto.Infrastructure.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.Infrastructure.Common.Repository.Tenant
{
    public interface IAssetStatusesRepository
    {
        SaveResult<AssetStatuses> CreateAssetStatuses(AssetStatuses assetStatuses);
        SaveResult<AssetStatuses> UpdateAssetStatuses(AssetStatuses assetStatuses);
        AssetStatuses GetById(int id);
        AssetStatuses GetByIdWithTeams(int id);
        SaveResult<AssetStatuses> DeleteAssetStatuses(AssetStatuses entity);
        AssetStatuses SetIsDefault(AssetStatuses assetStatuses);
        IQueryable<AssetStatuses> GetAll();
        IQueryable<AssetStatuses> GetAllByFilters(FilterQueryDto filterQuery);
        Dictionary<int, string> GetAllKeyValues();
    }
}