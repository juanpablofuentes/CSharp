namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemsFilterViewModel: BaseFilter
    {
        public ItemsFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string ERPReference { get; set; }
        public string InternalReference { get; set; }
        public bool Blocked { get; set; }
    }
}