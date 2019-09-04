namespace Group.Salto.SOM.Web.Models.Calendar
{
    public class CalendarFilterViewModel : BaseFilter
    {
        public CalendarFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}