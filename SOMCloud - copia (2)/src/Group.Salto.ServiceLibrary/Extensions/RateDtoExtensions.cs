using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.Items;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class RateDtoExtensions
    {
        public static IList<RateDto> ToDto(this IList<ItemsPointsRate> source) 
        {
            return source?.MapList(x => x.ToRateDto());
        }

        public static IList<RateDto> ToDto(this IList<ItemsPurchaseRate> source) 
        {
            return source?.MapList(x => x.ToRateDto());
        }

        public static IList<RateDto> ToDto(this IList<ItemsSalesRate> source) 
        {
            return source?.MapList(x => x.ToRateDto());
        }
        
        public static RateDto ToRateDto(this ItemsPointsRate source) 
        {
            RateDto result = null;
            if (source != null)
            {
                result = new RateDto
                {
                    Id = source.PointsRateId,
                    Name = source.PointsRate.Name,
                    Value = source.Points
                };
            }
            return result;            
        }

        public static RateDto ToRateDto(this ItemsPurchaseRate source) 
        {
            RateDto result = null;
            if (source != null)
            {
                result = new RateDto
                {
                    Id = source.PurchaseRateId,                    
                    Name = source.PurchaseRate.Name,
                    Value = source.Price
                };
            }
            return result;            
        }

        public static RateDto ToRateDto(this ItemsSalesRate source) 
        {
            RateDto result = null;
            if (source != null)
            {
                result = new RateDto
                {
                    Id = source.SalesRateId,                    
                    Name = source.SalesRate.Name,
                    Value = source.Price
                };
            }
            return result;            
        }
    }
}