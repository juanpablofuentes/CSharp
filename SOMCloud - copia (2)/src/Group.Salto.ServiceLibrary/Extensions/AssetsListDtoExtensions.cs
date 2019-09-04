using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class AssetsListDtoExtensions
    {
        public static AssetsListDto ToListDto(this Assets source, IList<AssetStatuses> status)
        {
            AssetsListDto result = null;
            if (source != null)
            {
                result = new AssetsListDto();
                result.Id = source.Id;
                result.SerialNumber = source.SerialNumber;
                result.StockNumber = source.StockNumber;
                result.AssetNumber = source.AssetNumber;
                result.AssetStatusId = source.AssetStatusId;
                result.AssetStatusName = status.FirstOrDefault(x => x.Id == source.AssetStatusId).Name;
                result.ProEndDate = source.Guarantee != null && source.Guarantee.ProEndDate.HasValue ? source.Guarantee?.ProEndDate.Value.ToString() : string.Empty;
                result.BlnEndDate = source.Guarantee != null && source.Guarantee.BlnEndDate.HasValue ? source.Guarantee?.BlnEndDate.Value.ToString() : string.Empty;
                result.StdEndDate = source.Guarantee != null && source.Guarantee.StdEndDate.HasValue ? source.Guarantee?.StdEndDate.Value.ToString() : string.Empty;
            }
            return result;
        }

        public static IEnumerable<AssetsListDto> ToListDto(this IEnumerable<Assets> source, IList<AssetStatuses> status)
        {
            return source?.MapList(x => x.ToListDto(status));
        }
    }
}