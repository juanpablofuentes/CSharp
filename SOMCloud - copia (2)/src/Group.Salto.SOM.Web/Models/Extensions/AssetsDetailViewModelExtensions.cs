using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Assets;
using Group.Salto.SOM.Web.Models.Assets;
using Group.Salto.SOM.Web.Models.MultiCombo;
using Group.Salto.SOM.Web.Models.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class AssetsDetailViewModelExtensions
    {
        public static ResultViewModel<AssetsDetailViewModel> ToViewModel(this ResultDto<AssetDetailsDto> source)
        {
            var response = source != null ? new ResultViewModel<AssetsDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static AssetsDetailViewModel ToViewModel(this AssetDetailsDto source)
        {
            AssetsDetailViewModel result = null;
            if (source != null)
            {
                result = new AssetsDetailViewModel()
                {
                    GenericDetailViewModel = new GenericDetailViewModel
                    {
                        Id = source.Id,
                        AssetNumber = source.AssetNumber,
                        StockNumber = source.StockNumber,
                        Observations = source.Observations,
                        SerialNumber = source.SerialNumber,
                        StatusId = source.SelectedStatus,
                        ContractId = source.SelectedContract,
                        SiteLocation = new MultiComboViewModel<int, string>
                        {
                            Value = source.SelectedSiteLocation.Key,
                            Text = source.SelectedSiteLocation.Value
                        },
                        Brand = source.SelectedBrand.ToComboViewModel(),
                        Family = source.SelectedFamily.ToComboViewModel(),
                        Model = source.SelectedModel.ToComboViewModel(),
                        SiteClient = source.SiteClient.ToComboViewModel(),
                        SiteUser = source.SelectedSiteUser.ToComboViewModel(),
                        SubFamily = source.SelectedSubFamily.ToComboViewModel(),
                    },
                    SecondaryDetailViewModel = new SecondaryDetailViewModel
                    {
                        HiredServices = source.HiredServices?.Select(x => x.ToViewModel()).ToList(),
                        Warranty = source.Warranty?.ToViewModel()
                    },
                    AssetsAuditViewModel = source.Audits.ToListViewModel()  
                };
            }
            return result;
        }

        public static AssetDetailsDto ToDto(this AssetsDetailViewModel source)
        {
            AssetDetailsDto result = null;
            if (source != null)
            {
                result = new AssetDetailsDto()
                {
                    Id = source.GenericDetailViewModel.Id,
                    UserId = source.UserId,
                    AssetNumber = source.GenericDetailViewModel.AssetNumber,
                    SerialNumber = source.GenericDetailViewModel.SerialNumber,
                    StockNumber = source.GenericDetailViewModel.StockNumber,
                    Observations = source.GenericDetailViewModel.Observations,
                    SelectedStatus = source.GenericDetailViewModel.StatusId,
                    SelectedContract = source.GenericDetailViewModel.ContractId,
                    SelectedSiteLocation = new KeyValuePair<int, string>(source.GenericDetailViewModel.SiteLocation.Value, source.GenericDetailViewModel.SiteLocation.Text),
                    SelectedModel = source.GenericDetailViewModel.Model.ToKeyValuePair(),
                    SelectedSubFamily = source.GenericDetailViewModel.SubFamily.ToKeyValuePair(),
                    SelectedSiteUser = source.GenericDetailViewModel.SiteUser.ToKeyValuePair(),
                    Warranty = source.SecondaryDetailViewModel.Warranty.ToDto(),
                    HiredServices = source.SecondaryDetailViewModel.HiredServices.ToListDto().ToList()
                };
            }
            return result;
        }

        public static MultiComboViewModel<int?, string> ToComboViewModel(this KeyValuePair<int?, string> source)
        { 
            MultiComboViewModel<int?, string> result = null;
            if (!source.Equals(null))
            {
                result = new MultiComboViewModel<int?, string>
                {
                    Value = source.Key,
                    Text = source.Value
                };
            }
            return result;
        }
        
        public static KeyValuePair<int?, string> ToKeyValuePair(this MultiComboViewModel<int?, string> source)
        { 
            KeyValuePair<int?, string> result = new KeyValuePair<int?, string>();
            if (source != null && source.Value > 0)
            {
                result = new KeyValuePair<int?, string>(source.Value, source.Text);
            }
            return result;
        }
    }
}