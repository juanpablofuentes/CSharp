namespace Group.Salto.Entities.Tenant
{
    public class ToolsTypeWorkOrderTypes
    {
        public int WorkOrderTypesId { get; set; }
        public int ToolsTypeId { get; set; }

        public ToolsType ToolsType { get; set; }
        public WorkOrderTypes WorkOrderTypes { get; set; }
    }
}
