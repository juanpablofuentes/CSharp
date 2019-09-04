namespace Group.Salto.SOM.Web.Models
{
    public class CustomerFilterViewModel : BaseFilter
    {
        public CustomerFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}
