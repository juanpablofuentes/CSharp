using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.SOM.Web.Models.Items;
using Group.Salto.SOM.Web.Models.Result;
using System;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ItemsDetailViewModelExtensions
    {
        public static ItemsDetailsDto ToDto(this ItemsDetailViewModel source)
        {
            ItemsDetailsDto result = null;
            if (source != null)
            {
                result = new ItemsDetailsDto()
                {
                    Id = source.GeneralViewModel.Id,
                    Name = source.GeneralViewModel.Name,
                    Description = source.GeneralViewModel.Description,
                    ERPReference = source.GeneralViewModel.ERPReference,
                    InternalReference = source.GeneralViewModel.InternalReference,
                    InDepot = source.GeneralViewModel.InDepot,
                    IsBlocked = source.GeneralViewModel.IsBlocked,
                    SyncErp = source.GeneralViewModel.SyncErp,
                    TrackSerialNumber = source.GeneralViewModel.TrackSerialNumber,
                    ItemsTypeId = source.GeneralViewModel.ItemsTypeId,                   
                    SelectedSubFamily = source.GeneralViewModel.SubFamily.ToKeyValuePair(),
                    SelectedFamily = source.GeneralViewModel.Family.ToKeyValuePair(),
                    ItemPointsRates = source.PointsViewModel?.PointsRates?.ToListDto(),
                    AvailablePointsRates = source.PointsViewModel?.OtherPointsRates?.ToListDto(),
                    ItemPurchaseRates = source.PurchasesViewModel?.PurchaseRates?.ToListDto(),
                    AvailablePurchaseRates = source.PurchasesViewModel?.OtherPurchasesRates?.ToListDto(),
                    ItemSalesRates = source.SalesViewModel?.SalesRates?.ToListDto(),
                    AvailableSalesRates = source.SalesViewModel?.OtherSalesRates?.ToListDto(),
                    ItemsSerialNumbers = source.TrackingViewModel?.SerialNumbers?.ToListDto()
                };
            }
            return result;
        }

        public static ResultViewModel<ItemsDetailViewModel> ToResultViewModel(this ResultDto<ItemsDetailsDto> source) 
        {
            var response = source != null ? new ResultViewModel<ItemsDetailViewModel>()
            {
                Data = source.Data.ToViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;
        }

        public static ItemsDetailViewModel ToViewModel(this ItemsDetailsDto source) 
        {
            ItemsDetailViewModel result = null;
            if (source != null)
            {
                result = new ItemsDetailViewModel()
                {
                    GeneralViewModel = source.ToGeneralViewModel(),
                    PointsViewModel = new ItemPointsViewModel(),
                    PurchasesViewModel = new ItemPurchasesViewModel(),
                    SalesViewModel = new ItemSalesViewModel(),
                    TrackingViewModel = new SerialNumbersTrackingViewModel()
                };
                result.PointsViewModel.ToViewModel(source.ItemPointsRates,source.AvailablePointsRates);
                result.PurchasesViewModel.ToViewModel(source.ItemPurchaseRates, source.AvailablePurchaseRates);
                result.SalesViewModel.ToViewModel(source.ItemSalesRates, source.AvailableSalesRates);
                result.TrackingViewModel.SerialNumbers = source.ItemsSerialNumbers.ToListViewModel();
            }
            return result;       
        }

        private static ItemGeneralViewModel ToGeneralViewModel(this ItemsDetailsDto source) 
        {
            ItemGeneralViewModel result = null;
            if (source != null)
            {
                result = new ItemGeneralViewModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    ERPReference = source.ERPReference,
                    InternalReference = source.InternalReference,
                    InDepot = source.InDepot,
                    IsBlocked = source.IsBlocked,
                    SyncErp = source.SyncErp,
                    TrackSerialNumber = source.TrackSerialNumber,
                    ItemsTypeId = source.ItemsTypeId,
                    Family = source.SelectedFamily.ToComboViewModel(),
                    SubFamily = source.SelectedSubFamily.ToComboViewModel()
                };
                var thumbnail = (source.Picture != null) ? Convert.ToBase64String(source.Picture) : string.Empty;
                result.Thumbnail = string.Format("data:image/gif;base64,{0}", thumbnail);
            }
            return result;       
        }
    }
}