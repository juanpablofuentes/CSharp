using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.BillingRule
{
    public class BillingRuleItemDto
    {
        public int Id { get; set; }
        public string Units { get; set; }
        public BaseNameIdDto<int> Item { get; set; }
        public int BillingRuleId { get; set; }
    }
}