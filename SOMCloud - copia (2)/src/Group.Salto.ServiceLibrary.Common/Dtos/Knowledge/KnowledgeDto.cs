using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.Knowledge
{
    public class KnowledgeDto : BaseNameIdDto<int>
    {
        public string UpdateDate { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }
    }
}