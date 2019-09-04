using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BillingRulesItemsDtoExtensions
    {
        public static BillingRuleItemDto ToDto(this BillingRuleItem source)
        {
            BillingRuleItemDto result = null;
            if (source != null)
            {
                result = new BillingRuleItemDto()
                {
                    Id = source.Id,
                    BillingRuleId = source.BillingRuleId,
                    Units = source.Units,
                    Item = new Common.Dtos.Base.BaseNameIdDto<int> 
                    {
                        Id = source.ItemId,
                        Name = source.Item?.Name 
                    }
                };
            }
            return result;
        }
               
               
        public static IList<BillingRuleItemDto> ToListDto(this ICollection<BillingRuleItem> source)
        {
            return source?.MapList(x => x.ToDto()).ToList();
        }

        public static BillingRuleItem ToEntity(this BillingRuleItemDto source) 
        {
            BillingRuleItem result = null;
            if (source != null)
            {
                result = new BillingRuleItem()
                {          
                    BillingRuleId = source.BillingRuleId,
                    ItemId = source.Item.Id,                      
                    Units = source.Units,
                };
            }
            return result;
        }

        public static void UpdateBillingRuleItem(this BillingRuleItem target, BillingRuleItemDto source)
        {
            if (source != null && target != null)
            {
                target.ItemId = source.Item.Id;
                target.Units = source.Units;                
            }
        }        
    }
}