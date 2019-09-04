namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderViewConfigurations
{
    public class WorkOrderViewConfigurationsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int UserConfigurationId { get; set; }
    }
}