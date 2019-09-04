using Group.Salto.Common;
using Group.Salto.Common.Entities;
using System.Collections.Generic;

namespace Group.Salto.Entities.Tenant
{
    public class ToolsType : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }

        public ICollection<KnowledgeToolsType> KnowledgeToolsType { get; set; }
        public ICollection<ToolsToolTypes> ToolsToolTypes { get; set; }
        public ICollection<ToolsTypeWorkOrderTypes> ToolsTypeWorkOrderTypes { get; set; }
    }
}
