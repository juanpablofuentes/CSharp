namespace Group.Salto.ServiceLibrary.Common.Dtos.ExternalWorkOrderStatus
{
    public class ExternalWorkOrderStatusFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}