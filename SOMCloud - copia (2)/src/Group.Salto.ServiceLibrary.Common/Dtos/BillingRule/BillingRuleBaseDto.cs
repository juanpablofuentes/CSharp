using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.BillingRule
{
    public class BillingRuleBaseDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Condition { get; set; }
        public BaseNameIdDto<int> ErpSystemInstance { get; set; }
    }
}