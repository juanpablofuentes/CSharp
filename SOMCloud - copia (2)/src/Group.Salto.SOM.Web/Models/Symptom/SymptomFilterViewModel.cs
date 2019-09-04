namespace Group.Salto.SOM.Web.Models.Symptom
{
    public class SymptomFilterViewModel : BaseFilter
    {
        public SymptomFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}