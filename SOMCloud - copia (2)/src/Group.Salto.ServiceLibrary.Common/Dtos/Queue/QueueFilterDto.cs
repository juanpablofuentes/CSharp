namespace Group.Salto.ServiceLibrary.Common.Dtos.Queue
{
    public class QueueFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}