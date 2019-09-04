using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class Knowledge : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }

        public ICollection<BasicTechnicianListFilterSkills> BasicTechnicianListFilterSkills { get; set; }
        public ICollection<KnowledgePeople> KnowledgePeople { get; set; }
        public ICollection<KnowledgeSubContracts> KnowledgeSubContracts { get; set; }
        public ICollection<KnowledgeToolsType> KnowledgeToolsType { get; set; }
        public ICollection<KnowledgeWorkOrderTypes> KnowledgeWorkOrderTypes { get; set; }
        public ICollection<WorkOrderCategoryKnowledge> WorkOrderCategoryKnowledge { get; set; }
    }
}
