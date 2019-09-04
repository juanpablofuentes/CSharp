using Group.Salto.ServiceLibrary.Common.Dtos;
using Group.Salto.ServiceLibrary.Common.Dtos.BillingRule;

namespace Group.Salto.ServiceLibrary.Common.Contracts.BillingRuleItem
{
    public interface IBillingRuleItemService
    {
        ResultDto<BillingRuleItemDto> Create(BillingRuleItemDto billignRuleItem);
        ResultDto<BillingRuleItemDto> Update(BillingRuleItemDto billingRuleItem);
        ResultDto<bool> Delete(int id);
    }
}