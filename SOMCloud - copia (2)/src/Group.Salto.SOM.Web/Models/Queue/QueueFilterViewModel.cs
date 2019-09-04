namespace Group.Salto.SOM.Web.Models.Queue
{
    public class QueueFilterViewModel : BaseFilter
    {
        public QueueFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int LanguageId { get; set; }
    }
}