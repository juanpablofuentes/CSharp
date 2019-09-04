using Group.Salto.Controls.Table.Models;

namespace Group.Salto.SOM.Web.Models.Items
{
    public class ItemsListViewModel
    {
        public MultiItemViewModel<ItemViewModel, int> Items { get; set; }

        public ItemsFilterViewModel ItemsFilters { get; set; }
    }
}