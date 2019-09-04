using Group.Salto.Common.Helpers;
using Group.Salto.Entities.Tenant;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;
using System.Collections.Generic;
using System.Linq;

namespace Group.Salto.ServiceLibrary.Extensions
{
    public static class BillingRuleDtoExtensions
    {
        public static BillingRuleBaseDto ToBaseDto(this BillingRule source) 
        {
            BillingRuleBaseDto result = null;
            if (source != null)
            {
                result = new BillingRuleBaseDto()
                {
                    Id = source.Id,
                    TaskId = source.TaskId,
                    Condition = source.Condition,
                    ErpSystemInstance = new Common.Dtos.Base.BaseNameIdDto<int> 
                    {
                        Id = source.ErpSystemInstanceId,
                        Name = source.ErpSystemInstance?.Name 
                    }                    
                };
            }
            return result;        
        }

        public static BillingRuleDto ToDto(this BillingRule source)
        {
            BillingRuleDto result = null;
            if (source != null)
            {
                result = new BillingRuleDto()
                {
                    Id = source.Id,
                    TaskId = source.TaskId,
                    Condition = source.Condition,
                    ErpSystemInstance = new Common.Dtos.Base.BaseNameIdDto<int> 
                    {
                        Id = source.ErpSystemInstanceId,
                        Name = source.ErpSystemInstance?.Name 
                    },
                    Items = source?.BillingRuleItem?.ToListDto()
                };
            }
            return result;
        }
               
        public static IList<BillingRuleDto> ToListDto(this IList<BillingRule> source)
        {
            return source?.MapList(x => x.ToDto()).ToList();
        }

        public static BillingRule ToEntity(this BillingRuleBaseDto source) 
        {
            BillingRule result = null;
            if (source != null)
            {
                result = new BillingRule()
                {
                    Id = source.Id,
                    TaskId = source.TaskId,
                    Condition = source.Condition,
                    ErpSystemInstanceId = source.ErpSystemInstance.Id,                    
                };
            }
            return result;
        }

        public static void UpdateBillingRule(this BillingRule target, BillingRuleBaseDto source)
        {
            if (source != null && target != null)
            {
                target.Condition = source.Condition;
                target.ErpSystemInstanceId = source.ErpSystemInstance.Id;                
            }
        }
    }
}