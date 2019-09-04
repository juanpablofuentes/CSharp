using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.SOM.Web.Models.Assets;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetsFilterViewModelExtensions
    {
        public static AssetsFilterDto ToFilterDto(this AssetsFilterViewModel source)
        {
            AssetsFilterDto result = null;
            if (source != null)
            {
                result = new AssetsFilterDto()
                {
                    SerialNumber = source.SerialNumber,
                    SitesId = source.SitesId,
                    StatusesSelected = source.StatusesSelected.ToAssetStatusesDto(),
                    ModelsSelected = source.ModelsSelected.ToModelsDto(),
                    BrandsSelected = source.BrandsSelected.ToBrandsDto(),
                    FamiliesSelected = source.FamiliesSelected.ToFamiliesDto(),
                    SubFamiliesSelected = source.SubFamiliesSelected.ToSubFamiliesDto(),
                    SitesSelected = source.SitesSelected.ToSitesDto(),
                    FinalClientsSelected = source.FinalClientsSelected.ToFinalClientsDto(),
                    Asc = source.Asc,
                    OrderBy = source.OrderBy,
                };
            }
            return result;
        }

        public static AssetsViewModel ToViewModel(this AssetsListDto source)
        {
            AssetsViewModel result = null;
            if (source != null)
            {
                result = new AssetsViewModel();
                source.ToViewModel(result);
            }

            return result;
        }

        public static void ToViewModel(this AssetsListDto source, AssetsViewModel target)
        {
            if (source != null && target != null)
            {
                target.SerialNumber = source.SerialNumber;
                target.StockNumber = source.StockNumber;
                target.AssetNumber = source.AssetNumber;
                target.AssetStatusId = source.AssetStatusId;
                target.AssetStatusName = source.AssetStatusName;
                target.BlnEndDate = source.BlnEndDate.ToString();
                target.StdEndDate = source.StdEndDate.ToString();
                target.ProEndDate = source.ProEndDate.ToString();
                target.Id = source.Id;
            }
        }

        public static IList<AssetsViewModel> ToListViewModel(this IList<AssetsListDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }
    }
}