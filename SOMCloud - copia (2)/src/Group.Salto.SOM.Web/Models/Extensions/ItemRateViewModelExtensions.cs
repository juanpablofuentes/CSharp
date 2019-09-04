using Group.Salto.Common.Helpers;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using Group.Salto.SOM.Web.Models.Items;
using System.Collections.Generic;

namespace Group.Salto.SOM.Web.Models.Extensions
{
    public static class ItemRateViewModelExtensions
    {
        public static ItemPointsViewModel ToViewModel(this ItemPointsViewModel target, IList<RateDto> source, IList<RateDto> otherSource) 
        {
            if (source != null && target != null)
            {
                target.PointsRates = source.ToListViewModel();
                target.OtherPointsRates = otherSource.ToListViewModel();
            }
            return target;       
        }

        public static ItemPurchasesViewModel ToViewModel(this ItemPurchasesViewModel target, IList<RateDto> source, IList<RateDto> otherSource) 
        {
            if (source != null && target != null)
            {
                target.PurchaseRates = source.ToListViewModel();
                target.OtherPurchasesRates = otherSource.ToListViewModel();
            }
            return target;       
        }

        public static ItemSalesViewModel ToViewModel(this ItemSalesViewModel target, IList<RateDto> source, IList<RateDto> otherSource) 
        {
            if (source != null && target != null)
            {
                target.SalesRates = source.ToListViewModel();
                target.OtherSalesRates = otherSource.ToListViewModel(); 
            }
            return target;       
        }

        public static RateViewModel ToViewModel(this RateDto source)
        {
            RateViewModel result = null;
            if (source != null)
            {
                result = new RateViewModel
                {
                    Id = source.Id,
                    Name = source.Name,
                    Value = source.Value
                };
            }
            return result;
        }

        public static IList<RateViewModel> ToListViewModel(this IList<RateDto> source)
        {
            return source?.MapList(x => x.ToViewModel());
        }

        public static IList<RateDto> ToListDto(this IList<RateViewModel> source)
        {
            return source?.MapList(x => x.ToDto());
        }

        public static RateDto ToDto(this RateViewModel source)
        {
            RateDto result = null;
            if (source != null && source.Value != null)
            {
                result = new RateDto
                {
                    Id = source.Id,
                    Name = source.Name,
                    Value = source.Value ?? 0
                };
            }
            return result;
        }
    }
}