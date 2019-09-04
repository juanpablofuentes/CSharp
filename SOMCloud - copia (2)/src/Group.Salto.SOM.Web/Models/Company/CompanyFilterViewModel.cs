namespace Group.Salto.SOM.Web.Models.Company
{
    public class CompanyFilterViewModel : BaseFilter
    {
        public CompanyFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
    }
}
