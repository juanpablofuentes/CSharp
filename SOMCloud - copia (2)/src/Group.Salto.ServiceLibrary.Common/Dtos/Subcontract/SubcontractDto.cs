using System.Collections.Generic;
using Group.Salto.ServiceLibrary.Common.Dtos.People;

namespace Group.Salto.ServiceLibrary.Common.Dtos.SubContract
{
    public class SubContractDto : SubContractBaseDto
    {
        public IList<PeopleSelectableDto> PeopleSelected { get; set; }
        public IList<SubContractKnowledgeDto> KnowledgeSelected { get; set; }
        public int? PurchaseRateId { get; set; }
    }
}
