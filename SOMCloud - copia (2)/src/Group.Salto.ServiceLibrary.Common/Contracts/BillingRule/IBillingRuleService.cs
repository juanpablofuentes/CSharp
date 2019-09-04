using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;
using System.Collections.Generic;

namespace Group.Salto.ServiceLibrary.Common.Contracts.BillingRule
{
    public interface IBillingRuleService
    {    
        ResultDto<IList<BillingRuleDto>> GetAllByTaskId(int id);
        ResultDto<BillingRuleBaseDto> Update(BillingRuleBaseDto billingRule);
        ResultDto<BillingRuleBaseDto> Create(BillingRuleBaseDto billignRule);
        ResultDto<bool> Delete(int id);
    }
}