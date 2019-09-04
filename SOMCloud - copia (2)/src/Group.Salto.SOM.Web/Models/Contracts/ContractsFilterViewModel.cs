namespace Group.Salto.SOM.Web.Models.Contracts
{
    public class ContractsFilterViewModel : BaseFilter
    {
        public ContractsFilterViewModel()
        {
            OrderBy = nameof(Object);
        }

        public string Object { get; set; }
    }
}