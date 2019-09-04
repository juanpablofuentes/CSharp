using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Dtos.BillingRule
{
    public class BillingRuleDto : BillingRuleBaseDto
    {
        public IList<BillingRuleItemDto> Items { get; set; }
    }
}