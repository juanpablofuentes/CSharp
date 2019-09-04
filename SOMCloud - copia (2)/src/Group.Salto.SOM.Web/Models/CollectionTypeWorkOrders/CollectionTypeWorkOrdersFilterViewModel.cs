namespace Group.Salto.SOM.Web.Models.CollectionTypeWorkOrders
{
    public class CollectionTypeWorkOrdersFilterViewModel : BaseFilter
    {
        public CollectionTypeWorkOrdersFilterViewModel()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}