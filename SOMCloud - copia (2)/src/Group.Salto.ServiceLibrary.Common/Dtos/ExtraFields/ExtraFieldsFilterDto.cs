namespace Group.Salto.ServiceLibrary.Common.Dtos.ExtraFields
{
    public class ExtraFieldsFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}