namespace Group.Salto.SOM.Web.Models.ExtraFields
{
    public class ExtraFieldsFilterViewModel : BaseFilter
    {
        public ExtraFieldsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public int LanguageId { get; set; }
    }
}