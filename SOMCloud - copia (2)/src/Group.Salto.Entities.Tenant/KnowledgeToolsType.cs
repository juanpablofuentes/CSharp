namespace Group.Salto.Entities.Tenant
{
    public class KnowledgeToolsType
    {
        public int KnowledgeId { get; set; }
        public int ToolsTypeId { get; set; }

        public Knowledge Knowledge { get; set; }
        public ToolsType ToolsType { get; set; }
    }
}
