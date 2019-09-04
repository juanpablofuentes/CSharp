namespace Group.Salto.SOM.Web.Models.SubContract
{
    public class SubContractFilterViewModel : BaseFilter
    {
        public SubContractFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}