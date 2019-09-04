using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.Warehouses;
using Group.Salto.SOM.Web.Models.Result;
using Group.Salto.SOM.Web.Models.Warehouses;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class WarehousesViewModelExtensions
    {
        public static IList<WarehouseGeneralViewModel> ToViewModel(this IList<WarehousesBaseDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        } 
        
        public static ResultViewModel<WarehousesBaseViewModel> ToResultViewModel(this ResultDto<WarehousesBaseDto> source) 
        {
            var response = source != null ? new ResultViewModel<WarehousesBaseViewModel>()
            {
                Data = source.Data.ToBaseViewModel(),
                Feedbacks = source.Errors.ToViewModel()
            } : null;
            return response;       
        }

        public static WarehousesBaseViewModel ToBaseViewModel(this WarehousesBaseDto source) 
        {
            WarehousesBaseViewModel result = null;
            if (source != null)
            {
                result = new WarehousesBaseViewModel()
                {
                    GeneralViewModel = source.ToViewModel(),
                    StockViewModel = new WarehouseStockViewModel()
                };                
            }
            return result;       
        }
    }
}