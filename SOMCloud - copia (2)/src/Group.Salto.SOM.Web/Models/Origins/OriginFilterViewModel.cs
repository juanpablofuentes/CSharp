namespace Group.Salto.SOM.Web.Models.Origins
{
    public class OriginsFilterViewModel : BaseFilter
    {
        public OriginsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Observations { get; set; }
    }
}
