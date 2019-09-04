namespace Group.Salto.SOM.Web.Models.Client
{
    public class ClientsFilterViewModel : BaseFilter
    {
        public ClientsFilterViewModel()
        {
            OrderBy = nameof(CorporateName);
        }

        public string CorporateName { get; set; }
    }
}