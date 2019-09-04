namespace Group.Salto.ServiceLibrary.Common.Dtos.WorkOrderStatus
{
    public class WorkOrderStatusFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}