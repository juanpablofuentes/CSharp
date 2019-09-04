using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.SubContract
{
    public class SubContractKnowledgeDto : BaseNameIdDto<int>
    {
        public int Priority { get; set; }
    }
}