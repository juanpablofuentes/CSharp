using Group.Salto.ServiceLibrary.Common.Dtos.Base;

namespace Group.Salto.ServiceLibrary.Common.Dtos.People
{
    public class PeopleKnowledgeDto : BaseNameIdDto<int>
    {
        public int Priority { get; set; }
    }
}