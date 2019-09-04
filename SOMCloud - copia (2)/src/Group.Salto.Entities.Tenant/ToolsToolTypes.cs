namespace Group.Salto.Entities.Tenant
{
    public class ToolsToolTypes
    {
        public int ToolId { get; set; }
        public int ToolTypeId { get; set; }

        public Tools Tool { get; set; }
        public ToolsType ToolType { get; set; }
    }
}
